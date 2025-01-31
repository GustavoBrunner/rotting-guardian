using Game.Audio;
using Game.Combat;
using Game.Data.Attributes;
using Game.Enemy;
using Game.GameCamera;
using Game.Player;
using Game.Ui.Hud;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public delegate void PlayerDied();
public delegate void NpcFound();
public delegate void TriggerInteracted(TriggerType type, Vector3 newPos);
namespace Game.Controllers
{

    public static class GameControllerDelegates
    {
        public static PlayerDied PlayerDied;
        public static NpcFound NpcFound;
        public static TriggerInteracted TriggerInteracted;
    }
    public enum GamePhase
    {
        first,
        second,
        third
    }
    public class GameController : MonoBehaviour
    {
        private static GameController _instance;
        public static GameController instance { get { return _instance; } }
        public static GamePhase Phase;
        public int Gold { 
            get => _gold; 
            set
            {
                _gold = value;
                HudDelegates.UpdateGold(_gold);
            }
        }
        [field: SerializeField] public bool IsNpcFound { get; private set; } = false;


        [SerializeField] private int _gold;
        [SerializeField] private CameraController Cam;
        [SerializeField] private Vector3 StartPos, NpcPos, FirstToSec, SecToFirst, SecToThird, ThirdToSec;  
        //[SerializeField] private Vector3 Hole01Exit;
        //[SerializeField] private Vector3 Hole02Exit;
        //[SerializeField] private Vector3 Hole03Exit;
        [SerializeField] private Quaternion Rotation;  
        [SerializeField] private GameObject Player, FirstPhase, SecondPhase, ThirdPhase, CurrentPhase;

        public List<EnemyDataController> EnemyKilled;

        [SerializeField] private GameObject Lv1EnemyPrefab, Lv2EnemyPrefab, Lv3EnemyPrefab, CurrentEnemiesPrefab;

        [field: SerializeField]public List<GameObject> SpawnedItems { get; private set; } = new();
        private void Awake()
        {
            _instance = this;
            Application.targetFrameRate = 60;
            Phase = GamePhase.first;
            CurrentPhase = FirstPhase;
            CurrentEnemiesPrefab = Lv1EnemyPrefab;
        }
        private void Start()
        {
            //AudioManager.Instance.PlayAmbienceMusic(AudioEvents.Instance.AmbienceSongEvent);
            //AudioManager.Instance.PlayMusic(AudioEvents.Instance.MusicEvent);
            Cam = FindObjectOfType<CameraController>();
            GameControllerDelegates.TriggerInteracted += PhaseLoader;
            GameControllerDelegates.PlayerDied += PlayerDied;
            AttrImprovementDelegates.ImproveAttribute += TransactionFinished;
        }
        private void OnDestroy()
        {
            GameControllerDelegates.TriggerInteracted -= PhaseLoader;
            GameControllerDelegates.PlayerDied -= PlayerDied;
            AttrImprovementDelegates.ImproveAttribute -= TransactionFinished;
        }

        private void SpendGold(TransactionDto transaction)
        {
            this.Gold -= transaction.Price;
        }
        private void UnloadDroppedItens()
        {
            var spawnedItens = FindObjectsOfType<Item>();
            if(spawnedItens.Length > 0)
            {
                foreach (var item in spawnedItens)
                {
                    DestroyImmediate( item.transform.parent.gameObject, true);
                    
                }
            }
                
        }

        private void PhaseLoader(TriggerType type, Vector3 newPos)
        {
            switch (type)
            {
                
                case TriggerType.doorTrigger:
                    StartCamAnim(CamAnimType.middle);
                    StartCoroutine(UpdatePosTimer(newPos));
                    break;
                case TriggerType.hole:
                    StartCoroutine(TurnNextPhaseTimer(FirstPhase));
                    StartCamAnim(CamAnimType.middle);
                    InstantiatePlayer(newPos);
                    TurnOnAllEnemies();
                    CurrentEnemiesPrefab.SetActive(false);
                    CurrentEnemiesPrefab = Lv1EnemyPrefab;
                    CurrentEnemiesPrefab.SetActive(true);
                    UnloadDroppedItens();
                    break;
                case TriggerType.firstToSecPhase:
                    StartCoroutine(TurnNextPhaseTimer(SecondPhase));
                    StartCamAnim(CamAnimType.middle);
                    InstantiatePlayer(FirstToSec);
                    TurnOnAllEnemies();
                    CurrentEnemiesPrefab.SetActive(false);
                    CurrentEnemiesPrefab = Lv2EnemyPrefab;
                    CurrentEnemiesPrefab.SetActive(true);
                    UnloadDroppedItens();
                    break;
                case TriggerType.secondToFirstPhase:
                    StartCoroutine(TurnNextPhaseTimer(FirstPhase));
                    StartCamAnim(CamAnimType.middle);
                    InstantiatePlayer(SecToFirst);
                    TurnOnAllEnemies();
                    CurrentEnemiesPrefab.SetActive(false);
                    CurrentEnemiesPrefab = Lv1EnemyPrefab;
                    CurrentEnemiesPrefab.SetActive(true);
                    UnloadDroppedItens();
                    break;
                case TriggerType.secondToThirdPhase:
                    StartCoroutine(TurnNextPhaseTimer(ThirdPhase));
                    StartCamAnim(CamAnimType.middle);
                    InstantiatePlayer(SecToThird);
                    CurrentEnemiesPrefab.SetActive(false);
                    CurrentEnemiesPrefab = Lv3EnemyPrefab;
                    CurrentEnemiesPrefab?.SetActive(true);
                    UnloadDroppedItens();
                    break;
                case TriggerType.thirdToSecPhase:
                    StartCoroutine(TurnNextPhaseTimer(SecondPhase));
                    StartCamAnim(CamAnimType.middle);
                    InstantiatePlayer(ThirdToSec);
                    CurrentEnemiesPrefab.SetActive(false);
                    CurrentEnemiesPrefab = Lv2EnemyPrefab;
                    CurrentEnemiesPrefab?.SetActive(true);
                    UnloadDroppedItens();
                    break;
                default:
                    break;
            }
        }
        public void NpcFound(bool isFound = true)
        {
            IsNpcFound = isFound;
        }
        private void InstantiatePlayer(Vector3 newPos)
        {
            //update player position
            StartCoroutine(UpdatePosTimer(newPos));
        }
        private void InstantiatePlayer()
        {
            InstantiatePlayer(Vector3.zero);
        }

        private void StartCamAnim(CamAnimType type)
        {
            if (Cam != null)
            {
                switch (type)
                {
                    case CamAnimType.none:
                        Debug.LogError("No animation type");
                        break;
                    case CamAnimType.shorter:
                        Cam.FadeAnimator.SetTrigger("FadeShort");
                        break;
                    case CamAnimType.middle:
                        Cam.FadeAnimator.SetTrigger("FadeMiddle");
                        break;
                    case CamAnimType.longer:
                        Cam.FadeAnimator.SetTrigger("FadeLonger");
                        break;
                    default:
                        break;
                }
            }
            else
                Debug.LogWarning("Animator not found!");
        }
        public void TurnOnAllEnemies()
        {
            if (EnemyKilled.Count <= 0) return;

            foreach(var enemy in EnemyKilled)
            {
                enemy.gameObject.SetActive(true);
                enemy.GetComponent<EnemyController>().isDead = false;
            }
        }

        private void PlayerDied()
        {
            PlayerDelegates.CanMove(false);
            HudDelegates.DeathScreen(true);
            StartCoroutine(RespawnPlayer());
        }
        private IEnumerator RespawnPlayer()
        {
            
            Gold = Mathf.RoundToInt(Gold / 2);

            if (CurrentPhase != FirstPhase)
            {
                CurrentPhase.SetActive(false);
                CurrentPhase = FirstPhase;
                CurrentPhase.SetActive(true);
                Phase = GamePhase.first;
            }
            TurnOnAllEnemies();
            CurrentEnemiesPrefab = Lv1EnemyPrefab;
            CurrentEnemiesPrefab.SetActive(true);

            if (!IsNpcFound)
            {
                StartCoroutine(UpdatePosTimer(StartPos, false));
                FindObjectOfType<Player.MovementController>().ResetRotation(new Vector3(0f, 0f, 0f));
            }
            else
            {
                StartCoroutine(UpdatePosTimer(NpcPos, false));
                FindObjectOfType<Player.MovementController>().ResetRotation(new Vector3(0f, -90f, 0f));
            }
            yield return new WaitForSeconds(5f);
            HudDelegates.DeathScreen(false);
            PlayerDelegates.CanMove(true);
        }
        IEnumerator UpdatePosTimer(Vector3 pos, bool camAnim = true)
        {
            if(camAnim)
                StartCamAnim(CamAnimType.middle);
            var playerPos = FindObjectOfType<PlayerController>().transform.position;
            yield return new WaitForSeconds(1.8f);
            PlayerDelegates.UpdatePlayerPos.Invoke(playerPos.Vector3ToTransformY(pos));
        }
        IEnumerator TurnNextPhaseTimer(GameObject nextPhase)
        {
            
            yield return new WaitForSeconds(1f);
            CurrentPhase.SetActive(false);
            CurrentPhase = nextPhase;
            CurrentPhase.SetActive(true);
        }


        private void TransactionFinished(TransactionDto transaction)
        {
            Gold -= transaction.Price;
        }
    }
}
using FMODUnity;
using Game.Combat;
using Game.Controllers;
using Game.Data;
using Game.Player;
using Game.TurnSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


namespace Game.Enemy
{


    public class EnemyController : ActionControllerBase
    {
        public EnemyDataController Data { get; private set; }
        public TurnSystemController TurnController { get; private set; }
        public PlayerDataController PlayerDataController { get; private set; }
        public MovementController MovementController { get; private set; }
        public CombatController CombatController { get; private set; }
        public GridManager GridManager { get; private set; }

        public IndividualTurnController ITC { get; private set; }
        public float CombatDistance { get; private set; } = 1.5f; //4f last value
        public float AttackDistance { get; private set; } = 1.01f; //2.1f correct value


        [SerializeField]
        private EventReference applyDamageSFx;
        [SerializeField] private FMODUnity.StudioEventEmitter noticePlayer;

        private bool playNoticeSFX = true;

        private IDamageble player;

        private EnemyAnimations animations;

        [field: SerializeField] public float DelayTime { get; private set; } = 3;

        private EnemyInteraction interaction;

        private bool isActing = false;

        public bool isDead = false;

        private void Awake()
        {
            TurnController = FindAnyObjectByType<TurnSystemController>();
            PlayerDataController = FindAnyObjectByType<PlayerDataController>();
            CombatController = FindAnyObjectByType<CombatController>();
            player = PlayerDataController.GetComponent<PlayerController>();
            Data = GetComponent<EnemyDataController>();
            //MovementController = GetComponent<MovementController>();
            GridManager = FindObjectOfType<GridManager>();


            interaction = GetComponent<EnemyInteraction>(); 
            ITC = GetComponent<IndividualTurnController>();

            animations = new(GetComponent<Animator>());
        }


        void Update()
        {
        //    if (Input.GetKeyDown(KeyCode.I)) 
        //    { 
        //        ITC.FinishAction(); 
        //    }
            if (Vector3.Distance(transform.position, PlayerDataController.transform.position) < CombatDistance)
            {
                //init combat
                StartCoroutine(playHEYsfx());
                TurnController.AddEnemyCombat(this);
                Debug.Log("Combat distance");
                GetComponent<MovementController>().RotateToPlayer();
                if(Vector3.Distance(transform.position, PlayerDataController.transform.position) < AttackDistance)
                    CombatController.ChangeCombatPhase(true);
                else
                    CombatController.ChangeCombatPhase(false);
            }
        }
        private void OnDisable()
        {
            CombatController.ChangeCombatPhase(false);
        }
        private IEnumerator playHEYsfx()
        {
            if (playNoticeSFX == true)
            {
                //noticePlayer.Play();
                playNoticeSFX = false;
            }
            yield return new WaitForSeconds(1f);
        }

        public override void StartTurn()
        {
            if ( ITC.isActing ) return;
            if ( interaction.CanAttack && !isDead)
            {
                animations.PlayAttackAnim();
                RuntimeManager.PlayOneShot(applyDamageSFx);
                player.TakeDamage(Data.Attributes.Damage);
            }
            ITC.FinishAction(ITC.Delay);
            
        }

        public void PlayDeathAnimation()
        {
            animations.PlayDeathAnim();
        }
        
    }
}

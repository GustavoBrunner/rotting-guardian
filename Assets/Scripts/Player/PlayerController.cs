using FMODUnity;
using Game.Audio;
using Game.Combat;
using Game.GameCamera;
using Game.TurnSystem;
using Game.Ui.Hud;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


namespace Game.Player
{
    [RequireComponent(typeof(CollisionController))]
    public class PlayerController : MonoBehaviour, IDamageble
    {
        [SerializeField] private PlayerDataController PlayerDataController; 
        
        [SerializeField]
        private EventReference applyDamageSFx, missSFX;

        [field: SerializeField] public PlayerHudAnimations PlayerHudAnimations { get; private set; }
        public bool CanBeAttacked { get; set; }

        public void TakeDamage(int damage)
        {

            HitChecker hitChecker = new();
            if (hitChecker.CheckIfHit(PlayerDataController.PlayerAttributes.Defense))
            {
                Debug.Log("Player hitted");
                StartCoroutine(TakeDamageRoutine(damage));
            }
            else
            {
                RuntimeManager.PlayOneShot(missSFX);
            }

        }
        private IEnumerator TakeDamageRoutine(int damage)
        {
            yield return new WaitForSeconds(0.1f);
            RuntimeManager.PlayOneShot(applyDamageSFx);
            PlayerDataController.TakeDamage(damage);
            //if(!PlayerHudAnimations.CheckPlayingAnimation(PlayerHudAnimations.DAMAGE_ANIMATION))

            PlayerHudAnimations.PlayTakeDamageAnim();
            //StartCoroutine(PlayerHudAnimations.AnimationCooldown());

        }

        private void Awake()
        {
            this.PlayerDataController = GetComponent<PlayerDataController>();
            PlayerHudAnimations = new(FindAnyObjectByType<Hud>().PlayerAnimator);    
        }

        public void TakeTrapDamage(int damage)
        {
            PlayerHudAnimations.PlayTakeDamageAnim();
            PlayerDataController.TakeDamage(damage);
        }
    }
}
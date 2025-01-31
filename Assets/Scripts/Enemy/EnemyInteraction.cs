using Game.Audio;
using Game.Combat;
using Game.Objects;
using Game.Player;
using Game.TurnSystem;
using Game.Ui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyInteraction : Interactable, IDamageble
    {
        private const string CUT_ANIM = "Cut_Anim";

        public EnemyDataController DataController;
        private PlayerDataController playerDataController;

        [Range(0f, 180f)]
        [SerializeField] public float RotationModifier;


        private RaycastHit hit;
        [field: SerializeField] public bool CanAttack { get; private set; }
        [SerializeField] private Vector3 offSet = new(0f, 1.5f, 0f);



        [SerializeField] private Animator HudAnimator;

        private EnemyController enemyController;


        //TESTE PARTICLA DE ATAQUE (IGOR)
        [SerializeField] private GameObject particlePrefab;

        [field: SerializeField] public float CombatDistance { get; private set; } = 8f;
        [field: SerializeField] public bool CanBeAttacked { get; set; }
        [SerializeField] private float rayRange = 1.2f;

        private EnemySounds sounds = new();
        private EnemyAnimations animations;

        private void OnDisable()
        {
            this.CanAttack = false;
        }
        private void Awake()
        {
            DataController = GetComponent<EnemyDataController>();
            
            playerDataController = FindAnyObjectByType<PlayerDataController>();

            enemyController = GetComponent<EnemyController>();

            //particlePrefab = Resources.Load<GameObject>("Resources/Prefabs/Particles/SmokeParticle1");

            animations = new EnemyAnimations(this.GetComponent<Animator>());
            this.HudAnimator = GetComponentInChildren<EnemyHud>().GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            CollisionRay();
            Debug.DrawRay(transform.position + offSet, transform.forward , Color.red);
        }
        public void TakeDamage(int damage = 0)
        {
            if(!CanBeAttacked) return;

            CombatEvents.onEnemyAttacked.Invoke();

            var hitChecker = new HitChecker();
            if (hitChecker.CheckIfHit(DataController.Attributes.Defense))
            {
                animations.PlayHitAnim();
                PlayCutAnim(0);
                if (hitChecker.CheckIfHitCritical(playerDataController.PlayerAttributes.CriticalChance))
                {
                    DataController.TakeDamage(playerDataController.PlayerAttributes.Damage,
                        playerDataController.PlayerAttributes.CriticalPercent, true);
                }
                else
                {
                    DataController.TakeDamage(playerDataController.PlayerAttributes.Damage,0);
                    //CombatEvents.onSendEnemyData.Invoke(CombatDto.EnemyData);
                }
                
                DataController.Hud.UpdateHpBar(DataController.Attributes.Hp, DataController.Attributes.MaxHp);
            }
            else
            {
                sounds.PlayMissSound(playerDataController.transform.position);
                PopUpDelegates.CreatePopup("MISS");
            }
            //Debug.Log(this.DataController.Attributes.Hp);
        }
        private void CollisionRay()
        {
            if (this.GetComponent<EnemyController>().isDead) return;
            if (Physics.Raycast(transform.position + offSet, transform.forward, out hit, rayRange))
            {
                if (hit.collider.GetComponent<PlayerController>())
                {
                    CanAttack = true; 
                }
            }
            else
            {
                CanAttack = false; 
            }
        }

        public async void PlayCutAnim(int timer)
        {
            sounds.PlayHitSound(playerDataController.transform.position);
            await Task.Delay(timer);
            this.HudAnimator.Play(CUT_ANIM);
            //TESTE PARTICLA DE ATAQUE (IGOR)
            GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            //TESTE PARTICLA DE ATAQUE (IGOR)
            particleInstance.transform.position += new Vector3(0, 1.2f, 0.5f);
        }

        public void TakeTrapDamage(int damage)
        {
            DataController.TakeDamage(damage, 0f);
        }

        internal void SetCanAttack(bool b)
        {
            CanAttack = b;
        }
    }
}
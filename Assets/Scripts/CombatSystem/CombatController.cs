using FMODUnity;
using Game.Audio;
using Game.Enemy;
using Game.GameCamera;
using Game.Player;
using Game.Ui;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Combat
{
    public class CombatController : MonoBehaviour
    {

        

        private CameraController cameraController;

        [field: SerializeField] public bool IsCombatPhase { get; private set; } = false;
        private void Awake()
        {
            CombatEvents.onEnemyAttacked.AddListener(PlayerAttack);
            cameraController = FindAnyObjectByType<CameraController>();
        }

        public void ChangeCombatPhase(bool  isCombatPhase = false)
        {
            IsCombatPhase = isCombatPhase;
        }

        public void PlayerAttack()
        {
            //if (isAnyoneActing) { return; }
            //isAnyoneActing = true;
            //var hitChecker = new HitChecker();
            //if (hitChecker.CheckIfHit(CombatDto.EnemyData.Attributes.Defense))
            //{
            //    AudioManager.Instance.PlayPlayerAttackSound(AudioEvents.Instance.PlayerAttackEvent, CombatDto.PlayerData.Data.Object.transform.position);
            //    if (hitChecker.CheckIfHitCritical(CombatDto.PlayerData.PlayerAttributes.CriticalChance))
            //    {
            //        CombatDto.EnemyData.TakeDamage(CombatDto.PlayerData.PlayerAttributes.Damage, 
            //            CombatDto.PlayerData.PlayerAttributes.CriticalPercent);
            //    }
            //    else
            //    {
            //        CombatDto.EnemyData.Attributes.Hp -= CombatDto.PlayerData.PlayerAttributes.Damage;
            //        //CombatEvents.onSendEnemyData.Invoke(CombatDto.EnemyData);
            //        CombatDto.EnemyData.Hud.UpdateHpBar(CombatDto.EnemyData.Attributes.Hp);
            //    }
            //}
            //else
            //{

            //}
            
        }


        
        
    }
}

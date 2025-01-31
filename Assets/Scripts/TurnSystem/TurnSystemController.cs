using Game.Combat;
using Game.Enemy;
using Game.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.TurnSystem
{
    public class TurnSystemController : MonoBehaviour
    {
        [SerializeField] private bool EnemiesCanAct;

        [Range(0f, 1f)]
        [SerializeField] private float Cooldown;

        [SerializeField] private List<EnemyController> TurnEnemies = new List<EnemyController>();
        [SerializeField] private PlayerInput PlayerCombat;


        [SerializeField] private CombatController CombatController;

        

        public TurnStates turnState {  get; private set; }

        private void Awake()
        {
            PlayerCombat = FindAnyObjectByType<PlayerInput>();
            CombatController = FindAnyObjectByType<CombatController>();
            turnState = TurnStates.waitingPlayer;
            
            
        }
        private void Update()
        {
            if (!CheckIfEnemyClose())
            {
                CombatController.ChangeCombatPhase();
            }
            

            
        }

        private bool CheckIfEnemyClose()
        {
            foreach (var enemy in TurnEnemies)
            {
                if (Vector3.Distance(enemy.transform.position, PlayerCombat.transform.position) < 8f)
                    return true;
            }
            return false;
        }

        IEnumerator ActCooldown()
        {
            EnemiesCanAct = true;
            yield return new WaitForSeconds(Cooldown);
            EnemiesCanAct = false;
            StopCoroutine(ActCooldown());
        }
        void StartActCooldown()
        {
            StartCoroutine(ActCooldown());
        }

        //public void ChangeTurnState(TurnStates state)
        //{
        //    turnState = state;
        //    foreach (var enemy in TurnEnemies)
        //    {
        //        enemy.ChangeCurrentState(turnState);
        //    }
        //}
        
        public void AddEnemyCombat(EnemyController enemy)
        {
            if (TurnEnemies.Contains(enemy))
                return;
            this.TurnEnemies.Add(enemy);
            Debug.Log("Enemy added to list");
        }
        public void RemoveEnemy(EnemyController enemy)
        {
            if (TurnEnemies.Contains(enemy)) 
                TurnEnemies.Remove(enemy);
            if (TurnEnemies.Count <= 0)
                CombatController.ChangeCombatPhase();
        }

        //public bool CheckIfAllEnemiesActed()
        //{
        //    foreach (var enemy in TurnEnemies)
        //    {
        //        if (enemy.CurrentState == TurnStates.enemyAct)
        //            return false;
        //        break;
        //    }
        //    return true;
        //}
        




        











    }
}
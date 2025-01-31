using Game.Combat;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Game.Enemy
{
    public class MovementController : MonoBehaviour
    {
        private PlayerDataController playerDataController;

        private EnemyDataController enemyDataController;

        private CombatController combatController;


        //private PathController pathController;
        //private NavMeshAgent agent;

        private Vector3 rotTarget;

        private void Awake()
        {
            playerDataController = FindAnyObjectByType<PlayerDataController>();
            enemyDataController = GetComponent<EnemyDataController>();
            combatController = FindAnyObjectByType<CombatController>();
            //pathController = GetComponent<PathController>();
            //agent = GetComponent<NavMeshAgent>();
        }

        private void FixedUpdate()
        {
            if (combatController.IsCombatPhase)
                RotateToPlayer();

            //if (Input.GetKeyDown(KeyCode.Tab))
            //{
            //    Move();
            //}
            //if (agent.velocity.magnitude > 0)
            //{
            //    GetComponent<Animator>().SetBool("IsWalking", true);
            //}
            //else
            //{
            //    GetComponent<Animator>().SetBool("IsWalking", false);
            //}
        }
        public void Move()
        {
            //this.agent.SetDestination(pathController.GetNextNodeInPath().Coords.ToVector3());
            
        }
        public void RotateToPlayer()
        {
            this.rotTarget = playerDataController.Data.Object.transform.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(this.rotTarget, Vector3.up);

            this.transform.rotation = Quaternion.Lerp(transform.rotation, 
                 Quaternion.Euler(0, rotation.eulerAngles.y, 0), Time.deltaTime * enemyDataController.EnemyData.TransitionSpeed) ;
        }

        private bool CheckDistanceToPlayer()
        {
            return Vector3.Distance(transform.position, 
                playerDataController.Data.Object.transform.position) < 4f;
        }
    }
}
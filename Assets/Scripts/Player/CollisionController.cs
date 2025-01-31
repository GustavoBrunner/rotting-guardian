using Game.Enemy;
using Game.GeneralEvents;
using Game.Ui;
using Game.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using System.Threading.Tasks;

namespace Game.Player
{
    public class CollisionController : MonoBehaviour
    {
        public RaycastHit RayFront { get { return _rayFront; } }

        private IDamageble damageble;

        private RaycastHit _rayFront; 
        [SerializeField] private PlayerDataController PlayerDataController;

        [SerializeField] private EnemyInteraction EnemyInteraction;

        private Vector3 offset = new(0f, .5f, 0f);
        [SerializeField] private Vector3 combatOffset = new(0f, .5f, 0f);

        [field: SerializeField] public bool HasTarget { get; private set; } 

        public LayerMask ignoreLayer;
        private int layerMask;

        private void Awake()
        {
            layerMask = ~ignoreLayer.value;
            this.PlayerDataController = GetComponent<PlayerDataController>();
        }

        private void FixedUpdate()
        {

            PlayerDataController.Data.FrontCollision = Physics.Raycast(this.transform.position + offset, transform.forward,
                PlayerDataController.Data.RayDistance, ~ignoreLayer);
            
            PlayerDataController.Data.BackCollision = Physics.Raycast(this.transform.position + offset, -transform.forward, 
                 PlayerDataController.Data.RayDistance, ~ignoreLayer);

            PlayerDataController.Data.LeftCollision = Physics.Raycast(this.transform.position + offset, -transform.right, 
                 PlayerDataController.Data.RayDistance, ~ignoreLayer);

            PlayerDataController.Data.RightCollision = Physics.Raycast(this.transform.position + offset, transform.right, 
                 PlayerDataController.Data.RayDistance, ~ignoreLayer);

            //Debug.DrawRay(this.transform.position + offset, transform.forward * PlayerDataController.Data.RayDistance,
            //    Color.green, PlayerDataController.Data.RayDistance);
            //Debug.DrawRay(this.transform.position + offset, -transform.forward * PlayerDataController.Data.RayDistance,
            //    Color.green, PlayerDataController.Data.RayDistance);
            //Debug.DrawRay(this.transform.position + offset, transform.right * PlayerDataController.Data.RayDistance,
            //    Color.green, PlayerDataController.Data.RayDistance);
            //Debug.DrawRay(this.transform.position + offset, -transform.right * PlayerDataController.Data.RayDistance,
            //    Color.green, PlayerDataController.Data.RayDistance);

            HasTarget = Physics.Raycast(transform.position + combatOffset, transform.forward, out _rayFront, 1.01f);
            if(HasTarget){
                var damageble = _rayFront.collider.GetComponent<IDamageble>();
                if (damageble != null)
                {
                    damageble.CanBeAttacked = true;
                    this.damageble = damageble;
                    return;
                }
            }
            else
            {
                if (this.damageble is null) return;
                this.damageble.CanBeAttacked = false;
                this.damageble = null;
            }
            

        }

    }
}
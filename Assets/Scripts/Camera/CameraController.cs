using Game.Combat;
using Game.Objects;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Game.GeneralEvents;
using Unity.Burst.CompilerServices;

namespace Game.GameCamera
{

    public enum CamAnimType
    {
        none,
        shorter,
        middle,
        longer,
    }


    public class CameraController : MonoBehaviour
    {
        [SerializeField] private PlayerDataController DataController;
        private RaycastHit? hit;

        [SerializeField]private Vector3 screenMousePos;

        [SerializeField] private Camera CurrentCam;

        [SerializeField] private CinemachineVirtualCamera VirtualCamera;

        [SerializeField] private CombatController CombatController;

        [SerializeField]
        private PointerSpriteChanger PointerSpriteChanger = new();

        private CollisionController collisionController;

        private Transform transform;

        private Ray ray;

        [SerializeField] private LayerMask IgnoreLayer;

        private int IgnoreMask;
        [field: SerializeField] public Animator FadeAnimator { get; private set; }
        [SerializeField] private Transform RaycastInitPos;

        private void Start()
        {
            IgnoreMask = ~(1 << IgnoreLayer.value);
            DataController = FindAnyObjectByType<PlayerDataController>();
            VirtualCamera = GetComponentInParent<CinemachineVirtualCamera>();
            CombatController = FindAnyObjectByType<CombatController>();
            collisionController = DataController.GetComponent<CollisionController>();   
            CurrentCam = GetComponent<Camera>();
            CameraEvents.onChangeCamTarget.AddListener(ChangeCamTarget);
            transform = this.gameObject.transform;
        }
        void Update()
        {
            hit = GetMouseRay();
            if (!hit.HasValue) { 
                PointerSpriteChanger.ResetPointer();
                return; 
            };
            
            if (hit.Value.collider.GetComponent<IInteractable>() != null)
            {
                var interactable = hit.Value.collider.GetComponent<IInteractable>();
                ChangeMousePointer(interactable);
            }
        }
        public void CheckObjectHitted()
        {
            if (!hit.HasValue)
                return;

            IInteractable obj = hit.Value.collider.GetComponent<IInteractable>();
                
            if (obj is null)
                return;

            if ((obj as Interactable).CalculateDistance(transform.position))
            {
                if (hit.Value.collider.GetComponent<IDamageble>() != null)
                {
                    if (!collisionController.HasTarget) return;

                    Debug.Log("Enemy interacted");
                    var damageble = obj as IDamageble;
                    //Debug.Log("Player controller null? " + DataController.GetComponent<PlayerController>() != null);
                    DataController.GetComponent<PlayerController>().PlayerHudAnimations.PlayAttackAnim();
                    damageble.TakeDamage();

                    return;
                }
                Debug.Log("Interactive obj");
                obj.Interact();
                return;
            }

        }
        private RaycastHit? GetMouseRay()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast( ray, out hit, Mathf.Infinity/*, ~IgnoreMask*/))
            {
                return hit;
            }
            return null;
        }
        private void ChangeCamTarget(Transform target)
        {
            if(target is null)
            {
                VirtualCamera.m_LookAt = transform;
            }
            VirtualCamera.m_LookAt = target;
        }

        private void ChangeMousePointer(IInteractable interactable)
        {
            switch (interactable.Type)
            {
                case InteractableType.lever:
                    PointerSpriteChanger.LeverPointer();
                    break;
                case InteractableType.chest:
                    PointerSpriteChanger.LeverPointer();
                    break;
                case InteractableType.enemy:
                    PointerSpriteChanger.AttackPointer();
                    break;
                case InteractableType.vase:
                    PointerSpriteChanger.AttackPointer();
                    break;
                case InteractableType.door:
                    PointerSpriteChanger.LeverPointer();
                    break;
                default:
                    PointerSpriteChanger.ResetPointer();
                    break;
            }

        }

    }
}

using FMODUnity;
using Game.Audio;
using Game.GeneralEvents;
using Game.TurnSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Player
{
    public class MovementController : MonoBehaviour
    {

        [SerializeField] private Vector3 TargetGridPos;
        [SerializeField] private Vector3 PrevTargetGridPos;
        [SerializeField] private Vector3 TargetRot;

        private bool _canRun;
        [SerializeField] private PlayerController PlayerController;
        [SerializeField] private CollisionController CollisionController;
        public PlayerDataController PlayerDataController;

        private GridManager gridManager;

        private Action endTurnAction;
        private bool canMove;

        public bool IsAtRest {
            get 
            {   
                //if not moving neither turning to a position, return true 
                if ((Vector3.Distance(transform.position, TargetGridPos) < 0.05f) &&
                    (Vector3.Distance(transform.eulerAngles, TargetRot)) < 0.05f){
                    return true;
                }
                else
                {
                    return false;
                }
            } 
        }
        
        public bool CanRun {
            get => _canRun;
            set
            {
                if (value)
                {
                    PlayerDataController.PlayerAttributes.RunSpeed = 4f;
                }
                else
                {
                    //PlayerDataController.PlayerAttributes.RunSpeed = 2f;
                }
                _canRun = value;
            } 
        }

        //unity methods


        private void Start()
        {
            canMove = true;
            PlayerDelegates.UpdatePlayerPos += UpdatePlayerPos;
            PlayerDelegates.CanMove += SetCanMove;
            PlayerDataController = GetComponent<PlayerDataController>();
            this.CollisionController = GetComponent<CollisionController>();
            gridManager = FindAnyObjectByType<GridManager>();
            TargetGridPos = Vector3Int.RoundToInt(transform.position);
            CanRun = false;
            PlayerDataController.Data.CanMove = true;
            //startPos = transform.position;
            //endPos = transform.position;
            gridManager.UpdatePlayerNodes(transform.position.ToVector2Int());
        }
        private void OnDestroy()
        {
            PlayerDelegates.CanMove -= SetCanMove;
            PlayerDelegates.UpdatePlayerPos -= UpdatePlayerPos;
        }
        private void LateUpdate()
        {
            Move();
            
        }
        private void UpdatePlayerPos(Vector3 newPos)
        {
            this.transform.position = newPos;
            TargetGridPos = newPos;
            
        }
        public void ResetRotation(Vector3 newRot)
        {
            TargetRot = newRot;
        }
        private void SetCanMove(bool b)
        {
            canMove = b;
        }
        //methods
        public void Move()
        {
            PrevTargetGridPos = TargetGridPos;
            Vector3 targetPos = TargetGridPos;
            if (TargetRot.y > 270 && TargetRot.y < 361) TargetRot.y = 0f;
            if (TargetRot.y < 0f) TargetRot.y = 270f;

            
            if (!PlayerDataController.Data.SmoothTransition)
            {
                transform.position = targetPos;
                transform.rotation = Quaternion.Euler(TargetRot);
            }
            else
            {
                transform.SetPositionAndRotation(Vector3.MoveTowards( transform.position, targetPos, 
                        Time.deltaTime * PlayerDataController.Data.TransitionSpeed ), Quaternion.RotateTowards(transform.rotation, 
                    Quaternion.Euler(TargetRot), Time.deltaTime* PlayerDataController.Data.TransitionRotationSpeed));
            }
            

            
        }
        void SentPositionToGrid()
        {
            gridManager.UpdatePlayerNodes(transform.position.ToVector2Int());
        }
        public void RotateLeft() {
            if (!canMove) { return; }
            if (IsAtRest) TargetRot -= Vector3.up * 90f;
            PlaySteps();
        }
        public void RotateRight() {
            if (!canMove) { return; }
            if (IsAtRest) TargetRot += Vector3.up * 90f;
            PlaySteps();
        }
        public void MoveRight() {
            if (!canMove) { return; }
            if (IsAtRest && !PlayerDataController.Data.RightCollision) TargetGridPos += transform.right;
            SentPositionToGrid(); PlaySteps();
        }
        public void MoveLeft() {
            if (!canMove) { return; }
            if (IsAtRest && !PlayerDataController.Data.LeftCollision) TargetGridPos -= transform.right;
            SentPositionToGrid(); PlaySteps();
        }
        public void MoveFoward() {
            if (!canMove) { return; }
            if (IsAtRest && !PlayerDataController.Data.FrontCollision) TargetGridPos += transform.forward;
            SentPositionToGrid(); PlaySteps();
        }
        public void MoveBackward() {
            if (!canMove) { return; }
            if (IsAtRest && !PlayerDataController.Data.BackCollision) TargetGridPos -= transform.forward;
            SentPositionToGrid(); PlaySteps();
        }

        private void PlaySteps()
        {
            AudioManager.Instance.PlayFootsteps(AudioEvents.Instance.PlayerStepsEvent, this.transform.position);
        }

    }
}
using Game.Combat;
using Game.GameCamera;
using Game.TurnSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(MovementController))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private PlayerDataController PlayerDataController;

        [SerializeField] MovementController MovementController;
        private IndividualTurnController iTC;

        [field: SerializeField] public float DelayTime {  get; private set; }        
        
        [SerializeField] CombatController combatController;
        [SerializeField] CameraController cameraController;
        [SerializeField] TurnSystemController turnController;

        private bool isActing = false;

        [field: SerializeField] public TurnStates CurrentState { get; private set; }

        private void Awake()
        {
            PlayerDataController = GetComponent<PlayerDataController>();
            MovementController = GetComponent<MovementController>();
            iTC = GetComponent<IndividualTurnController>();
            
            combatController = FindAnyObjectByType<CombatController>();
            cameraController = FindAnyObjectByType<CameraController>();
            turnController = FindAnyObjectByType<TurnSystemController>();
        }

        private void Update()
        {
            if (iTC.CurrentState == TurnStates.waitingPlayer)
            {
                if (!iTC.isActing)
                {
                    if (Input.GetKeyDown(PlayerDataController.Data.Foward))
                    {
                        MovementController.MoveFoward();
                        //PassTurn(0f);
                    }
                    if (Input.GetKeyDown(PlayerDataController.Data.Backward))
                    {
                        MovementController.MoveBackward();
                        //PassTurn(0f);
                    }
                    if (Input.GetKeyDown(PlayerDataController.Data.Left))
                    {
                        MovementController.MoveRight();
                        //PassTurn(0f);
                    }
                    if (Input.GetKeyDown(PlayerDataController.Data.Right))
                    {
                        MovementController.MoveLeft();
                        //PassTurn(0f);
                    }
                    if (Input.GetKeyDown(PlayerDataController.Data.RotateLeft))
                    {
                        MovementController.RotateLeft();
                    }
                    if (Input.GetKeyDown(PlayerDataController.Data.RotateRight))
                    {
                        MovementController.RotateRight();
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        cameraController.CheckObjectHitted();
                        PassTurn(iTC.Delay);
                    };

                }



                //if (!iTC.isActing)
                //{

                //    if (!MovementController.isAnimating)
                //        MovementController.CheckInput();
                //    else
                //    {
                //        MovementController.Animate();

                //        iTC.FinishAction();
                //    }
                //    
                //}
            }
        }

        private void PassTurn(float delay)
        {
            //EnemyDelegates.RecalculatePath.Invoke();
            if (combatController.IsCombatPhase) 
                iTC.FinishAction(delay);
        }
    }
    
}
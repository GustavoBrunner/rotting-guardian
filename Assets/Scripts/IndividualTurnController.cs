using Game.Enemy;
using Game.Player;
using Game.TurnSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualTurnController : MonoBehaviour
{

    public bool isActing { get; private set; }
    [field: SerializeField] public EntityType Entity { get; private set; }

    [field: SerializeField] public TurnStates CurrentState { get; set; }

    [field: SerializeField] public float Delay { get; private set; }

    private void Start()
    {
        if (this.Entity == EntityType.player)
            TurnEvents.EndEnemyTurn.AddListener(EnterTurn);
        else
            TurnEvents.EndPlayerTurn.AddListener(EnterTurn);
    }

    public void EnterTurn()
    {
        isActing = false;
        if(this.Entity == EntityType.player)
        {
            this.CurrentState = TurnStates.waitingPlayer;
        }
        else
        {
            this.CurrentState = TurnStates.enemyAct;
            var controller = GetComponent<ActionControllerBase>() as EnemyController;
            controller.StartTurn();
        }
    }
    private IEnumerator FinishTurn(float delay)
    {
        isActing = true;
        yield return new WaitForSeconds(delay);
        if (this.Entity == EntityType.player)
        {
            this.CurrentState = TurnStates.enemyAct;
            TurnEvents.EndPlayerTurn.Invoke();
        }
        else
        {
            this.CurrentState = TurnStates.waitingPlayer;
            TurnEvents.EndEnemyTurn.Invoke();
        }
        StopCoroutine(FinishTurn(delay));
    }
    public void FinishAction(float delay = 0f)
    {
       StartCoroutine(FinishTurn(delay)); 
    }

    //private void OnDisable()
    //{
    //    this.FinishAction();
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EndPlayerTurn : UnityEvent { }
public class EndEnemyTurn : UnityEvent { }

public static class TurnEvents
{
    public readonly static EndPlayerTurn EndPlayerTurn = new();
    public readonly static EndEnemyTurn EndEnemyTurn = new();
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatEntity 
{
    Action Action { get; set; }
    Action NextAction { get; set; }

    void EndTurn();

    void FireTurnAction(Action onTurnComplete);

}

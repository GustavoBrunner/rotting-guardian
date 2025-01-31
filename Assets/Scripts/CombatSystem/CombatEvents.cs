using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Combat
{
    public class SendEnemyData : UnityEvent<EnemyDataController> { }
    public class SendPlayerData : UnityEvent<int> { }

    public class  CombatFinished: UnityEvent { }

    public class EnemyAttacked: UnityEvent { }
    public class EnemyFinishedTurn: UnityEvent { }
    public class ResetLifeBar : UnityEvent<int> { }

    public class CombatEvents 
    {
        public readonly static ResetLifeBar onResetLifeBar = new();

        public readonly static CombatFinished onCombatFinished = new CombatFinished();

        public readonly static SendEnemyData onSendEnemyData = new SendEnemyData();

        public readonly static SendPlayerData onSendPlayerData = new SendPlayerData();

        public readonly static EnemyAttacked onEnemyAttacked = new EnemyAttacked();

        public readonly static EnemyFinishedTurn onEnemyFinishedTurn = new EnemyFinishedTurn();
    }
}
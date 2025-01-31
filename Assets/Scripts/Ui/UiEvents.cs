using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Game.Ui
{
    public class UiDebug<T> : UnityEvent<T> { }
    public class UpdatePlayerLife : UnityEvent<PlayerDataController> { }
    public class UpdateEnemyLife : UnityEvent<EnemyDataController> { }

    public class ChangeLifeBar : UnityEvent<PlayerDataController> { }

    public class ChangeXpBar : UnityEvent<PlayerDataController> { }
    public class NpcInteracted : UnityEvent { }

    public class UiEvents<T> 
    {
        public static readonly UiDebug<T> onUiDebug = new UiDebug<T>();
        public static readonly UpdatePlayerLife onUpdatePlayerLife = new UpdatePlayerLife();

        public static readonly UpdateEnemyLife onUpdateEnemyLife = new UpdateEnemyLife();

        public static readonly ChangeLifeBar onChangeLifeBar = new ChangeLifeBar();

        public static readonly ChangeXpBar onChangeXpBar = new ChangeXpBar();
        public static readonly NpcInteracted onNpcInteracted = new();

        //public static readonly UiDebug onUiDebug = new UiDebug();
    }
}
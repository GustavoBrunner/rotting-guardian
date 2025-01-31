using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Ui.Hud
{
    public delegate void UpdateLifeMaxValue(int value); 



    public static class HudDelegates 
    {
        public static UpdateLifeMaxValue UpdateLife;

        public static Action<int> UpdateGold;
        public static Action<bool> DeathScreen;
    }
}
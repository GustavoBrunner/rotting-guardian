using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Attributes
{
    [System.Serializable]
    public record PlayerAttributes : BaseAttributes
    {
        [Header("Player level.")]
        public int Level;

        

        [Header("Actual experience quantity.")]
        public int Xp;

        [Header("Chance to hit a critic attack.")]
        public float CriticalChance;

        [Header("Percentage of player damage to be added to the player damage.")]
        public float CriticalPercent;
    }
}
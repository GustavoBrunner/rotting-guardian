using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Attributes
{
    [System.Serializable]
    public record BaseAttributes
    {
        public int Hp;
        public int MaxHp = 100;
        public int Damage;
        public float RunSpeed;
        public float Defense;
    }
}
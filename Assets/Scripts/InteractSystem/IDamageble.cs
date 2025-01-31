using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public interface IDamageble
    {
        public bool CanBeAttacked { get; set; }
        void TakeDamage(int damage = 0);
        void TakeTrapDamage(int damage);
    }
}
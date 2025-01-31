using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data{

    [System.Serializable]
    public record Data
    {
        [Range(0f, 50f)]
        public float TransitionSpeed;
        public bool CanAttack = true;
        

    }

}
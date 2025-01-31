using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    public record EnemyData : Data
    {
        public GameObject EnemyObject;

        public int GoldQtd = 0;
    }
}
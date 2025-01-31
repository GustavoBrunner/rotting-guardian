using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    public record PlayerData : Data
    {
        public GameObject Object;
        public bool SmoothTransition;
        public bool CanMove;
        

        [Range(0f, 200f)]
        public int TransitionRotationSpeed;
        
        public bool FrontCollision, BackCollision, LeftCollision, RightCollision;

        public bool PlayerAttacked;

        
        public KeyCode Foward;
        public KeyCode Backward;
        public KeyCode Right;
        public KeyCode Left;
        public KeyCode RotateRight;
        public KeyCode RotateLeft;

        public float RayDistance;
    }
}
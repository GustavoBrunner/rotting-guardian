using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Audio
{

    public class AudioEvents : MonoBehaviour
    {
        public static AudioEvents Instance;
        [field: SerializeField] public EventReference PlayerAttackEvent { get; private set; }
        [field: SerializeField] public EventReference EnemyAttackEvent { get; private set; }
        [field: SerializeField] public EventReference MissedAttackEvent { get; private set; }
        [field: SerializeField] public EventReference PlayerStepsEvent { get; private set; }
        [field: SerializeField] public EventReference TorchSoundEvent { get; private set; }
        [field: SerializeField] public EventReference AmbienceSongEvent { get; private set; }
        [field: SerializeField] public EventReference MusicEvent { get; private set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(Instance);
            }
        }
    }
}
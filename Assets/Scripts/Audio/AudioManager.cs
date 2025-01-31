using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Audio{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(this.gameObject);
            }
        }

        //Player sounds
        public void PlayFootsteps(EventReference @event, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(@event, worldPos);
        }

        public void PlayPlayerAttackSound(EventReference @event, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(@event, worldPos);
        }
        public void PlayEnemyAttackSound(EventReference @event, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(@event, worldPos);
        }

        public void PlayAmbienceMusic(EventReference @event)
        {
            RuntimeManager.PlayOneShot(@event);
        }
        public void PlayMusic(EventReference @event)
        {
            RuntimeManager.PlayOneShot(@event);
        }
        public void PlayTorchSound(EventReference @event, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(@event);
        }
        public void PlayMissedAttackSound(EventReference @event, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(@event, worldPos);
        }
    }
}
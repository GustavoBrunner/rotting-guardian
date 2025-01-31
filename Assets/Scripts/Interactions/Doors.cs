using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity; // Para trabalhar com FMOD no Unity
using FMOD.Studio; // Para manipular eventos

namespace Game.Objects
{
    public class Doors : Interactable, IInteractable
    {

        [SerializeField] private FMODUnity.StudioEventEmitter doorSFX;

        private float WaitTime = 1f;

        private void Awake()
        {

        }
        public new void Interact()
        {
            StartCoroutine(WaitAndOpenDoor(WaitTime));
        }

        private IEnumerator WaitAndOpenDoor(float waitTime01)
        {
            yield return new WaitForSeconds(waitTime01);

            doorSFX.Play();
        }
    }
}
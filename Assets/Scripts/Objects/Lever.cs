using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity; // Para trabalhar com FMOD no Unity
using FMOD.Studio; // Para manipular eventos

namespace Game.Objects
{
    public class Lever : Interactable, IInteractable
    {
        [SerializeField] private Animator leverAnimator;
        [SerializeField] private Animator gradeAnimator01;

        [SerializeField] private FMODUnity.StudioEventEmitter leverSFX;
        [SerializeField] private FMODUnity.StudioEventEmitter gradeSFX01;

        [SerializeField] private GameObject RaycastObject;

        public Vector3 Vector3 => throw new System.NotImplementedException();
        public bool useMultLeverSystem = false;
        public MultLeverController leverController;

        private bool isOpen = false;
        private BoxCollider boxCollider;
        private float timeToWaitOpenGrade = 1f;
        private float WaitTime = 0.5f;

        public bool fazNada = false;

        private void Awake()
        {
            if (fazNada == false)
            {
                boxCollider = this.gameObject.GetComponent<BoxCollider>();
                this.leverAnimator = GetComponent<Animator>();
            }
        }
        public new void Interact()
        {
            if (fazNada == false)
            {
                if (!isOpen)
                {
                    leverSFX.Play();
                    isOpen = true;
                    leverAnimator.Play("Open_Lever_Anim");
                    if (useMultLeverSystem == true)
                    {
                        leverController.AddLever(1);
                    }
                    StartCoroutine(WaitAndOpenGrade(WaitTime, timeToWaitOpenGrade));
                }
                else
                {
                    leverSFX.Play();
                    isOpen = false;
                    leverAnimator.Play("Close_Lever_Anim");
                    if (useMultLeverSystem == true)
                    {
                        leverController.RemoveLever(1);
                    }
                    StartCoroutine(WaitAndOpenGrade(WaitTime, timeToWaitOpenGrade));
                }

            }

        }

        private IEnumerator WaitAndOpenGrade(float waitTime01, float waitTime02)
        {
            boxCollider.enabled = false;
            RaycastObject.SetActive(false);

            yield return new WaitForSeconds(waitTime01);

            if(useMultLeverSystem == false)
            {
                gradeAnimator01.SetTrigger("OpenGrade");
                gradeSFX01.Play();
            }

            yield return new WaitForSeconds(waitTime02);

            boxCollider.enabled = true;
            RaycastObject.SetActive(true);
        }
        private void OnEnable()
        {
            if(isOpen)
                StartCoroutine(WaitAndOpenGrade(WaitTime, timeToWaitOpenGrade));
        }
    }
}
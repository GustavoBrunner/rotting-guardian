using Game.Combat;
using Game.Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        public Vector3 Vector3 => transform.position;

        [field: SerializeField] public InteractableType Type { get; set; }

        public virtual void Interact()
        {
            UiEvents<string>.onUiDebug.Invoke("Interact object clicked!");
        }
    }
}
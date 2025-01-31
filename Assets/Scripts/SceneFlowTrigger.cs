using FMOD.Studio;
using FMODUnity;
using Game.Controllers;
using Game.Objects;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum TriggerType
{
    
    npcTrigger,
    doorTrigger,
    firstToSecPhase,
    secondToFirstPhase,
    secondToThirdPhase,
    thirdToSecPhase,
    hole
}
[RequireComponent(typeof(Collider))]
public class SceneFlowTrigger : Interactable, IInteractable
{
    [Header("Enum que indica qual o colisor que está sendo clicado.")]
    [SerializeField] private TriggerType triggerType;

    [Header("Nova posição para o player")]
    [SerializeField] private Vector3 NewPos;

    [SerializeField] private BoxCollider Collider;

    [Tooltip("Evento FMOD a ser reproduzido.")]
    
    [SerializeField] private bool playSFX;

    [SerializeField]
    private EventReference fmodEvent;

    private void Awake()
    {
        if (TryGetComponent<BoxCollider>(out Collider))
            Collider = GetComponent<BoxCollider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() == null)
            return;
        if (this.triggerType == TriggerType.hole)
        {
            GameControllerDelegates.TriggerInteracted(this.triggerType, NewPos);
            RuntimeManager.PlayOneShot(fmodEvent);
        }
    }
    public override void Interact()
    {
        Collider.enabled = false;
        base.Interact();
        GameControllerDelegates.TriggerInteracted.Invoke(triggerType, NewPos);

        if(playSFX == true)
        {
            RuntimeManager.PlayOneShot(fmodEvent);
        }       
    }

}

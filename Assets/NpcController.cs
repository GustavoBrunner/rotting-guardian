using Game.Controllers;
using Game.Objects;
using Game.Player;
using Game.Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NpcController : Interactable, IInteractable
{
    private MovementController movement;
    private void Awake()
    {
        movement = FindAnyObjectByType<Game.Player.MovementController>();
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position, movement.transform.position) < 3f)
        {
            if (!GameController.instance.IsNpcFound)
                GameController.instance.NpcFound();
        }
    }
    public new void Interact()
    {
        UiEvents<string>.onNpcInteracted.Invoke();
        this.gameObject.SetActive(false);
    }

}

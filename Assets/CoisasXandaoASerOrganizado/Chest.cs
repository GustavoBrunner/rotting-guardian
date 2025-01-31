using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableProp
{
    bool isOpen = false;
    Animator animator;
    const string OPENCHEST = "OPENCHEST";
    public GameObject item;
    public EventReference opemSFX01;
    public override void Start()
    {
        this.animator = GetComponentInChildren<Animator>();
    }
    public override void Interact()
    {
        if (!isOpen) { 
            isOpen = true;
            RuntimeManager.PlayOneShot(opemSFX01);
            animator.Play(OPENCHEST);
            StartCoroutine(SpawnItem());
        }
    }

    IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(1);
        GameObject SpawnedItem = Instantiate(item, transform.position + new Vector3(0, .7f, 0f), Quaternion.identity, this.transform);
        //SpawnedItem.transform.localPosition = new Vector3(0, .7f, -.09f);
    }
}

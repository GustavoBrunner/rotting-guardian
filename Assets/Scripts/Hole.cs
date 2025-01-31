using Game.Controllers;
using Game.Objects;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hole : MonoBehaviour
{
    [SerializeField] TriggerType Type;
    [SerializeField] Vector3 NewPos;

    private void Awake()
    {
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() != null)
            GameControllerDelegates.TriggerInteracted(Type,NewPos);
    }
}

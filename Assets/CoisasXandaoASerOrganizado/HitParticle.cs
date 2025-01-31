using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    public ParticleSystem hitEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitEffect.transform.position = collision.contacts[0].point;
            hitEffect.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitEffect.transform.position = other.transform.position;
            hitEffect.Play();
        }
    }
}

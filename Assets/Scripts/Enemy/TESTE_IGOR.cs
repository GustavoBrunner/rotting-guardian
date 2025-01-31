using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTE_IGOR : MonoBehaviour
{
    public ParticleSystem damageParticle; // Arraste o sistema de partículas aqui via inspector

    void OnMouseDown()
    {
        // Verifica se a partícula foi atribuída
        if (damageParticle != null)
        {
            // Ativa a partícula na posição do inimigo
            damageParticle.transform.position = transform.position;
            damageParticle.Play();
        }
        else
        {
            Debug.LogWarning("Sistema de partículas não foi atribuído.");
        }
    }
}

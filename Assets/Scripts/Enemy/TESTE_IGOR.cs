using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTE_IGOR : MonoBehaviour
{
    public ParticleSystem damageParticle; // Arraste o sistema de part�culas aqui via inspector

    void OnMouseDown()
    {
        // Verifica se a part�cula foi atribu�da
        if (damageParticle != null)
        {
            // Ativa a part�cula na posi��o do inimigo
            damageParticle.transform.position = transform.position;
            damageParticle.Play();
        }
        else
        {
            Debug.LogWarning("Sistema de part�culas n�o foi atribu�do.");
        }
    }
}

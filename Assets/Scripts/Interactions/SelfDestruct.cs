using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Tempo em segundos at� o objeto ser destru�do
    public float destructionDelay = 5f;

    void Start()
    {
        // Agenda a destrui��o do objeto ap�s o tempo especificado
        Destroy(gameObject, destructionDelay);
    }
}

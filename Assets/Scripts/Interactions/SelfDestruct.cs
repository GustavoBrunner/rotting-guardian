using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Tempo em segundos até o objeto ser destruído
    public float destructionDelay = 5f;

    void Start()
    {
        // Agenda a destruição do objeto após o tempo especificado
        Destroy(gameObject, destructionDelay);
    }
}

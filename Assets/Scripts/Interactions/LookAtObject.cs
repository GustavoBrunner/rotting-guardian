using Game.Player;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    // O alvo que o objeto vai olhar
    public Transform target;
    private GameObject playerController;

    // Atualiza a rota��o a cada frame

    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>().gameObject;
        target = playerController.transform;
    }
    void Update()
    {
        if (target != null)
        {
            // Calcula a dire��o do alvo
            Vector3 direction = new Vector3 (target.position.x - transform.position.x, 0, target.position.z - transform.position.z);

            // Calcula a rota��o necess�ria para olhar o alvo
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Atualiza a rota��o do objeto
            transform.rotation = targetRotation;
        }
    }
}

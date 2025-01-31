using UnityEngine;

public class RaycastHandlerInteractions : MonoBehaviour
{
    [Header("Configuração do Raycast")]
    public float rayDistance = 10f; // Distância do Raycast
    public LayerMask rayLayerMask; // Define quais camadas o Raycast pode detectar

    [Header("Collider do terceiro objeto")]
    public Collider targetCollider; // Referência ao Collider do terceiro objeto

    private bool m_isEnabled = false;

    private void Update()
    {
        // Dispara o Raycast para frente a partir do centro do objeto
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Verifica se o Raycast colidiu com algo
        if (Physics.Raycast(ray, out hit, rayDistance, rayLayerMask))
        {
            // Verifica se o objeto colidido tem o nome "Player"
            if (hit.collider.gameObject.name == "PlayerInteraction")
            {
                if (m_isEnabled == false)
                {
                    m_isEnabled = true;
                    ActivateTargetCollider(true); // Ativa o Collider do terceiro objeto
                    return;
                }
                else
                {
                    return; // Sai do método para evitar desativação
                }
            }
        }

        // Caso contrário, desativa o Collider do terceiro objeto
        m_isEnabled = false;
        ActivateTargetCollider(false);
    }

    private void ActivateTargetCollider(bool isActive)
    {
        if (targetCollider != null)
        {
            targetCollider.enabled = isActive;
        }
        else
        {
            Debug.LogWarning("Nenhum Collider foi atribuído ao script.");
        }
    }

    private void OnDrawGizmos()
    {
        // Desenha o Raycast no editor para visualização
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}
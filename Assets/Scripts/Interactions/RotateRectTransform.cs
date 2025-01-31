using UnityEngine;

public class RotateRectTransform : MonoBehaviour
{
    [Header("Configura��es")]
    [Tooltip("RectTransform a ser rotacionado.")]
    [SerializeField]
    private RectTransform targetRectTransform;

    [Tooltip("�ngulo de rota��o no eixo Y.")]
    [SerializeField]
    private float rotationAngle;

    /// <summary>
    /// Roda o RectTransform no eixo Y para o �ngulo especificado.
    /// </summary>
    public void RotateY()
    {
        if (targetRectTransform == null)
        {
            Debug.LogWarning("Nenhum RectTransform foi atribu�do.");
            return;
        }

        // Define a nova rota��o no eixo Y
        Vector3 newRotation = targetRectTransform.eulerAngles;
        newRotation.y = rotationAngle;
        targetRectTransform.eulerAngles = newRotation;

        Debug.Log($"RectTransform rotacionado para o �ngulo Y: {rotationAngle}.");
    }
}

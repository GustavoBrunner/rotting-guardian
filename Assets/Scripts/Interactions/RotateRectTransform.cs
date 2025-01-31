using UnityEngine;

public class RotateRectTransform : MonoBehaviour
{
    [Header("Configurações")]
    [Tooltip("RectTransform a ser rotacionado.")]
    [SerializeField]
    private RectTransform targetRectTransform;

    [Tooltip("Ângulo de rotação no eixo Y.")]
    [SerializeField]
    private float rotationAngle;

    /// <summary>
    /// Roda o RectTransform no eixo Y para o ângulo especificado.
    /// </summary>
    public void RotateY()
    {
        if (targetRectTransform == null)
        {
            Debug.LogWarning("Nenhum RectTransform foi atribuído.");
            return;
        }

        // Define a nova rotação no eixo Y
        Vector3 newRotation = targetRectTransform.eulerAngles;
        newRotation.y = rotationAngle;
        targetRectTransform.eulerAngles = newRotation;

        Debug.Log($"RectTransform rotacionado para o ângulo Y: {rotationAngle}.");
    }
}

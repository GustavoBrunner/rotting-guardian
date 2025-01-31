using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Função que fecha a aplicação
    public void QuitGame()
    {
        // Fecha a aplicação
        Application.Quit();

        // Apenas para o editor do Unity (não será necessário no build final)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

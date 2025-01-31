using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Fun��o que fecha a aplica��o
    public void QuitGame()
    {
        // Fecha a aplica��o
        Application.Quit();

        // Apenas para o editor do Unity (n�o ser� necess�rio no build final)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

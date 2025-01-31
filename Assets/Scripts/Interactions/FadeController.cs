using UnityEngine;
using UnityEngine.UI; // Para manipular UI Elements
using System.Collections;

public class FadeController : MonoBehaviour
{
    [Header("Refer�ncias")]
    [SerializeField] private Image fadeImage; // Imagem usada para o fade (geralmente um fundo preto)

    [Header("Configura��es de Tempo")]
    [SerializeField] private float fadeDuration = 1f; // Dura��o do fade em segundos

    private void Start()
    {
        // Inicia com a tela vis�vel (100% opaco)
        SetAlpha(1f);
        // Faz um FadeIn automaticamente no in�cio
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        yield return Fade(1f, 0f); // Vai de opaco (1) para transparente (0)
    }

    public IEnumerator FadeOut()
    {
        yield return Fade(0f, 1f); // Vai de transparente (0) para opaco (1)
    }

    public void FadeInBotton()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeOutBotton()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        // Obt�m a cor inicial da imagem
        Color color = fadeImage.color;
        color.a = startAlpha;

        // Define o alfa inicial
        fadeImage.color = color;

        // Executa o fade
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;

            yield return null; // Espera at� o pr�ximo frame
        }

        // Garante que o alfa final seja definido com precis�o
        color.a = endAlpha;
        fadeImage.color = color;
    }

    // Define o alfa inicial para a imagem
    private void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }
}

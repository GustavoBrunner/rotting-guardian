using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [Header("Configurações")]
    [Tooltip("Nome da cena a ser carregada.")]
    [SerializeField]
    private string sceneName;

    [Tooltip("Tempo do fade em segundos.")]
    [SerializeField]
    private float fadeDuration = 1f;

    [Tooltip("Imagem usada para o efeito de fade.")]
    [SerializeField]
    private Image fadeImage;

    private bool isTransitioning = false;

    /// <summary>
    /// Chamado ao clicar no botão.
    /// </summary>

    public void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void StartSceneTransition()
    {
        if (!isTransitioning)
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    /// <summary>
    /// Coroutine que realiza o fade out e carrega a nova cena.
    /// </summary>
    private IEnumerator FadeOutAndLoadScene()
    {
        isTransitioning = true;

        // Garante que a imagem do fade está visível.
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(fadeDuration/2);
            Color fadeColor = fadeImage.color;

            // Suaviza o alpha da imagem do fade.
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                fadeColor.a = Mathf.Lerp(0, 1, t / fadeDuration);
                fadeImage.color = fadeColor;
                yield return null;
            }

            // Garante que o fade está completo.
            fadeColor.a = 1;
            fadeImage.color = fadeColor;
        }

        // Carrega a nova cena.
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeIn()
    {
        isTransitioning = true;

        // Garante que a imagem do fade está visível.
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            Color fadeColor = fadeImage.color;
            yield return new WaitForSeconds(fadeDuration/2);
            // Suaviza o alpha da imagem do fade.
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                fadeColor.a = Mathf.Lerp(1, 0, t / fadeDuration);
                fadeImage.color = fadeColor;
                yield return null;
            }

            // Garante que o fade está completo.
            fadeColor.a = 0;
            fadeImage.color = fadeColor;
            fadeImage.gameObject.SetActive(false);
            isTransitioning = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AparecerPos : MonoBehaviour
{
    public GameObject gameOver;
    public Button textoPos;

    public float fadeDuration = 1f;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = textoPos.GetComponent<CanvasGroup>();
        if(canvasGroup == null)
        {
            canvasGroup = textoPos.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;

        textoPos.gameObject.SetActive(false);

        Invoke("ShowButton", 1f);
    }

    void ShowButton()
    {
        gameOver.SetActive(true);

        textoPos.gameObject.SetActive(true);

        StartCoroutine(FadeIn());

    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }   
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MenuManegerController : MonoBehaviour
{
    public CanvasGroup menuCanvas;     
    public CanvasGroup creditsCanvas;  
    public CanvasGroup fadeOverlay;
    public Animator creditsAnimator;
    public float fadeDuration = 1f;

    public void ShowCredits()
    {
        StartCoroutine(FadeTransition(menuCanvas, creditsCanvas));
    }

    public void ShowMenu()
    {
        StartCoroutine(FadeTransition(creditsCanvas, menuCanvas));
    }

    private System.Collections.IEnumerator FadeTransition(CanvasGroup fromCanvas, CanvasGroup toCanvas)
    {
        fadeOverlay.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }
        fadeOverlay.alpha = 1;

        fromCanvas.alpha = 0;
        fromCanvas.interactable = false;
        fromCanvas.blocksRaycasts = false;
        fromCanvas.gameObject.SetActive(false);

        toCanvas.gameObject.SetActive(true);
        toCanvas.alpha = 1;
        toCanvas.interactable = true;
        toCanvas.blocksRaycasts = true;

        creditsAnimator.Play("Creditos");

        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }
        fadeOverlay.alpha = 0;

        fadeOverlay.gameObject.SetActive(false);
    }
}

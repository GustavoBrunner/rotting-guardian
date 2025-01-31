using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FadeCreditsController : MonoBehaviour
{
    public CanvasGroup creditsCanvas;
    public float fadeDuration = 1f;

    private bool isCreditsActive = false;   

    public void ToggleCredits()
    {
        isCreditsActive = !isCreditsActive;
        StopAllCoroutines();
        StartCoroutine(FadeCanvas(isCreditsActive ? 1 : 0));
    }

    private System.Collections.IEnumerator FadeCanvas(float targetAlpha)
    {
        float startAlpha = creditsCanvas.alpha;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            creditsCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            yield return null;
        }

        creditsCanvas.alpha = targetAlpha;

        creditsCanvas.interactable = targetAlpha > 0.9f;
        creditsCanvas.blocksRaycasts = targetAlpha > 0.9f;
    }
}

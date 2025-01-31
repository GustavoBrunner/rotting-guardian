using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public CanvasGroup mainmenuCanvas; // Arraste o CanvasGroup do menu
    public CanvasGroup creditsCanvas; // Arraste o CanvasGroup dos créditos
    public float fadeDuration = 1f; // Duração do fade

    // Método para mostrar os créditos
    public void ShowCredits()
    {
        StartCoroutine(SwitchCanvas(mainmenuCanvas, creditsCanvas));
    }

    // Método para voltar ao menu
    public void ShowMenu()
    {
        StartCoroutine(SwitchCanvas(creditsCanvas, mainmenuCanvas));
    }

    private System.Collections.IEnumerator SwitchCanvas(CanvasGroup fromCanvas, CanvasGroup toCanvas)
    {
        // Fade-out do canvas atual
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fromCanvas.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }

        fromCanvas.alpha = 0;
        fromCanvas.interactable = false;
        fromCanvas.blocksRaycasts = false;
        fromCanvas.gameObject.SetActive(false); // Desativa o Canvas atual

        // Ativa e faz fade-in no próximo canvas
        toCanvas.gameObject.SetActive(true);
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            toCanvas.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        toCanvas.alpha = 1;
        toCanvas.interactable = true;
        toCanvas.blocksRaycasts = true;
    }
}

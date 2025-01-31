using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public CanvasGroup menuCanvas;
    public CanvasGroup pauseCanvas;
    public CanvasGroup fadeOverlay;
    public GameObject currentCanvas;
    public GameObject commandsCanvas;
    public GameObject backButton;
    private GameObject previousCanvas;
    public float fadeDuration = 1f;


    void Start()
    {
        if (backButton != null)
        {
            backButton.SetActive(false); 
        }
    }

    public void SwitchToCommands()
    {
        if (currentCanvas != null && commandsCanvas != null)
        {
            previousCanvas = currentCanvas;
            pauseCanvas.alpha = 0f;
            //currentCanvas.SetActive(false); 
            commandsCanvas.SetActive(true); 
            currentCanvas = commandsCanvas;

            if (backButton != null)
            {
                backButton.SetActive(true);
            }
        }
        else
        {
            //Debug.LogError("Um ou ambos os canvases não estão atribuídos!");
        }
    }

    public void SwitchBack()
    {
        if (previousCanvas != null)
        {
            currentCanvas.SetActive(false);
            pauseCanvas.alpha = 1f;
            //previousCanvas.SetActive(true);
            currentCanvas = previousCanvas;
            previousCanvas = null; 

            if (backButton != null)
            {
                backButton.SetActive(false);
            }
        }
        else
        {
            //Debug.LogError("Nenhum canvas anterior foi armazenado!");
        }
    }

    public void ReturnToGame()
    {
        if (pauseCanvas != null)
        {
            pauseCanvas.gameObject.SetActive(false);
        }
    }


    private void OnEnable()
    {
        PlayerDelegates.CanMove(false);
    }
    private void OnDisable()
    {
        PlayerDelegates.CanMove(true);
    }
    private void OnDestroy()
    {
        PlayerDelegates.CanMove(true);
    }
    public void TurnPauseMenuOff()
    {
        gameObject.SetActive(false);
    }
    public void BackToMenu()
    {
        SceneLoader.Instance.LoadSceneByName("MenuInicial");
    }
    public void ShowCredits()
    {
        StartCoroutine(FadeTransition(menuCanvas, pauseCanvas));
    }

    public void ShowMenu()
    {
        StartCoroutine(FadeTransition(pauseCanvas, menuCanvas));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public Animator creditsAnimator;
    public string creditsAnimationName = "CreditsAnimation"; 
    public string onCreditsFinishedTrigger = "OnCreditsFinished";

    void Start()
    {
        if (creditsAnimator == null)
        {
            Debug.LogError("Animator não atribuído!");
        }
    }

    public void StartCredits()
    {
        creditsAnimator.Play(creditsAnimationName);
        Debug.Log("Créditos iniciados!");
    }

    public void OnCreditsFinished()
    {
        Debug.Log("Créditos finalizados!");
        StartCoroutine(ReturnToMenu());
    }

    private System.Collections.IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(2f);

        Debug.Log("Voltando ao menu principal...");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAnimationClick : MonoBehaviour
{
    public Animator animator; // Referência ao Animator
    public GameObject objectToAnimate; // GameObject que contém o Animator
    public string animationName; // Nome da animação que você quer tocar

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (objectToAnimate != null && animator != null)
        {
            objectToAnimate.SetActive(true); // Ativa o GameObject
            animator.Play(animationName);   // Toca a animação
        }
    }
}

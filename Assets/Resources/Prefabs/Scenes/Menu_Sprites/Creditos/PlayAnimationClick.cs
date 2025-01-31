using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAnimationClick : MonoBehaviour
{
    public Animator animator; // Refer�ncia ao Animator
    public GameObject objectToAnimate; // GameObject que cont�m o Animator
    public string animationName; // Nome da anima��o que voc� quer tocar

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
            animator.Play(animationName);   // Toca a anima��o
        }
    }
}

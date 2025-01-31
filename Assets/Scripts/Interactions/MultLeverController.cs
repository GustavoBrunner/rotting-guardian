using UnityEngine;
using System.Collections;

public class MultLeverController : MonoBehaviour
{
    [Header("Configura��es")]
    [Tooltip("Quantidade necess�ria para abrir o grade.")]
    [SerializeField]
    private int openGrade = 3; // N�mero necess�rio para abrir a grade

    private int quantLever = 0; // Contador de alavancas acionadas

    private bool isGradeOpen = false;

    [SerializeField] private Animator gradeAnimator;
    [SerializeField] private FMODUnity.StudioEventEmitter gradeSFX;
    /// <summary>
    /// Soma um valor ao contador de alavancas e verifica as condi��es.
    /// </summary>
    /// <param name="value">Valor a ser somado.</param>
    public void AddLever(int value)
    {
        quantLever += value;
        ClampQuantLever();
        Debug.Log($"quantLever ap�s soma: {quantLever}");

        if (quantLever == openGrade)
        {
            isGradeOpen =true;
            StartCoroutine(WaitAndOpenGrade());
        }
    }

    /// <summary>
    /// Subtrai um valor do contador de alavancas e verifica as condi��es.
    /// </summary>
    /// <param name="value">Valor a ser subtra�do.</param>
    public void RemoveLever(int value)
    {
        quantLever -= value;
        ClampQuantLever();
        if (isGradeOpen == true) 
        {
            isGradeOpen = false;
            StartCoroutine(WaitAndOpenGrade());
        }
        Debug.Log($"quantLever ap�s subtra��o: {quantLever}");
    }

    /// <summary>
    /// Garante que quantLever est� dentro dos limites permitidos.
    /// </summary>
    private void ClampQuantLever()
    {
        if (quantLever < 0)
        {
            quantLever = 0;
        }
        else if (quantLever > openGrade)
        {
            quantLever = openGrade;
        }
    }

    /// <summary>
    /// Coroutine chamada quando quantLever atinge o valor de openGrade.
    /// </summary>
    private IEnumerator WaitAndOpenGrade()
    {
        yield return new WaitForSeconds(0.5f); // Substitua 1f pelo tempo desejado
        gradeAnimator.SetTrigger("OpenGrade");
        gradeSFX.Play();
    }
}

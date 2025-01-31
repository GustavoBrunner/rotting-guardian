using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aparecer : MonoBehaviour
{
    public GameObject ProximoTexto;
    IEnumerator aparecerProximoTexto()
    {
        yield return new WaitForSeconds(1.5f);
        ProximoTexto.SetActive(true);
    }
}

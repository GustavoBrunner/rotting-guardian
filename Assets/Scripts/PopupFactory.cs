using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpDelegates
{
    public static Action<string> CreatePopup; 
}


public class PopupFactory : MonoBehaviour
{
    public GameObject DamagePopUpPrefab;
    public Vector3 Offset = new(0, 0, 0);

    PopupSpawnerDummy spawnerDummy;
    private void Start()
    {
        PopUpDelegates.CreatePopup += InstantiatePopup;
    }
    public void InstantiatePopup(string value)
    {
        if ( DamagePopUpPrefab )
        {
            ShowDamagePopUp(value);
        }
    }
    void ShowDamagePopUp(string value)
    {
        Debug.Log("ShowDamagePopUp called with value: " + value);
        spawnerDummy = FindObjectOfType<PopupSpawnerDummy>();
        var go = Instantiate(DamagePopUpPrefab, spawnerDummy.GetPos, spawnerDummy.GetRot );
        go.GetComponent<TextMeshPro>().text = value;
    }
}

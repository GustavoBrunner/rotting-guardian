using Game.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcMenuController : MonoBehaviour
{
    [field: SerializeField] public int critChanceLv { get; private set; } = 1;
    [field: SerializeField] public int damageLv { get; private set; } = 1;
    [field: SerializeField] public int defenseLv { get; private set; } = 1;
    [field: SerializeField] public int hpLv { get; private set; } = 1;

    [SerializeField] GameObject InitialScreen;
    [SerializeField] GameObject ImprovementScreen;
    [SerializeField] GameObject HealScreen;
    [SerializeField] GameObject ConfirmScreen;
    [SerializeField] GameObject StatusScreen;

    [SerializeField] TMP_Text GoldText;
    [SerializeField] TMP_Text ConfirmationText;

    public TransactionDto CurrentTransaction { get; private set; }

    private void OnEnable()
    {
        UpdateGoldText();
        InitialScreen.SetActive(true);
        ImprovementScreen.SetActive(false);
        HealScreen.SetActive(false);
        ConfirmScreen.SetActive(false);
        StatusScreen.SetActive(false);
    }
    private void Update()
    {
        
    }
    private void UpdateGoldText()
    {
        GoldText.text = GameController.instance.Gold.ToString();
    }
    public void Back(GameObject lastScreen)
    {
        lastScreen.SetActive(false);
        InitialScreen.SetActive(true);
        if (!GoldText.transform.parent.gameObject.activeSelf)
            GoldText.transform.parent.gameObject.SetActive(true);
        UpdateGoldText();
    }
    public void ImprovementScreenOn()
    {
        InitialScreen.SetActive(false);
        ImprovementScreen.SetActive(true);
        UpdateGoldText();
    }
    public void HealScreenOn()
    {
        InitialScreen.SetActive(false);
        HealScreen.SetActive(true);
        UpdateGoldText();
    }
    public void ConfirmScreenOn(TransactionDto transaction, string confirmText)
    {
        ConfirmationText.text = confirmText;
        CurrentTransaction = transaction;
        ImprovementScreen.SetActive(false);
        ConfirmScreen.SetActive(true);
    }
    public void FinishTransaction()
    {
        AttrImprovementDelegates.ImproveAttribute(CurrentTransaction);
        switch (CurrentTransaction.Type)
        {
            case TransactionType.none:
                break;
            case TransactionType.hp:
                hpLv++;
                break;
            case TransactionType.damage:
                damageLv++;
                break;
            case TransactionType.crit:
                critChanceLv++;
                break;
            case TransactionType.defense:
                defenseLv++;
                break;
            default:
                break;
        }
        Back(ConfirmScreen);
    }
    public void StatusScreenOn()
    {
        InitialScreen.SetActive(false);
        StatusScreen.SetActive(true);
        GoldText.transform.parent.gameObject.SetActive(false);
    }
    public void CloseNpcMenu()
    {
        this.gameObject.SetActive(false);
    }


}

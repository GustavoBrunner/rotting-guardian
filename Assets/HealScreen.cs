using Game.Controllers;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealScreen : MonoBehaviour
{
    [SerializeField] Button HealAllBtn, PartialHealBtn;

    [SerializeField] int HealAllPrice, PartialHealPrice;

    private PlayerDataController playerDataController;

    private void Awake()
    {
        playerDataController = FindAnyObjectByType<PlayerDataController>();
    }
    private void OnEnable()
    {
        HealAllBtn.interactable = false;
        PartialHealBtn.interactable = false;
        var money = GameController.instance.Gold;
        if(money >= HealAllPrice)
        {
            HealAllBtn.interactable = true;
            HealAllBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            HealAllBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }
        if (money >= PartialHealPrice)
        {
            PartialHealBtn.interactable = true;
            PartialHealBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            PartialHealBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }
    }


    public void HealAllLife()
    {
        PlayerDelegates.HealHp(300);
        GameController.instance.Gold -= HealAllPrice;
    }
    public void HealPartialLife()
    {
        PlayerDelegates.HealHp(50);
        GameController.instance.Gold -= PartialHealPrice;
    }
}

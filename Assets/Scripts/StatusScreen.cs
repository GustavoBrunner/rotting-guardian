using Game.Controllers;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusScreen : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text lifeTxt;
    [SerializeField] TMPro.TMP_Text attackTxt;
    [SerializeField] TMPro.TMP_Text defenseTxt;
    [SerializeField] TMPro.TMP_Text critChanceTxt;
    [SerializeField] TMPro.TMP_Text goldTxt;
    private void OnEnable()
    {
        var playerData = FindObjectOfType<PlayerDataController>();
        lifeTxt.text = $"HEALTH POINTS [{playerData.PlayerAttributes.Hp}]";
        defenseTxt.text = $"DEFENSE [{playerData.PlayerAttributes.Defense}]";
        attackTxt.text = $"DAMAGE [{playerData.PlayerAttributes.Damage}]";
        critChanceTxt.text = $"CRITICAL CHANCE [{playerData.PlayerAttributes.CriticalChance}]";
        goldTxt.text = GameController.instance.Gold.ToString();
    }
}

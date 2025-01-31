using Game.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void ImproveAttribute(TransactionDto dto);

public class AttribImprovementSystem : MonoBehaviour
{
    private NpcMenuController menuController;

    

    [SerializeField] private Button HpBtn, DamageBtn, DefenseBtn, CritBtn;
    private void OnEnable()
    {
        var money = GameController.instance.Gold;
        if (money > GetTransactionPrice(menuController.hpLv))
        {
            HpBtn.interactable = true;
            HpBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            HpBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }
        if (money > GetTransactionPrice(menuController.damageLv))
        {
            DamageBtn.interactable = true;
            DamageBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            DamageBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }
        if (money > GetTransactionPrice(menuController.defenseLv))
        {
            DefenseBtn.interactable = true;
            DefenseBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            DefenseBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }
        if (money > GetTransactionPrice(menuController.critChanceLv))
        {
            CritBtn.interactable = true;
            CritBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            CritBtn.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }
    }
    private void OnDisable()
    {
        HpBtn.interactable = false;
        DamageBtn.interactable = false;
        DefenseBtn.interactable = false; 
        CritBtn.interactable = false;
    }
    private void Awake()
    {
        menuController = GetComponentInParent<NpcMenuController>();
    }

    public void NewHp()
    {
        var newTransaction = CreateNewHpTransaction();
        menuController.ConfirmScreenOn(newTransaction, 
            $"Pay {newTransaction.Price} gold to improve your {newTransaction.TransactionName}?");
    }
    public void NewDefense()
    {
        var newTransaction = CreateNewDefenseTransaction();
        menuController.ConfirmScreenOn(newTransaction,
            $"Pay {newTransaction.Price} gold to improve your {newTransaction.TransactionName}?");
    }
    public void NewCrit()
    {
        var newTransaction = CreateNewCritTransaction();
        menuController.ConfirmScreenOn(newTransaction,
            $"Pay {newTransaction.Price} gold to improve your {newTransaction.TransactionName}?");
    }
    public void NewDamage()
    {
        var newTransaction = CreateNewDamageTransaction();
        menuController.ConfirmScreenOn(newTransaction,
            $"Pay {newTransaction.Price} gold to improve your {newTransaction.TransactionName}?");
    }

    private TransactionDto CreateNewHpTransaction()
    {
        return new TransactionDto() 
        {
            Type = TransactionType.hp,
            Improvement = 10,
            Price = GetTransactionPrice(menuController.hpLv),
        };
    }
    private TransactionDto CreateNewDefenseTransaction()
    {
        return new TransactionDto()
        {
            Type = TransactionType.defense,
            Improvement = 2.5f,
            Price = GetTransactionPrice(menuController.defenseLv),
        };
    }
    private TransactionDto CreateNewCritTransaction()
    {
        return new TransactionDto()
        {
            Type = TransactionType.crit,
            Improvement = 1.5f,
            Price = GetTransactionPrice(menuController.critChanceLv),
        };
    }
    private TransactionDto CreateNewDamageTransaction()
    {
        return new TransactionDto()
        {
            Type = TransactionType.damage,
            Improvement = 5f,
            Price = GetTransactionPrice(menuController.damageLv),
        };
    }
    private int GetTransactionPrice(int lv)
    {
        return 25 * lv;
    }
}

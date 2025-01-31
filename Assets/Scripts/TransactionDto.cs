using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransactionType
{
    none,
    hp,
    damage,
    crit,
    defense
}


[System.Serializable]
public record TransactionDto 
{
    public string TransactionName { get
        {
            string name = string.Empty;
            switch (Type)
            {
                case TransactionType.none:
                    break;
                case TransactionType.hp:
                    name = "Health Points";
                    break;
                case TransactionType.damage:
                    name = "Damage";
                    break;
                case TransactionType.crit:
                    name = "Critical Chance";
                    break;
                case TransactionType.defense:
                    name = "Defense";
                    break;
                default:
                    break;
            }
            return name;
        } 
    }
    public TransactionType Type;
    public int Price;
    public float Improvement;
}

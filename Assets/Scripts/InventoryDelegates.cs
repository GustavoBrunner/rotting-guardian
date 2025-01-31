using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GetNewItem(WeaponDto dto);
public delegate void ResetSlot(WeaponDto dto);
public class InventoryDelegates
{
    public GetNewItem GetNewItem;

    public ResetSlot ResetSlot;

    public InventoryDelegates() 
    {
        GetNewItem = null;
        ResetSlot = null;
    }

}

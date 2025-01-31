
using Game.Controllers;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryDelegates Delegates { get; private set; }
    

    [SerializeField] private List<InventorySlot> _inventorySlots = new();
    public IEnumerable<InventorySlot> InventorySlots {  get { return _inventorySlots; } }



    private void Awake()
    {
        Delegates = new();
        StartDelegates();
    }
    private void Start()
    {
        _inventorySlots.AddRange(GetComponentsInChildren<InventorySlot>());
        Debug.Log($"Número de slots no inventário: {_inventorySlots.Count}");
    }

    private void StartDelegates()
    {
        Delegates.GetNewItem += InsertItemOnSlot;
        Delegates.ResetSlot += ResetSlot;
    }
    private void UnlinkDelegates()
    {
        Delegates.GetNewItem -= InsertItemOnSlot;
        Delegates.ResetSlot -= ResetSlot;
    }
    private void InsertItemOnSlot(WeaponDto dto)
    {
        foreach (var slot in InventorySlots)
        {
            if (slot.Type == dto.ItemType)
            {
                if(slot.ItemDto != null)
                {
                    ResetSlot(slot.ItemDto);
                    Instantiate(slot.ItemDto.ItemGo, dto.LastWorldPos, Quaternion.identity);
                }
                slot.UpdateSlotItem(dto.ItemType, dto.Sprite, dto);
                PlayerDelegates.UpdateWeapon.Invoke(dto);
            }
        }
    }
    private void ResetSlot(ItemDto dto)
    {
        foreach (var slot in InventorySlots)
        {
            if (slot.Type == dto.ItemType)
                slot.ResetSlotItem();
        }
    }

}

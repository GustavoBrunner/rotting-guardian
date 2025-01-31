using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.Mathematics;
using Game.Controllers;

public class ItemPool : MonoBehaviour
{
    private static System.Random random = new System.Random();
    private static ItemPool _instance;

    public static ItemPool Instance { get { return _instance; } }
    
    [SerializeField] private List<ItemDto> items = new();

    [SerializeField] Vector3 offset = new(0f, 0.5f, 0f);

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
        else
            DestroyImmediate(_instance);
    }

    private void Start()
    {
        StartDelegates();
    }
    private void OnDestroy()
    {
        UnlinkDelegates();
    }
    void UnlinkDelegates()
    {
        ItemPoolDelegates.InstantiateItem -= InstantiateItem;
    }

    void StartDelegates()
    {
        ItemPoolDelegates.InstantiateItem += InstantiateItem;
    }
    private ItemDto GetDroppedItem()
    {
        double dropRoll = random.NextDouble() * 100;

        ItemType itemType = DetermineItemType(dropRoll);
        Rarity itemRarity = DetermineItemRarity(itemType);
        

        IList<ItemDto> itens = items.Where(i => i.ItemRarity == itemRarity ).ToList();
        itens = itens.Where(i => i.ItemType == itemType ).ToList();


        int index = UnityEngine.Random.Range(0, itens.Count()-1);

        Debug.Log(itemType);
        Debug.Log(itemRarity);
        return itens.ElementAt(index);
    }

    private ItemType DetermineItemType(double roll)
    {
        if (roll < 5) return ItemType.mainWeapon;
        else if (roll < 10) return ItemType.armor;
        else if (roll < 15) return ItemType.secondaryWeapon;
        else if (roll < 20) return ItemType.shield;
        else return ItemType.consumable;
    }

    private Rarity DetermineItemRarity(ItemType type)
    {
        //// Consumables always have common rarity
        //if (type == ItemType.consumable)
        //    return Rarity.common;

        // Non-consumables have a 75% chance to be Common and 25% to be Rare
        double rarityRoll = random.NextDouble() * 100;

        return rarityRoll < 75 ? Rarity.common : Rarity.uncommon;
    }

    public void InstantiateItem( Vector3Int pos )
    {
        var go = GetDroppedItem();
        //GameController.instance.SpawnedItems.Add(go.ItemGo);
        Instantiate(go.ItemGo, pos + offset, Quaternion.identity, this.transform);
    }
    ItemDto GetItemOnList()
    {
        int index = UnityEngine.Random.Range( 0, items.Count-1 );
        return items[index];
    }

}

using Game.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable, IInteractable
{
    [SerializeField] private WeaponDto _data;


    public WeaponDto Data { get { return _data; } }

    public new Vector3 Vector3 => this.transform.position;
    [SerializeField] private SpriteRenderer _image;
    [SerializeField] private GameObject _Item;

    [SerializeField] private FMODUnity.StudioEventEmitter collectSFX;

    private void Awake()
    {
        //_image = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _image.sprite = _data.DropSprite; 
        
    }

    public new void Interact()
    {
        Debug.Log("Item interacted");
        collectSFX.Play();
        Destroy(_Item);
        if (_data.ItemType == ItemType.consumable)
        {
            PlayerDelegates.HealHp(_data.ItemEffect);
            return;
        }
        this.Data.LastWorldPos = this.transform.position;
        Debug.Log(this.Data.LastWorldPos);
        InventoryManager.Delegates.GetNewItem.Invoke(this._data);
        
    }
}

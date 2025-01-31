using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public WeaponDto ItemDto { get => _itemDto; }
    public ItemType Type { get { return _type; } }

    private Animator animator;

    private UnityEngine.UI.Image itemSprite;

    private WeaponDto _itemDto;
    [SerializeField] private ItemType _type;

    Color noAlpha = new(255f, 255f, 255f, 0f);
    Color fullAlpha = new(255f, 255f, 255f, 255f);
    private void Awake()
    {
        this.itemSprite = GetComponent<UnityEngine.UI.Image>();
    }
    public void UpdateSlotItem(ItemType type, Sprite sprite,WeaponDto itemDto ,Animator animator = null)
    {
        if (type != this._type)
            return;
        if (animator != null)
            animator.Play("Idle_Item_Anim");

        this.itemSprite.sprite = sprite;
        this._itemDto = itemDto;
        itemSprite.color = fullAlpha;
    }
    public void ResetSlotItem()
    {
        this.itemSprite.color = noAlpha;
        
        animator = null;
    }
}

using UnityEngine;


[CreateAssetMenu(fileName ="ItemDto", menuName = "Dto/ItemDto")]
public class ItemDto : ScriptableObject
{
    [Header("Define the type of the item")]
    public ItemType ItemType;

    [Header("Define the rarity of the item")]
    public Rarity ItemRarity;

    [Header("Define the effect of the item (damage, defense, healing)")]
    public int ItemEffect;

    [Header("Define the strength of the item")]
    [SerializeField] public Tier ItemTier;

    public string Name;

    [Header("Define the sprite of the item (hud)")]
    public Sprite Sprite;

    [Header("Define the sprite of the item (when dropped on scenario)")]
    public Sprite DropSprite;

    public int Price;

    [Header("Define the description of the item to be displayed when needed")]
    [TextArea(5, 15)]
    private string Description;

    public GameObject ItemGo;

}
public enum Tier
{
    veryLow,
    low,
    medium,
    high,
    veryHigh
}

public enum Rarity
{
    common,
    uncommon
}

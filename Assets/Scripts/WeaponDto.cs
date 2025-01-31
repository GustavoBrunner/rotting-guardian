using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponDto", menuName = "Dto/Weapon")]
public class WeaponDto : ItemDto
{
    [Header("Chance to player hit a critical strike")]
    public float CritChance;

    [Header("The minimum damage a weapon can deal")]
    public int MinDamage;

    [Header("The maximum damage a weapon can deal")]
    public int MaxDamage;

    [Header("Percent to be added to the base damage when critical hit")]
    public float CritPercent;

    public Vector3 LastWorldPos;
}

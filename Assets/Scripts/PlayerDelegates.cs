using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDelegates
{
    public static Action<Vector3> UpdatePlayerPos;
    public static Action<WeaponDto> UpdateWeapon;
    public static Action ResetWeaponStatus;
    public static Action<int> HealHp;

    public static Action<bool> CanMove;
}

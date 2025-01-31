using Game.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InteractableExtensions
{
    public static bool CalculateDistance(this Interactable @this, Vector3 camPos)
    {
        return Vector3.Distance(@this.transform.position, camPos) < 2.5f;
    }
}

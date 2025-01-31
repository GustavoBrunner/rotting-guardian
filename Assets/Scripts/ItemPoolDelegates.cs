using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void InstantiateItem(Vector3Int position);

public static class ItemPoolDelegates
{
    public static InstantiateItem InstantiateItem;
}

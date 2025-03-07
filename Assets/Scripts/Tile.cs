using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    floor,
    itemSpot,
    enemySpot,
    playerSpot
}

public class Tile : MonoBehaviour
{
    public bool IsBlocked;

    public TileType TileType = TileType.floor;

    public Vector2Int cords;

    GridManager gridManager;

    void Start()
    {
        SetCords();

        if (IsBlocked)
            gridManager.BlockNode(cords);
    }

    private void SetCords()
    {
        gridManager = FindObjectOfType<GridManager>();
        int x = (int)transform.position.x;
        int z = (int)transform.position.z;

        cords = new Vector2Int(x / gridManager.GridGap, z / gridManager.GridGap);
    }
}

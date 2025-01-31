using UnityEngine;

[System.Serializable]
public class Node 
{
    public Node(Vector2Int cords, bool walkable)
    {
        this.Coords = cords;
        this.IsWalkable = walkable;
    }

    public Vector2Int Coords;
    public bool IsWalkable;
    public bool IsExplored;
    public bool IsPath;
    public Node ConnectTo;

}

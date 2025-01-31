using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class VectorExtensions
{
    /// <summary>
    /// Combine the new Vector3 position with the transform Vector 3 Y position
    /// </summary>
    /// <param name="this"></param>
    /// <param name="newPos"></param>
    /// <returns></returns>
    public static Vector3 Vector3ToTransformY(this Vector3 @this, Vector3 newPos)
    {
        return new Vector3(newPos.x, @this.y, newPos.z);
    }
    /// <summary>
    /// Get this Vector3 instance and convert to a Vector2Int
    /// </summary>
    /// <param name="pos">relative position</param>
    /// <returns></returns>
    public static Vector2Int ToVector2Int(this Vector3 pos)
    {
        return new((int)pos.x, (int)pos.z);
    }
    /// <summary>
    /// Get this Vector2Int instance and convert to a Vector3
    /// </summary>
    /// <param name="coords">relative coordenates</param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector2Int coords)
    {
        return new(coords.x, 0f, coords.y);
    }
    /// <summary>
    /// Return a vector3Int from a vector 3
    /// </summary>
    /// <param name="pos"> relative position of the player</param>
    /// <returns></returns>
    public static Vector3Int Vector3ToVector3Int(this Vector3 pos) { 
        return new Vector3Int((int) pos.x, (int) pos.y, (int) pos.z);
    }
    /// <summary>
    /// Return a vector3Int from a Vector2Int
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Vector3Int Vector2IntToVector3Int(this Vector2Int pos)
    {
        return new Vector3Int(pos.x, 0, pos.y);
    }
}

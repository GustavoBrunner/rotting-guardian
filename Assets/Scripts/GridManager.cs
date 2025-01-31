using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;



public class GridManager : MonoBehaviour
{
    readonly PhaseArrayBuilder phaseBuilder = new();

    [SerializeField] private int[] phaseMap;

    [SerializeField] private int width, height;

    List<Tile> tiles = new();

    private Vector2Int[] directions = { Vector2Int.down, Vector2Int.up, Vector2Int.right, Vector2Int.left };

    private List<Node> playerNodes = new();
    private List<PathController> observers = new();

    [field: SerializeField] public int GridGap { get; private set; }
    
    public Dictionary<Vector2Int, Node> Nodes { get; private set; } = new();

    public Vector2Int[] PlayerNodesCoords;
    public IEnumerable<Node> PlayerNodes { get => playerNodes; }

    private void Awake()
    {
        phaseMap = phaseBuilder.GetPhaseArray(Phases.second);
        if(GridGap == 0)
        {
            GridGap = 1;
        }
        //var size = Mathf.Sqrt(phaseMap.Length);

        //width = height = (int)size;

        StartTiles();

        //Debug.Log(GetTileByType(TileType.itemSpot).Count());
        
    }
    void StartTiles()
    {
        tiles.AddRange(FindObjectsOfType<Tile>().OrderBy(t => t.gameObject.name));
        foreach (var tile in tiles)
        {
            Vector2Int tilePos = new((int)tile.transform.position.x, (int)tile.transform.position.z);
            Nodes.Add(tilePos, new Node(tilePos, !tile.IsBlocked));
        }
    }
    public void UpdatePlayerNodes(Vector2Int playerPos)
    {
        if (!Nodes.ContainsKey(playerPos))
            return;
        playerNodes.Clear();
        foreach(Vector2Int dir in directions)
        {
            Vector2Int newPos = new((int)dir.x + playerPos.x, (int) dir.y + playerPos.y);
            if (Nodes.ContainsKey(newPos))
            {
                playerNodes.Add(Nodes[newPos]);
            } 
        }
        PlayerNodesCoords = new Vector2Int[PlayerNodes.Count()];
        int index = 0;
        foreach (Node node in PlayerNodes)
        {
            PlayerNodesCoords[index] = node.Coords;
            index++;
        }
        NotifyReceivers();
        Debug.Log(playerNodes.Count);
    }
    public void NotifyReceivers()
    {
        observers.AddRange(FindObjectsOfType<PathController>());
        if(observers.Count > 0)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                //observers[i].CheckPlayerNodes();
            }
        }
    }
    public IEnumerable<Tile> GetTileByType(TileType type)
    {
        return tiles.Where(t => t.TileType == type);
    }

    public void BlockNode(Vector2Int coords)
    {
        if (Nodes.ContainsKey(coords)) 
        {
            Nodes[coords].IsWalkable = false;
        }
    }
    public bool? GetBlockStatus(Vector2Int coords)
    {
        if (!Nodes.ContainsKey(coords))
            return null;
        return Nodes[coords].IsWalkable;
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in Nodes)
        {
            entry.Value.ConnectTo = null;
            entry.Value.IsExplored = false;
            entry.Value.IsPath = false;
        }
    }
    public Vector2Int GetCoordinatesFromPos(Vector3 pos)
    {
        Vector2Int position = new();

        position.x = Mathf.RoundToInt(pos.x / GridGap);
        position.y = Mathf.RoundToInt(pos.z / GridGap);

        return position;    
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coords)
    {
        return new Vector3(coords.x * GridGap, 0f, coords.y * GridGap);
    }

    private void CreatePhase()
    {
        int index = -1;
        for (int i = 0; i < height; i++) 
        {
            for (int j = 0; j < width; j++) 
            {
                Vector2Int coords;
                index++;
                switch (phaseMap[index])
                {
                    case 0:
                        continue;
                    case 1:
                        coords = new(i, j);
                        Nodes.Add(coords, new Node(coords, true));
                        continue;
                    case 2:
                        coords = new(i, j);
                        Nodes.Add(coords, new Node(coords, false));
                        continue;
                    default:
                        continue;
                }

            }
        }
        Debug.Log(Nodes.Count);
    }
    private void DebugCubes()
    {
        foreach (var node in Nodes)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new Vector3(node.Value.Coords.x, 0f, node.Value.Coords.y);
        }
    }
    private void GetWalkableNodes()
    {
        var walkable = Nodes.Where(n => n.Value.IsWalkable is not true).ToList();
        //Debug.Log(walkable.Count);
    }
}

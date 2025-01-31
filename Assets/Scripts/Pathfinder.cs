using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCords;
    public Vector2Int StartCords { get { return startCords; } }

    [SerializeField] Vector2Int targetCords;
    public Vector2Int TargetCords { get { return targetCords; } }

    Node startNode;
    Node targetNode;
    Node currentNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    Vector2Int[] searchOrder = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Nodes;
        }
    }
    public List<Node> GetNewPath()
    {
        return GetNewPath(startCords);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();

        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.IsWalkable = true;
        targetNode.IsWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while (frontier.Count > 0 && isRunning == true)
        {
            currentNode = frontier.Dequeue();
            currentNode.IsExplored = true;
            ExploreNeighbors();
            if (currentNode.Coords == targetCords)
            {
                isRunning = false;
                currentNode.IsWalkable = false;
            }
        }
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new();

        foreach (Vector2Int direction in searchOrder)
        {
            Vector2Int neighborCords = currentNode.Coords + direction;

            if (grid.ContainsKey(neighborCords))
            {
                neighbors.Add(grid[neighborCords]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.Coords) && neighbor.IsWalkable)
            {
                neighbor.ConnectTo = currentNode;
                reached.Add(neighbor.Coords, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        path.Add(currentNode);
        currentNode.IsPath = true;

        while (currentNode.ConnectTo != null)
        {
            currentNode = currentNode.ConnectTo;
            path.Add(currentNode);
            currentNode.IsPath = true;
        }

        path.Reverse();
        return path;
    }


    public void NotifyReceievers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

    public void SetNewDestination(Vector2Int startCoordinates, Vector2Int targetCoordinates)
    {
        startCords = startCoordinates;
        targetCords = targetCoordinates;
        startNode = grid[this.startCords];
        targetNode = grid[this.targetCords];
        GetNewPath();
    }





}

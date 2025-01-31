using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] private Pathfinder pathfinder;
    [SerializeField] private GridManager gridManager;
    private Vector2Int[] directions = new Vector2Int[4] /*{ Vector2Int.right, Vector2Int.up, Vector2Int.left, Vector2Int.down}*/;

    //List<Node> path = new();

    bool canWalk = false;

    Node targetNode;
    [SerializeField] Vector3 nextPos;

    [field: SerializeField] public Vector2Int TargetPos { get; private set; }
    [SerializeField] private Node actualNode;

    [field:SerializeField] public List<Node> path { get; private set; } = new();
    private void Awake()
    {
        pathfinder = GetComponent<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
        directions[0] = transform.up.ToVector2Int();
        directions[1] = -transform.up.ToVector2Int();
        directions[2] = transform.right.ToVector2Int();
        directions[3] = -transform.right.ToVector2Int();
    }
    private void Start()
    {
        EnemyDelegates.RecalculatePath += CheckPlayerNodes;
    }
    private void OnDestroy()
    {
        EnemyDelegates.RecalculatePath -= CheckPlayerNodes;
    }
    public Node GetNextNodeInPath()
    {
        if(this.path.Count <= 0)
            StartPath();

        Node nextNode = this.path[0];
        path.Remove(nextNode);
        return nextNode;
    }
    public void StartPath()
    {
        CheckPlayerNodes();
        Vector2Int initialPos = transform.position.ToVector2Int();
        
        this.pathfinder.SetNewDestination(initialPos, TargetPos);
        RecalculatePath(true);
    }
    public void CheckPlayerNodes()
    {
        if (gridManager == null)
            return;
        var nodes = gridManager.PlayerNodes;
        if (nodes.Count() <= 0)
        {
            return;
        }
        Vector3 nodePos = nodes.ElementAt(0).Coords.ToVector3();
        float distance = Vector3.Distance(transform.position, nodePos);
        float finalDistance = distance;
        targetNode = gridManager.Nodes[transform.position.ToVector2Int()];
        TargetPos = targetNode.Coords;
        for (int i = 0; i < nodes.Count(); i++)
        {
            if (!nodes.ElementAt(i).IsWalkable)
            {
                continue;
            }
            nodePos = nodes.ElementAt(i).Coords.ToVector3();
            distance = Mathf.Abs(Vector3.Distance(transform.position, nodePos));
            if (distance < finalDistance)
            {
                finalDistance = distance;
                targetNode = nodes.ElementAt(i);
                TargetPos = targetNode.Coords;

            }
        }
    }
    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new();
        if (resetPath)
        {
            coordinates = pathfinder.StartCords;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPos(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path.AddRange(pathfinder.GetNewPath(coordinates));
        //StartCoroutine(FollowPath());
    }
    




}

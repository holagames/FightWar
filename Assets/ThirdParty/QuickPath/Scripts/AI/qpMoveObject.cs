using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[AddComponentMenu("QuickPath/AI/Basic Move Object")]
/// <summary>
/// Basic Move Object.
/// </summary>
public class qpMoveObject : MonoBehaviour
{

    /// <summary>
    /// If true the object is moving.
    /// </summary>
    public bool Moving = false;

    /// <summary>
    /// Should this object be able to move? Relevant to disable if you want to make your own movement/motor control script.
    /// </summary>
    public bool AbleToMove = true;
    /// <summary>
    /// Should the object be able to move diagonally?
    /// </summary>
    public bool DiagonalMovement = true;

    /// <summary>
    /// The radius around from each node that this object will go to. If higher it will walk further away from nodes, if lower it will walk closer to nodes.
    /// </summary>
    public float SpillDistance = .1f;

    /// <summary>
    /// Determines how fast this object will move.
    /// </summary>
    public float Speed = 10f;

    /// <summary>
    /// If (0,0,0) is not the correct anchor, you can specify a new anchor here.
    /// </summary>
    public Vector3 Offset = new Vector3(0, 0, 0);

    /// <summary>
    /// The current direction this object is moving in, is always normalized to 1.
    /// </summary>
    public Vector3 MoveDirection = Vector3.zero;

    /// <summary>
    /// The coordinate of the next node in this objects path. Relevant if you want to make your own movement/motor control script.
    /// </summary>
    public Vector3 NextDestination = Vector3.zero;

    /// <summary>
    /// The final destination that the current path of this object leads to.
    /// </summary>
    public Vector3 FinalDestination = Vector3.zero;

    /// <summary>
    /// If true the path of this object will be drawn in editor with lines.
    /// </summary>
    public bool DrawPathInEditor = false;

    protected List<qpNode> Path = new List<qpNode>();
    protected qpNode PreviousNode;
    protected qpNode NextNode;
    protected qpNode DestinationNode = null;

    public GameObject destinationMarker;
    private int _moveCounter = 0;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public virtual void Start()
    {
        FindClosestNode();
    }

    /// <summary>
    /// Creates a path to the desired node, and begins walking the path
    /// </summary>
    /// <param name="destination">Desired desination node</param>
    public void MakePath(qpNode destination)
    {
        SetPath(AStar(_nearNode(), destination));
    }

    /// <summary>
    /// Creates a path to the desired coordinate, and begins walking the path
    /// </summary>
    /// <param name="destination">Desired desination</param>
    public void MakePath(Vector3 destination)
    {
        SetPath(AStar(_nearNode(), qpManager.Instance.FindNodeClosestTo(destination)));
    }

    /// <summary>
    /// Hands the object a new path, which it will immediately begin to walk
    /// </summary>
    /// <param name="path">The new path</param>
    public void SetPath(List<qpNode> path)
    {
        if (path.Count > 0)
        {
            //check to see if already moving and if so determine which node are closest.
            if ((_moveCounter + 1) < Path.Count && path.Count > 1)
            {
                if (Path[_moveCounter].GetCoordinate() == path[1].GetCoordinate()) path.RemoveAt(0);
            }
            _moveCounter = 0;
            FinalDestination = path[path.Count - 1].GetCoordinate();
            Path = path;
            if (DrawPathInEditor) _drawDestination();
            _updateDestinations();
        }
    }

    /// <summary>
    /// Hands the object a new path, which it will immediately begin to walk
    /// </summary>
    /// <param name="path">The new path</param>
    public void SetPath(List<Vector3> path)
    {
        if (path.Count > 0 && qpManager.Instance.nodes.Count > 0)
        {
            _moveCounter = 0;
            FinalDestination = path[path.Count - 1];
            List<qpNode> _list = new List<qpNode>();
            for (int i = 0; i < path.Count; i++) _list.Add(qpManager.Instance.FindNodeClosestTo(path[i]));
            Path = _list;

            if (DrawPathInEditor) _drawDestination();
            _updateDestinations();
        }
    }

    /// <summary>
    /// Called whenever this object reaches its destination. This method is empty and intended to be overriden
    /// </summary>
    public virtual void FinishedPath()
    {
        if (destinationMarker != null) GameObject.DestroyImmediate(destinationMarker);
        destinationMarker = null;
    }

    /// <summary>
    /// Finds the closest node to the object
    /// </summary>
    public void FindClosestNode()
    {
        PreviousNode = qpManager.Instance.FindNodeClosestTo(this.transform.position);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected void FixedUpdate()
    {
        _verifyNodes();
        _move();
        if (DrawPathInEditor && Path.Count > 0)
        {
            for (int i = 1; i < Path.Count; i++)
            {
                qpNode prevNode = Path[i - 1];
                Debug.DrawLine(prevNode.GetCoordinate() + Offset, Path[i].GetCoordinate() + Offset, Color.red, 0, false);
            }
        }
    }
    /// <summary>
    /// Performs an A* algorithm
    /// </summary>
    /// <param name="start">Starting node</param>
    /// <param name="end">Destination Node</param>
    /// <returns>The fastest path from start node to the end node</returns>
    protected List<qpNode> AStar(qpNode start, qpNode end)
    {
        //Debug.Log("astar from "+start.GetCoordinate()+" to "+end.GetCoordinate());
        List<qpNode> path = new List<qpNode>();         //will hold the final path
        bool complete = (end == null || start == null) ? true : false;                                  //Regulates the main while loop of the algorithm
        List<qpNode> closedList = new List<qpNode>();   //Closed list for the best candidates.
        List<qpNode> openList = new List<qpNode>();     //Open list for all candidates(A home for all).
        qpNode candidate = start;                           //The current node candidate which is being analyzed in the algorithm.
        openList.Add(start);                                    //Start node is added to the openlist
        if (start == null || end == null) return null;            //algoritmen cannot be executed if either start or end node are null.

        int astarSteps = 0;
        while (openList.Count > 0 && !complete)                  //ALGORITHM STARTS HERE.
        {
            astarSteps++;
            if (candidate == end)                                //If current candidate is end, the algorithm has been completed and the path can be build.
            {
                DestinationNode = end;
                complete = true;
                bool pathComplete = false;
                qpNode node = end;
                while (!pathComplete)
                {
                    path.Add(node);
                    if (node == start) pathComplete = true;
                    node = node.GetParent();
                }
            }
            List<qpNode> allNodes = (DiagonalMovement ? candidate.Contacts : candidate.NonDiagonalContacts);
            List<qpNode> potentialNodes = new List<qpNode>();
            foreach (qpNode n in allNodes) if (n.traverseable) potentialNodes.Add(n);
            foreach (qpNode n in potentialNodes)
            {
                bool inClosed = closedList.Contains(n);
                bool inOpen = openList.Contains(n);
                //Mark candidate as parent if not in open nor closed.
                if (!inClosed && !inOpen)
                {
                    n.SetParent(candidate);
                    openList.Add(n);
                }
                //But if in open, then calculate which is the better parent: Candidate or current parent.
                else if (inOpen)
                {
                    float math2 = n.GetParent().GetG();
                    float math1 = candidate.GetG();
                    if (math2 > math1)
                    {
                        //candidate is the better parent as it has a lower combined g value.
                        n.SetParent(candidate);
                    }
                }
            }

            //Calculate h, g and total
            if (openList.Count == 0) break;
            openList.RemoveAt(0);
            if (openList.Count == 0) break;
            //the below one-lined for loop,if conditional and method call updates all nodes in openlist.
            for (int i = 0; i < openList.Count; i++) openList[i].CalculateTotal(start, end);
            openList.Sort(delegate(qpNode node1, qpNode node2)
            {
                return node1.GetTotal().CompareTo(node2.GetTotal());
            });

            candidate = openList[0];
            closedList.Add(candidate);
        }
        //Debug.Log("astar completed in " + astarSteps + " steps. Path found:"+complete);
        path.Reverse();
        return path;

    }

    private void _verifyNodes()
    {
        bool outdated = false;
        if (_nearNode() != null)
        {
            if (_nearNode().outdated)
            {
                outdated = true;
            }
            if (DestinationNode != null)
            {
                if (DestinationNode.outdated)
                {
                    outdated = true;
                }
            }
            if (_moveCounter < Path.Count)
            {
                foreach (qpNode node in Path)
                {
                    if (node.outdated)
                    {
                        outdated = true;
                        break;
                    }
                }
            }

            if (outdated)
            {
                MakePath(FinalDestination);
            }
        }
    }

    private void _move()
    {
        if (Path != null)
        {
            if (_moveCounter < Path.Count)
            {
                Moving = true;
                _updateDestinations();
                if (AbleToMove) transform.position = Vector3.MoveTowards(transform.position, Path[_moveCounter].GetCoordinate() + Offset, Time.deltaTime * Speed);
                if (Vector3.Distance(transform.position, Path[_moveCounter].GetCoordinate() + Offset) < SpillDistance)
                {
                    PreviousNode = Path[_moveCounter];
                    _moveCounter++;
                    if (_moveCounter < Path.Count) NextNode = Path[_moveCounter];
                    else FinishedPath();
                }
            }
            else Moving = false;
        }
    }

    private qpNode _nearNode()
    {
        qpNode node = (NextNode == null || !NextNode.outdated ? PreviousNode : NextNode);
        if (node != null)
        {
            if (node.outdated || Vector3.Distance(node.GetCoordinate(),this.transform.position)>(SpillDistance*1.5))
            {
                FindClosestNode();
                return PreviousNode;
            }
        }
        return node;
    }


    private void _drawDestination()
    {
        if (destinationMarker == null)
        {
            destinationMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        destinationMarker.renderer.material.color = new Color(1, 0, 0);
        if (Path.Count > 0) destinationMarker.transform.position = Path[Path.Count - 1].GetCoordinate() + Offset;
    }
    private void _updateDestinations()
    {
        if (NextNode != null && Path != null)
        {
            MoveDirection = Vector3.Normalize(NextNode.GetCoordinate() - this.transform.position);
            NextDestination = NextNode.GetCoordinate();
            FinalDestination = Path[Path.Count - 1].GetCoordinate();

        }
    }
}
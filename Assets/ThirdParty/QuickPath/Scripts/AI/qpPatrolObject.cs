using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[AddComponentMenu("QuickPath/AI/Patrol Object")]
/// <summary>
/// MoveObject that patrols a given path.
/// </summary>
public class qpPatrolObject : qpMoveObject
{

    /// <summary>
    /// The path the object will move, when it reaches its end it will start over.
    /// </summary>
    public List<Vector3> PatrolPath = new List<Vector3>();

    /// <summary>
    /// Should the object reverse the path and walk it, when it finishes the path?
    /// </summary>
    public bool PingPong = true;

    /// <summary>
    /// Should the object perform an A* search algorithm between each point in the patrol path?
    /// </summary>
    public bool PathfindingBetweenPoints = false;

    /// <summary>
    /// Inhertied method from MoveObject, called whenever destination has been reached.
    /// </summary>
    public override void FinishedPath()
    {
        if (PingPong) Path.Reverse();

        SetPath(Path);
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public override void Start()
    {
        base.Start();

        if (!PathfindingBetweenPoints)
        {
            SetPath(PatrolPath);
        }
        if (PathfindingBetweenPoints)
        {
            List<qpNode> _pathfindingPath = new List<qpNode>();
            for (int i = 0; i < PatrolPath.Count - 1; i++)
            {
                _pathfindingPath.AddRange(AStar(qpManager.Instance.FindNodeClosestTo(PatrolPath[i]), qpManager.Instance.FindNodeClosestTo(PatrolPath[i + 1])));
            }
            SetPath(_pathfindingPath);
        }
    }

}

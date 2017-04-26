using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// The Manager contains the most current nodes.
/// </summary>
public sealed class qpManager :UnityEngine.Object{

    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static qpManager Instance
    {
        get
        {

            return _instance;
        }
    }

    /// <summary>
    /// List containing all up-to-date nodes.
    /// </summary>
	public List<qpNode> nodes = new List<qpNode>();
    
    /// <summary>
    /// List containing all waypoints
    /// </summary>
    public List<qpNodeScript>  waypoints = new List<qpNodeScript>();

    /// <summary>
    /// List containing all grid points
    /// </summary>
    public List<qpGridNode> gridpoints = new List<qpGridNode>();

    public List<string> ignoreTags = new List<string>();
    public List<string> disallowedTags = new List<string>();
    public qpGrid.Axis KnownUpDirection = qpGrid.Axis.Y;
    public Vector3 UpVector
    {
        get
        {
            if (KnownUpDirection == qpGrid.Axis.Z) return new Vector3(0, 0, 1);
            else  return new Vector3(0, 1, 0);
        }
        set
        {
        }
    }
    //Singleton class
    private static readonly qpManager _instance = new qpManager();
	
    /// <summary>
    /// Registers nodes(normally send from qpGrid)
    /// </summary>
    /// <param name="selection">The selection of nodes to register.</param>
    public void RegisterNodes(List<qpNode> selection)
    {
        if (nodes == null) nodes = new List<qpNode>();
        nodes.AddRange(selection);
    }

    /// <summary>
    /// Registers a single node(normally send from qp(qpPointNode)
    /// </summary>
    /// <param name="selection"></param>
    public void RegisterNode(qpNode selection)
    {
        selection.outdated = false;
        nodes.Add(selection);
    }

    /// <summary>
    /// Deregisters nodes
    /// </summary>
    /// <param name="selection">nodes to deregister.</param>
    public void DelistNodes(List<qpNode> selection)
    {
        foreach (qpNode node in selection)
        {
            node.outdated = true;
            for (int i = node.Contacts.Count; i > 0; i--)
            {
                node.Contacts[i - 1].RemoveMutualConnection(node);
            }
            nodes.Remove(node);
        }
    }

    /// <summary>
    /// Deregisters a single node
    /// </summary>
    /// <param name="node"></param>
    public void DelistNode(qpNode node)
    {
        node.outdated = true;
        for (int i = node.Contacts.Count; i > 0; i--)
        {
            node.Contacts[i - 1].RemoveMutualConnection(node);
        }
        nodes.Remove(node);
    }

    /// <summary>
    /// Finds the node closest to the given point
    /// </summary>
    /// <param name="vec">The point</param>
    /// <returns>The node closest to the point</returns>
    public qpNode FindNodeClosestTo(Vector3 vec)
    {
        if (qpManager.Instance.nodes.Count == 0)
        {
            Debug.LogError("Object can't move because there are no nodes(you haven't baked a qpGrid or instantiated any qpWayPoints)");
            return null;
        }
        else
        {
            qpNode returnNode;
            //assumes waypoints or grid has been made.
            //Debug.Log("nodes:" + nodes);
            //Debug.Log("nodes length:" + nodes.Count);
            returnNode = nodes[0];
            float distance = Vector3.Distance(returnNode.GetCoordinate(), vec);
            for (int i = 1; i < nodes.Count; i++)
            {
                if (nodes[i] != null)
                {
                    if (Vector3.Distance(nodes[i].GetCoordinate(), vec) < distance)
                    {
                        distance = Vector3.Distance(nodes[i].GetCoordinate(), vec);
                        returnNode = nodes[i];
                    }
                }
            }
            return returnNode;
        }

    }
    public void RegisterWaypoint(qpNodeScript waypoint)
    {
        if(!waypoints.Contains(waypoint))waypoints.Add(waypoint);
    }
    private List<qpNode> _findNodesNear(qpNode target, float radius)
    {
        List<qpNode> retList = new List<qpNode>();

        foreach (qpNode node in nodes)
        {
            if (target != node && Vector3.Distance(target.GetCoordinate(), node.GetCoordinate()) < radius)
            {
                //Debug.Log("node found at:" + Vector3.Distance(target.transform.position, node.transform.position));
                retList.Add(node);
            }
        }
        return retList;
    }

    private qpManager()
    {

        nodes = Array.ConvertAll<UnityEngine.Object, qpNode>(GameObject.FindObjectsOfType(typeof(qpNodeScript)), delegate(UnityEngine.Object i)
        {

            return (i as qpNodeScript).Node;
        }).ToList();
    }



    public void RegisterGridpoint(qpGridNode point)
    {
        gridpoints.Add(point);
    }

    public void RegisterIgnoreTags(List<string> tags)
    {
        foreach (string s in tags)
        {
            if (!ignoreTags.Contains(s)) ignoreTags.Add(s);
        }
    }

    public void RegisterDisallowedTags(List<string> tags)
    {
        foreach (string s in tags)
        {
            if (!disallowedTags.Contains(s)) disallowedTags.Add(s);
        }
    }

    public void DeregisterWaypoint(qpNodeScript script)
    {
        waypoints.Remove(script);
    }
}

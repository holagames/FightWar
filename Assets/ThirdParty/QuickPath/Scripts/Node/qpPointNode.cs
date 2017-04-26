using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A point node(in other words a WayPoint node). Contained by NodeScript.
/// </summary>
public class qpPointNode : qpNode {
    /// <summary>
    /// The nodescript component which holds this node.
    /// </summary>
    public qpNodeScript nodescript;
    public qpPointNode()
    {
        
    }
    /// <summary>
    /// Initializes the node
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="connections"></param>
    public void Init(qpNodeScript parent,List<qpNodeScript> onewayConnections,List<qpNodeScript> mutualConnections)
    {
        nodescript = parent;
        SetCoordinate(parent.transform.position);
        foreach (qpNodeScript ns in onewayConnections) SetConnection(ns.Node);
        foreach (qpNodeScript ns in mutualConnections) SetMutualConnection(ns.Node);
        qpManager.Instance.RegisterNode(this);
    }
    public void SetCoords(Vector3 coord)
    {
        SetCoordinate(coord);
    }
}

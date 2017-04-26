using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("QuickPath/WayPointScript")]
/// <summary>
/// A script that creates a node at its position
/// </summary>
public class qpNodeScript : MonoBehaviour
{
    /// <summary>
    /// The created node
    /// </summary>
    public qpPointNode Node = new qpPointNode();

    /// <summary>
    /// The connections/contacts of the node
    /// </summary>
    public List<qpNodeScript> OneWayConnections = new List<qpNodeScript>();
    public List<qpNodeScript> MutualConnections = new List<qpNodeScript>();
    public bool ShouldScanForWaypointConnections = true;
    public bool ShouldScanForGridConnections = true;
    public float ScanRadius = 10;
    public bool DrawScansInEditor = false;
    public bool DrawConnectionsInEditor = true;
    private List<Vector3> _scanRayCasts = new List<Vector3>();
    
    /// <summary>
    /// Use this for initialization
    /// </summary>
	void Awake () {
        Enable();
        if (renderer != null)
        {
            renderer.enabled = false;
            if (renderer.material != null) renderer.material.color = new Color(0, 1, 0);
        }
	}
    void Start()
    {
        Node.SetCoords(this.transform.position);
        Scan();
    }
    public void Scan()
    {

        List<qpNode> candidates = new List<qpNode>();
        if (ShouldScanForWaypointConnections)
        {
            foreach (qpNodeScript ns in qpManager.Instance.waypoints)
            {
                if (Vector3.Distance(this.transform.position, ns.transform.position) < ScanRadius && ns != this) candidates.Add(ns.Node);
            }
        }
        if (ShouldScanForGridConnections)
        {
            foreach (qpGridNode gn in qpManager.Instance.gridpoints)
            {
                if (Vector3.Distance(gn.GetCoordinate(), this.transform.position) < ScanRadius) candidates.Add(gn);
            }
        }

        foreach (qpNode candidate in candidates)
        {

            //Ray cast a straight line to target;
            bool goodCandidate = this.Node.CanConnectTo(candidate);
            
            if (!goodCandidate) continue;
            else
            {
                //Debug.Log("found a good candidate");
                if (candidate is qpPointNode) MutualConnections.Add((candidate as qpPointNode).nodescript);
                candidate.SetMutualConnection(this.Node);

            }
        }
    }
    //void Update()
    //{
    //    if(DrawScansInEditor)
    //    {
    //        foreach (Vector3 vec in _scanRayCasts)
    //        {
    //            Debug.DrawRay(vec,new Vector3(0,-ScanRadius,0),Color.red);
    //        }
    //    }
    //    if (DrawConnectionsInEditor)
    //    {
    //        foreach (qpNode n in Node.Contacts)
    //        {
    //            Debug.DrawLine(this.transform.position, n.GetCoordinate(), n is qpGridNode?Color.green:Color.yellow);
    //        }
    //    }
        
    //}
    void OnDestroy()
    {
        //Debug.Log("node destroyed");
        Disable();
       
    }
    void OnDisable()
    {
        Disable();
    }
    void OnEnable()
    {
        Enable();
    }
    /// <summary>
    /// Disables the node, making it impossible to create new paths containing it.
    /// </summary>
    void Disable()
    {
        qpManager.Instance.DelistNode(Node);
        qpManager.Instance.DeregisterWaypoint(this);
    }
    /// <summary>
    /// Enables the node, making it possible to create new paths containing it.
    /// </summary>
    void Enable()
    {
        Node.outdated = false;
        Node.Init(this, OneWayConnections, MutualConnections);
        qpManager.Instance.RegisterWaypoint(this);
        
    }

}

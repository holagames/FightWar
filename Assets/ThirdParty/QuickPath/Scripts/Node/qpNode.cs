using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Basic Node
/// </summary>
public abstract class qpNode
{

    /// <summary>
    /// The nodes with which this node is connected to.
    /// </summary>
    public List<qpNode> Contacts = new List<qpNode>();

    public List<qpNode> NonDiagonalContacts = new List<qpNode>();
    /// <summary>
    /// Whether or not objects may traverse this node.
    /// </summary>
    public bool traverseable = true;

    /// <summary>
    /// Whether or not this node is outdated.
    /// </summary>
    public bool outdated = false;

    private float _g = 1;
    private float _h;
    private float _total;
    private qpNode _parent;
    private Vector3 _coordinate;
    public List<Vector3> ScanRayCasts = new List<Vector3>();

    /// <summary>
    /// Gets the coordinate
    /// </summary>
    /// <returns>The coordinate</returns>
    public Vector3 GetCoordinate()
    {
        return _coordinate;
    }
    /// <summary>
    /// Creates a mutual connection between this node and another node.
    /// </summary>
    /// <param name="node">the other node.</param>
    /// <param name="diagonal">Is the other node diagonally placed from this?</param>
    public void SetMutualConnection(qpNode node, bool diagonal = false)
    {
        if (node != null)
        {
            if (!Contacts.Contains(node))
            {
                Contacts.Add(node);
            }
            if (!node.Contacts.Contains(this)) node.Contacts.Add(this);
            if (!diagonal)
            {
                if (!NonDiagonalContacts.Contains(node)) NonDiagonalContacts.Add(node);
                if (!node.NonDiagonalContacts.Contains(this)) node.NonDiagonalContacts.Add(this);
            }
        }
    }

    /// <summary>
    /// Sets connection to another node.
    /// </summary>
    /// <param name="node">The other node</param>
    public void SetConnection(qpNode node)
    {
        if (node != null)
        {
            if (!Contacts.Contains(node))
            {
                Contacts.Add(node);
            }
        }
    }
    /// <summary>
    /// Remove a mutual connection between this node and another node.
    /// </summary>
    /// <param name="otherNode">The other node that this node is currently connected to.</param>
    public void RemoveMutualConnection(qpNode otherNode)
    {
        if (otherNode != null)
        {
            if (Contacts.Contains(otherNode))
            {
                Contacts.Remove(otherNode);
            }
            if (NonDiagonalContacts.Contains(otherNode))
            {
                NonDiagonalContacts.Remove(otherNode);
            }
            if (otherNode.Contacts.Contains(this))
            {
                otherNode.Contacts.Remove(this);
            }
            if (otherNode.NonDiagonalContacts.Contains(this))
            {
                otherNode.NonDiagonalContacts.Add(this);
            }
        }
    }
    protected void SetCoordinate(Vector3 newCoordinate)
    {
        _coordinate = newCoordinate;
    }
    public bool CanConnectTo(qpNode candidate)
    {
        int steps = (int)(Vector3.Distance(candidate.GetCoordinate(), _coordinate) * 2);
        float castDistance = Mathf.Abs((candidate.GetCoordinate() - _coordinate).y) + 4;
        if (qpManager.Instance.KnownUpDirection == qpGrid.Axis.Z) castDistance = Mathf.Abs((candidate.GetCoordinate() - _coordinate).z) + 4;
        Vector3 difference = (candidate.GetCoordinate() - _coordinate) / steps;
        RaycastHit info;
        Vector3 upDirection = qpManager.Instance.UpVector;
        Vector3 downDirection = -upDirection;
        Vector3 myCoordinate = _coordinate + (upDirection / 5);
        Vector3 destinationPoint = candidate.GetCoordinate() - difference + (upDirection / 5);
        if (Physics.Linecast(myCoordinate, destinationPoint, out info))
        {
            //if (qpManager.Instance.disallowedTags.Contains(info.collider.gameObject.tag)) return false;
            return false;
        }

        //Ray cast downward along the previously casted straight line
        for (int i = 1; i < steps; i++)
        {
            int ignoreHits = 0;
            RaycastHit[] hits;

            //Vector3 pureLinearPoint = myCoordinate+(difference*i);
            Vector3 rayCastPositionStart = new Vector3(myCoordinate.x + (difference.x * i), myCoordinate.y + (castDistance / 2), myCoordinate.z + (difference.z * i));
            if (upDirection == new Vector3(0, 0, 1))
            {
                rayCastPositionStart.y = myCoordinate.y + (difference.y * i);
                rayCastPositionStart.z = myCoordinate.z + (castDistance / 2);
            }
            //Debug.Log("ray start:" + rayCastPositionStart + " down:" + downDirection + " castdistance:" + castDistance+" steps"+steps);
            ScanRayCasts.Add(rayCastPositionStart);

            hits = Physics.RaycastAll(rayCastPositionStart, downDirection, castDistance);
            if (hits.Length == 0)
            {
                return false;
            }
            List<float> heightPoints = new List<float>();
            foreach (RaycastHit hit in hits)
            {
                if (qpManager.Instance.disallowedTags.Contains(hit.collider.gameObject.tag))
                {

                    return false;
                }
                else if (qpManager.Instance.ignoreTags.Contains(hit.collider.gameObject.tag))
                {
                    ignoreHits++;
                }
                else
                {
                    if (qpManager.Instance.KnownUpDirection == qpGrid.Axis.Y) heightPoints.Add(hit.point.y);
                    else if (qpManager.Instance.KnownUpDirection == qpGrid.Axis.Z) heightPoints.Add(hit.point.z);
                }
            }
            if (hits.Length == ignoreHits)
            {
                return false;
            }
            //int invalidPoints = 0;
            //float trueHeightPoint = myCoordinate.y+(myCoordinate.y - candidate.GetCoordinate().y) / steps;
            //foreach (float heightPoint in heightPoints)
            //{
            //    if (Mathf.Abs(trueHeightPoint - heightPoint) > 1.5) invalidPoints++;
            //}
            //if (invalidPoints == heightPoints.Count) return false;
        }
        return true;
    }
    #region Pathfinding
    /// <summary>
    /// Used for A*
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public float CalculateTotal(qpNode start, qpNode end)
    {
        _h = CalculateH(end);
        if (_parent != null) _g = CalculateG(_parent) + _parent.GetG();
        else _g = 1;
        _total = _g + _h;
        return _total;
    }
    /// <summary>
    /// Used for A*
    /// </summary>
    public float CalculateH(qpNode end)
    {
        return Vector3.Distance(_coordinate, end.GetCoordinate());
    }
    /// <summary>
    /// Used for A*
    /// </summary>
    public float CalculateG(qpNode parent)
    {
        return Vector3.Distance(_coordinate, parent.GetCoordinate());
    }
    /// <summary>
    /// Used for A*
    /// </summary>
    public float GetG()
    {
        return _g;
    }
    /// <summary>
    /// Used for A*
    /// </summary>
    public float GetTotal()
    {
        return _total;
    }
    /// <summary>
    /// Used for A*
    /// </summary>
    public void SetParent(qpNode parent)
    {
        _parent = parent;
    }
    /// <summary>
    /// Used for A*
    /// </summary>
    public qpNode GetParent()
    {
        return _parent;
    }

    #endregion
}

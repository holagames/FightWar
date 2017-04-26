using UnityEngine;
using System.Collections;

/// <summary>
/// A Grid Node
/// </summary>
public class qpGridNode : qpNode {
    
    /// <summary>
    /// Grid Node Constructor
    /// </summary>
    /// <param name="position">The position of this Node</param>
	public qpGridNode(Vector3 position)
	{
        SetCoordinate(position);
        qpManager.Instance.RegisterGridpoint(this);
	}
    
}

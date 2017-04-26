using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[AddComponentMenu("QuickPath/AI/Follow Target Object")]
/// <summary>
/// MoveObject that follows a Target Gameobject.
/// </summary>
public class qpFollowTargetObject : qpMoveObject {

    /// <summary>
    /// Target to follow.
    /// </summary>
    public GameObject Target;

    /// <summary>
    /// The distance the target has to be lower than for the object to chase.
    /// </summary>
    public float AgroRadius = 100;

    /// <summary>
    /// Whether or not this object should raycast to check for vision between it and the target before it chases.
    /// </summary>
    public bool UseLineOfSight = true;

    /// <summary>
    /// If true then the object will draw its line of sight.
    /// </summary>
    public bool DrawLineOfSightLineInEditor = false;

    /// <summary>
    /// The tags that this object will ignore when raycasting, only relevant if UseLineOfSight is set to true.
    /// </summary>
    public List<string> IgnoreTags = new List<string>();

    private qpNode lastSeenTargetNode = null;

	/// <summary>
    /// Update is called once per frame
	/// </summary>
	protected void Update () {
        if (Target != null)
        {
            bool ShouldChase = false;
            if (Vector3.Distance(Target.transform.position, this.transform.position) < AgroRadius)
            {
                Vector3 myOffset = this.transform.position;
                Vector3 targetOffset = Target.transform.position;
                myOffset.y += .2f;
                targetOffset.y += .2f;
                if (UseLineOfSight)
                {
                    RaycastHit hit;
                    
                    if (Physics.Linecast(myOffset, targetOffset, out hit))
                    {
                        if (hit.collider.gameObject.tag != this.gameObject.tag && hit.collider.gameObject.tag != Target.gameObject.tag)
                        {

                            //Inside agro, but not in Line of Sight
                        }
                        else
                        {
                            //inside agro, and in LineOFSight-LineCast collides only with ignored tags, making path towards target
                            ShouldChase = true;
                        }
                    }
                    else if (this.PreviousNode != qpManager.Instance.FindNodeClosestTo(Target.transform.position))
                    {
                        //inside agro, and in line of sight, making path towards target
                        ShouldChase = true;
                        
                    }

                    if (DrawLineOfSightLineInEditor) Debug.DrawLine(myOffset, hit.point);
                }
                else
                {
                    //Not using line of sight, and inside agro, making path towards player.
                    ShouldChase = true;
                }
            }

            if (ShouldChase)
            {
                if (lastSeenTargetNode != qpManager.Instance.FindNodeClosestTo(Target.transform.position))
                {
                    if (Moving)
                    {
                        List<qpNode> prePath = AStar(PreviousNode, lastSeenTargetNode);
                        for (int i = prePath.Count; i > 0; i--)
                        {
                            if (prePath[i - 1] == NextNode)
                            {
                                prePath.RemoveRange(0, i - 1);
                                break;
                            }
                        }
                        SetPath(prePath);
                    }
                    else MakePath(Target.transform.position);
                }
                lastSeenTargetNode = qpManager.Instance.FindNodeClosestTo(Target.transform.position);
                if (DrawLineOfSightLineInEditor) Debug.DrawLine(this.transform.position, Target.transform.position, Color.magenta, 5f, true);
            }
        }
	}
}

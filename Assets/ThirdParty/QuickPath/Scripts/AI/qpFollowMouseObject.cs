using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[AddComponentMenu("QuickPath/AI/Follow Mouse Object")]
/// <summary>
/// MoveObject that goes where the mouse is clicked.
/// </summary>
public class qpFollowMouseObject : qpMoveObject {

	// Update is called once per frame
	public void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            RaycastHit hit;
            if (Physics.Raycast(raycast, out hit))
            {
                if (Moving)
                {
                    List<qpNode> prePath = AStar(PreviousNode, qpManager.Instance.FindNodeClosestTo(hit.point));
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
                else MakePath(hit.point);
            }
        }
	}

}

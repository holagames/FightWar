using UnityEngine;
using System.Collections;

public class LegionControlbyUI : MonoBehaviour {

    public LegionWarCamera legionWarCamera;

    void OnDrag(Vector2 move)
    {
        legionWarCamera.ControlOnDrag(move);
    }

    void OnDragEnd()
    {
        legionWarCamera.ControlOnDragEnd();
    }
}

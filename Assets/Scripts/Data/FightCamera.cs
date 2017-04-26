using UnityEngine;
using System.Collections;

public class FightCamera {

    public int cameraID { get; private set; }

    public string remark { get; private set; }

    public int targetRule { get; private set; }

    public int cameraSpeed { get; private set; }

    public int zoomAction { get; private set; }

    public FightCamera(int newCameraID, string newRemark, int newTargetRule,
        int newCameraSpeed, int newZoomAction)
    {
        this.cameraID = newCameraID;
        this.remark = newRemark;
        this.targetRule = newTargetRule;
        this.cameraSpeed = newCameraSpeed;
        this.zoomAction = newZoomAction;
    }
}


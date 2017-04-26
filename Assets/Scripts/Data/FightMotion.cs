using UnityEngine;
using System.Collections;

public class FightMotion {

    public int motionID { get; private set; }

    public int skillID { get; private set; }

    public int cardID { get; private set; }

    public string remark { get; private set; }

    public BetterList<int> effectList = new BetterList<int>();
    public BetterList<int> effectTimeList = new BetterList<int>();
    public BetterList<int> cameraList = new BetterList<int>();
    public BetterList<int> cameraTimeList = new BetterList<int>();


    public FightMotion(int newMotionID, int newSkillID, int newCardID, string newRemark,
        string newEffectList, string newEffectTimeList, string newCameraList,
        string newCameraTimeList)
    {
        this.motionID = newMotionID;
        this.skillID = newSkillID;
        this.cardID = newCardID;
        this.remark = newRemark;

        effectList.Clear();
        string[] effectSplit = newEffectList.Split('$');
        for (int i = 0; i < effectSplit.Length - 1; i++)
        {
            effectList.Add(int.Parse(effectSplit[i]));
        }

        effectTimeList.Clear();
        string[] effectTimeSplit = newEffectTimeList.Split('$');
        for (int i = 0; i < effectTimeSplit.Length - 1; i++)
        {
            effectTimeList.Add(int.Parse(effectTimeSplit[i]));
        }

        cameraList.Clear();
        string[] cameraSplit = newCameraList.Split('$');
        for (int i = 0; i < cameraSplit.Length - 1; i++)
        {
            cameraList.Add(int.Parse(cameraSplit[i]));
        }

        cameraTimeList.Clear();
        string[] cameraTimeSplit = newCameraTimeList.Split('$');
        for (int i = 0; i < cameraTimeSplit.Length - 1; i++)
        {
            cameraTimeList.Add(int.Parse(cameraTimeSplit[i]));
        }
    }
}
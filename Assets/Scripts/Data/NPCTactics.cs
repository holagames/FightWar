using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCTactics
{
    public int npcTacticsID { get; private set; }

    public int groupID { get; private set; }
    public int goNum { get; private set; }


    public int manualSkillID { get; private set; }
    public int manualSkillNum { get; private set; }

    public NPCTactics(int _npcTacticsID, int _groupID, int _goNum, int _manualSkillID, int _manualSkillNum)
    {
        this.npcTacticsID = _npcTacticsID;
        this.groupID =_groupID;
        this.goNum = _goNum;
        this.manualSkillID = _manualSkillID;
        this.manualSkillNum = _manualSkillNum;
    }
}

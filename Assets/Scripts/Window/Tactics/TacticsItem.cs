using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TacticsItem: MonoBehaviour 
{
    public UISprite icon;
    [HideInInspector]
    public ManualSkill _ManualSkill;
    public GameObject selectEffect;
    private TacticsWindow _TacticsWindow;
	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(icon.gameObject).onClick = ClickThisTacticsItem;
        UIEventListener.Get(this.gameObject).onClick = ClickThisTacticsItem;
        if (_TacticsWindow == null)
        {
            _TacticsWindow = GameObject.Find("TacticsWindow").GetComponent<TacticsWindow>();
        }
        
	}
    void OnEnable()
    {
        //selectEffect.SetActive(false);
    }
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetTacticsItem(ManualSkill _skill)
    {
        _ManualSkill = _skill;
        this.name = _skill.skillID.ToString();
        icon.name = _skill.skillID.ToString();
 //       icon.gameObject.GetComponent<TacticsDragObj>().skillID = _skill.skillID;//不可回拖
        icon.gameObject.GetComponent<TacticsCanDragBack>().skillID = _skill.skillID;//可以回拖
        icon.spriteName = _skill.skillID.ToString();
        //不满足使用等级的，变灰
        //if (CharacterRecorder.instance.level < _skill.skillLevel)
        if (CharacterRecorder.instance.lastGateID <= _skill.skillLevel)//不满足使用关卡的，变灰
        {
            //旧的变暗
           // SetColorToGray();
            icon.spriteName = _skill.skillID.ToString() + "1";
            icon.GetComponent<BoxCollider>().enabled = false;
        }
    }
    
    public void SetTacticsItem(ManualSkill _skill, List<string> myOwnTacticList)//没用这个
    {
        _ManualSkill = _skill;
        this.name = _skill.skillID.ToString();
        icon.name = _skill.skillID.ToString();
        icon.gameObject.GetComponent<TacticsDragObj>().skillID = _skill.skillID;
        icon.spriteName = _skill.skillID.ToString();
        //icon.MakePixelPerfect();
        for (int i = 0; i < myOwnTacticList.Count; i++)
        {
            if (_skill.skillID.ToString() == myOwnTacticList[i])
            {
                icon.color = Color.gray;
                icon.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
    public void SetColorToGray()
    {
        icon.color = Color.gray;
        icon.GetComponent<BoxCollider>().enabled = false;
    }
    public void SetColorBackToNormal()
    {
        //if (CharacterRecorder.instance.level < _ManualSkill.skillLevel)
        if (CharacterRecorder.instance.lastGateID <= _ManualSkill.skillLevel)//不满足使用关卡的，变灰
        {
            //旧的变暗
          //  SetColorToGray();
        }
        else
        {
            icon.color = Color.white;
            icon.GetComponent<BoxCollider>().size = new Vector3(100, 100, 0);
            icon.GetComponent<BoxCollider>().enabled = true;
        }
    }
    private void ClickThisTacticsItem(GameObject go)
    {
        if (go != null && go.name == _ManualSkill.skillID.ToString())
        {
            TacticsWindow.curManualSkill = _ManualSkill;
            _TacticsWindow.ResetCurManualSkillInfo(_ManualSkill);
            //selectEffect.SetActive(true);
        }
    }
}

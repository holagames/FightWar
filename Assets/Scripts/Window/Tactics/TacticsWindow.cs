using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TacticsWindow : MonoBehaviour
{
    public static ManualSkill curManualSkill;
    public UIAtlas commanAtlas;
    public UIAtlas skillAtlas;
    [HideInInspector]
    public List<GameObject> tacticsItemList = new List<GameObject>();
    [HideInInspector]
    public List<int> myOwnTacticList = new List<int>();
    //当前技能信息
    public UILabel labelSkillName;
    public UILabel labelSkillLv;
    public UILabel labelSkillDes;
    public UILabel labelSkillCD;
    //public UILabel labelSkillUpCost;
    public UISprite spriteIcon;
    //public GameObject buttonUpGrade;
    //左边
    public GameObject uiGride;
    public GameObject TacticsItem;
    //下方
    public List<UISprite> myOwnTacticsItemList = new List<UISprite>();
    private List<int> myOwnTacticsItemOpenLvList = new List<int>();
    public GameObject MySkill1;
    public GameObject MySkill2;
    public GameObject MySkill3;
    void OnEnable()
    {
        myOwnTacticsItemOpenLvList.Add(3);
        myOwnTacticsItemOpenLvList.Add(20);//关卡
        myOwnTacticsItemOpenLvList.Add(75);//关卡
        //NetworkHandler.instance.SendProcess("1031#");
    }
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.战术);
        UIManager.instance.UpdateSystems(UIManager.Systems.战术);

        // UIEventListener.Get(buttonUpGrade).onClick = ClickButtonUpGrade;
        TextTranslator.instance.MyManualSkillList = TextTranslator.instance.GetMyManualSkillList(1);
        SetTacticsWindow(TextTranslator.instance.MyManualSkillList);
        SetMyOwnTactics(CharacterRecorder.instance.ListManualSkillId);
        UIEventListener.Get(MySkill1).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.lastGateID-1 >=TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhanshu).Level)
            {
                UIManager.instance.OpenPromptWindow("请拖动装备您想要使用的战术", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(MySkill2).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.lastGateID - 1 >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhihuiSkill_2).Level)
            {
                UIManager.instance.OpenPromptWindow("请拖动装备您想要使用的战术", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(MySkill3).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.lastGateID - 1 >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhihuiSkill_3).Level)
            {
                UIManager.instance.OpenPromptWindow("请拖动装备您想要使用的战术", PromptWindow.PromptType.Hint, null, null);
            }
        };
    }

    public void SetMyOwnTactics(List<int> MyOwnTacticList)
    {
        myOwnTacticList = MyOwnTacticList;
        //SetTacticsWindow(TextTranslator.instance.ManualSkillList, MyOwnTacticList);
        for (int i = 0; i < myOwnTacticsItemList.Count; i++)
        {
            //if (CharacterRecorder.instance.level >= i + 1)
            if (i == 0 && CharacterRecorder.instance.lastGateID > 10003)//CharacterRecorder.instance.level >= myOwnTacticsItemOpenLvList[i]) //第一个等级限制
            {
                switch (MyOwnTacticList[i])
                {
                    case 0:
                        myOwnTacticsItemList[i].atlas = commanAtlas;
                        myOwnTacticsItemList[i].spriteName = "add";
                        break;
                    default:
                        //旧的不可拖回时
                        /*  myOwnTacticsItemList[i].atlas = skillAtlas;
                          myOwnTacticsItemList[i].spriteName = MyOwnTacticList[i].ToString(); */
                        Transform skillFrameTrans = myOwnTacticsItemList[i].transform.parent.transform;
                        /*   if (skillFrameTrans.childCount >= 3)
                           {
                               Debug.LogError("skillFrameTrans.childCount.." + skillFrameTrans.childCount);
                               DestroyImmediate(skillFrameTrans.GetChild(3).gameObject);
                           }*/
                        if (skillFrameTrans.FindChild(myOwnTacticList[i].ToString()) == null)
                        {
                            for (int j = 0; j < tacticsItemList.Count; j++)
                            {
                                if (tacticsItemList[j].name == myOwnTacticList[i].ToString())
                                {
                                    tacticsItemList[j].GetComponent<TacticsItem>().SetColorToGray();
                                    GameObject go = tacticsItemList[j].transform.FindChild(tacticsItemList[j].name).gameObject;
                                    GameObject clone = NGUITools.AddChild(skillFrameTrans.gameObject, go);
                                    clone.transform.localPosition = new Vector3(0, 0, 0);
                                    clone.transform.localScale = Vector3.one;
                                    clone.name = myOwnTacticList[i].ToString();
                                    clone.GetComponent<UISprite>().color = Color.white;
                                    clone.GetComponent<UISprite>().enabled = true;
                                    clone.GetComponent<UISprite>().MakePixelPerfect();
                                    clone.GetComponent<BoxCollider>().enabled = true;
                                    clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                                    clone.GetComponent<TacticsCanDragBack>().enabled = true;
                                    clone.GetComponent<TacticsCanDragBack>().cloneOnDrag = false;
                                    clone.tag = "lineup";
                                }
                            }

                        }
                        else
                        {
                        }
                        break;
                }
                myOwnTacticsItemList[i].transform.FindChild("Label").gameObject.SetActive(false);
            }
            else if (i == 0)
            {
                myOwnTacticsItemList[i].atlas = commanAtlas;
                myOwnTacticsItemList[i].spriteName = "ui4icon2";
                myOwnTacticsItemList[i].transform.FindChild("Label").gameObject.SetActive(true);
            }
            else
            {
                //后两个 关卡限制
                int gateContion = 10020;
                switch (i)
                {
                    case 1: gateContion = 10020; break;
                    case 2: gateContion = 10075; break;
                }
                if (CharacterRecorder.instance.lastGateID <= gateContion)
                {
                    myOwnTacticsItemList[i].atlas = commanAtlas;
                    myOwnTacticsItemList[i].spriteName = "ui4icon2";
                    myOwnTacticsItemList[i].transform.FindChild("Label").gameObject.SetActive(true);
                }
                else
                {
                    switch (MyOwnTacticList[i])
                    {
                        case 0:
                            myOwnTacticsItemList[i].atlas = commanAtlas;
                            myOwnTacticsItemList[i].spriteName = "add";
                            break;
                        default:
                            //旧的不可拖回时
                            /*  myOwnTacticsItemList[i].atlas = skillAtlas;
                              myOwnTacticsItemList[i].spriteName = MyOwnTacticList[i].ToString(); */
                            Transform skillFrameTrans = myOwnTacticsItemList[i].transform.parent.transform;
                            /*   if (skillFrameTrans.childCount >= 3)
                               {
                                   Debug.LogError("skillFrameTrans.childCount.." + skillFrameTrans.childCount);
                                   DestroyImmediate(skillFrameTrans.GetChild(3).gameObject);
                               }*/
                            if (skillFrameTrans.FindChild(myOwnTacticList[i].ToString()) == null)
                            {
                                for (int j = 0; j < tacticsItemList.Count; j++)
                                {
                                    if (tacticsItemList[j].name == myOwnTacticList[i].ToString())
                                    {
                                        tacticsItemList[j].GetComponent<TacticsItem>().SetColorToGray();
                                        GameObject go = tacticsItemList[j].transform.FindChild(tacticsItemList[j].name).gameObject;
                                        GameObject clone = NGUITools.AddChild(skillFrameTrans.gameObject, go);
                                        clone.transform.localPosition = new Vector3(0, 0, 0);
                                        clone.transform.localScale = Vector3.one;
                                        clone.name = myOwnTacticList[i].ToString();
                                        clone.GetComponent<UISprite>().color = Color.white;
                                        clone.GetComponent<UISprite>().enabled = true;
                                        clone.GetComponent<UISprite>().MakePixelPerfect();
                                        clone.GetComponent<BoxCollider>().enabled = true;
                                        clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                                        clone.GetComponent<TacticsCanDragBack>().enabled = true;
                                        clone.GetComponent<TacticsCanDragBack>().cloneOnDrag = false;
                                        clone.tag = "lineup";
                                    }
                                }

                            }
                            else
                            {
                            }
                            break;
                    }
                    myOwnTacticsItemList[i].transform.FindChild("Label").gameObject.SetActive(false);
                }
            }
            myOwnTacticsItemList[i].MakePixelPerfect();

        }
        for (int i = 0; i < MyOwnTacticList.Count; i++)
        {
            for (int j = 0; j < tacticsItemList.Count; j++)
            {
                if (tacticsItemList[j].GetComponent<TacticsItem>()._ManualSkill.skillID == MyOwnTacticList[i])
                {
                    tacticsItemList[j].GetComponent<TacticsItem>().SetColorToGray();
                }
            }
        }
    }
    public void SetTacticsWindow(List<ManualSkill> _ManualSkillList)
    {
        SortManualSkillListByLv(_ManualSkillList);
        if (_ManualSkillList != null && _ManualSkillList.Count != 0)
        {
            //默认第一个技能
            //curManualSkill = _ManualSkillList[0];
            //默认显示最新开启的技能
            curManualSkill = GetNewstManualSkill(_ManualSkillList);
            if (curManualSkill == null)
            {
                curManualSkill = _ManualSkillList[0];
            }
        }

        if (_ManualSkillList.Count <= 12 && uiGride.transform.parent.GetComponent<UIScrollView>() != null)
        {
            uiGride.transform.parent.GetComponent<UIScrollView>().enabled = false;
        }
        DestroyGride();
        for (int i = 0; i < _ManualSkillList.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, TacticsItem) as GameObject;
            obj.GetComponent<TacticsItem>().SetTacticsItem(_ManualSkillList[i]);
            tacticsItemList.Add(obj);
        }
        uiGride.GetComponent<UIGrid>().repositionNow = true;
        ResetCurManualSkillInfo(curManualSkill);
    }
    ManualSkill GetNewstManualSkill(List<ManualSkill> _ManualSkillList)
    {
        for (int i = _ManualSkillList.Count - 1; i >= 0; i--)
        {
            //if (CharacterRecorder.instance.level >= _ManualSkillList[i].skillLevel)
            if (CharacterRecorder.instance.lastGateID > _ManualSkillList[i].skillLevel)//满足使用关卡的
            {
                return _ManualSkillList[i];
            }
        }
        return null;
    }
    public void SetTacticsWindow(List<ManualSkill> _ManualSkillList, List<string> MyOwnTacticList)
    {
        if (_ManualSkillList != null && _ManualSkillList.Count != 0)
        {
            curManualSkill = _ManualSkillList[0];
        }

        if (_ManualSkillList.Count <= 12 && uiGride.transform.parent.GetComponent<UIScrollView>() != null)
        {
            uiGride.transform.parent.GetComponent<UIScrollView>().enabled = false;
        }
        DestroyGride();
        for (int i = 0; i < _ManualSkillList.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, TacticsItem) as GameObject;
            obj.GetComponent<TacticsItem>().SetTacticsItem(_ManualSkillList[i], MyOwnTacticList);
            tacticsItemList.Add(obj);
        }
        uiGride.GetComponent<UIGrid>().repositionNow = true;
        ResetCurManualSkillInfo(curManualSkill);
    }
    void SortManualSkillListByLv(List<ManualSkill> _ManualSkillList)
    {
        _ManualSkillList.Sort();
        /*  for (int i = 0; i < _ManualSkillList.Count; i++)
          {
              int 
              if (_ManualSkillList[i].skillLevel )
              {

              }
          }*/
    }
    public void ResetCurManualSkillInfo(ManualSkill _curManualSkill)
    {
        labelSkillName.text = _curManualSkill.skillName;
        string lvColorStr = "";
        //if (_curManualSkill.skillLevel <= CharacterRecorder.instance.level)
        if (CharacterRecorder.instance.lastGateID > _curManualSkill.skillLevel)//满足使用关卡的
        {
            lvColorStr = "[3ee817]";
        }
        else
        {
            lvColorStr = "[ff0000]";
        }
        labelSkillLv.color = Color.white;
        labelSkillLv.text = string.Format("[f8911d]通关第{0}[f8911d]关开启", lvColorStr + (_curManualSkill.skillLevel > 10000 ? _curManualSkill.skillLevel - 10000 : _curManualSkill.skillLevel));//"[f8911d]开启关卡：" + lvColorStr + (_curManualSkill.skillLevel > 10000 ? _curManualSkill.skillLevel - 10000 : _curManualSkill.skillLevel).ToString();
        labelSkillDes.text = _curManualSkill.description.Replace("%d", (_curManualSkill.skillVal1 / 100f * CharacterRecorder.instance.level).ToString() + "%");
        labelSkillDes.text = labelSkillDes.text.Replace("%s", (_curManualSkill.skillVal2 + _curManualSkill.skillVal1 * CharacterRecorder.instance.level).ToString());
        labelSkillDes.text = labelSkillDes.text.Replace("[$]", "");
        labelSkillCD.text = string.Format("冷却时间：{0}秒", _curManualSkill.coolDown);
        spriteIcon.spriteName = _curManualSkill.skillID.ToString();
        for (int i = 0; i < tacticsItemList.Count; i++)
        {
            GameObject _selectEffect = tacticsItemList[i].GetComponent<TacticsItem>().selectEffect;
            if (curManualSkill.skillID.ToString() == tacticsItemList[i].name)
            {
                _selectEffect.SetActive(true);
            }
            else
            {
                _selectEffect.SetActive(false);
            }
        }
    }
    void DestroyGride()
    {
        tacticsItemList.Clear();
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }
    /* private void ClickButtonUpGrade(GameObject go)
     {
         if(go != null)
         {

         }
     }*/
}

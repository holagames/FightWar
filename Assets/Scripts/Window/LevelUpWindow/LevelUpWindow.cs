using UnityEngine;
using System.Collections;

public class LevelUpWindow : MonoBehaviour 
{
    public UILabel Team_Old_Level;
    public UILabel Team_New_Level;
    public UILabel Physical_Old_Number;
    public UILabel Physical_New_Number;
    public UILabel PhysicalLimit_Old_Number;
    public UILabel PhysicalLimit_New_Number;
    public UILabel Hero_Old_LevelLimit;
    public UILabel Hero_New_LevelLimit;

    public UILabel TeamOldLevel;
    public UILabel TeamNewLevel;
    public UILabel PhysicalOldNumber;
    public UILabel PhysicalNewNumber;
    public UILabel PhysicalLimitOldNumber;
    public UILabel PhysicalLimitNewNumber;
    //public UILabel HeroOldLevelLimit;
    //public UILabel HeroNewLevelLimit;

    public GameObject ObjHaveWillItems;
    public GameObject ObjNoneWillItems;
    public GameObject uiGride;
    public GameObject levelUpItem;
    public GameObject Effect;
    public GameObject maskButton;
    private int lastGateId;//旧的lastGateId
	// Use this for initialization
	void Start () 
    {
        if (SceneTransformer.instance.NowGateID == 10001)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_712);
        }
        else if(SceneTransformer.instance.NowGateID == 10002)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1009);
        }
        else if (SceneTransformer.instance.NowGateID == 10003)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1207);
        }
        else if (SceneTransformer.instance.NowGateID == 10004)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1309);
        }
        else if (SceneTransformer.instance.NowGateID == 10005)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1610);
        }

        UIEventListener.Get(maskButton).onClick = delegate(GameObject go)
        {
            ClosePlane();
        };

        GameObject _mapwindow = GameObject.Find("MapCon");
        if (_mapwindow != null)
        {
            _mapwindow.GetComponent<MapWindow>().UpdateLeiDaState(true);
        }
        maskButton.GetComponent<BoxCollider>().enabled = false;
        Invoke("DelayShowMaskBox",2.8f);
	}
    void DelayShowMaskBox()
    {
        maskButton.GetComponent<BoxCollider>().enabled = true;
    }
    public void SetInfo(int lestGateID,string _NewLevel,string _PhysicalNewNumber,string _PhysicalLimitNewNumber)
    {
        this.lastGateId = lestGateID;
        this.gameObject.layer = 11;
        foreach (Component c in this.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }
        //Debug.LogError(CharacterRecorder.instance.level.ToString() + "  ..stamina..  " + CharacterRecorder.instance.stamina.ToString() + "   ..staminaCap..   " + CharacterRecorder.instance.staminaCap.ToString());
        //Debug.LogError("lastGateId.." + lestGateID + "...nowGateId...." + SceneTransformer.instance.NowGateID);
        //   BetterList<NewGuide> newGuideWillOpenList = TextTranslator.instance.FindNewGuideWillOpenListByCurLevel(int.Parse(_NewLevel));
        BetterList<NewGuide> newGuideWillOpenList = TextTranslator.instance.FindNewGuideWillOpenListByCurLevel(lestGateID);
        TextTranslator.instance.newGuideWillOpenList = newGuideWillOpenList;

        if (TextTranslator.instance.newGuideWillOpenList == null)
        {
            ObjNoneWillItems.SetActive(true);
            ObjHaveWillItems.SetActive(false);
            SetInfoNoneWillItems(_NewLevel, _PhysicalNewNumber, _PhysicalLimitNewNumber);
            return;
        }
        else if (TextTranslator.instance.newGuideWillOpenList.size == 0)
        {
            ObjNoneWillItems.SetActive(true);
            ObjHaveWillItems.SetActive(false);
            SetInfoNoneWillItems(_NewLevel, _PhysicalNewNumber, _PhysicalLimitNewNumber);
            return;
        }
        //Debug.LogError("size........." + TextTranslator.instance.newGuideWillOpenList.size);
        ObjNoneWillItems.SetActive(false);
        ObjHaveWillItems.SetActive(true);
        Team_New_Level.gameObject.SetActive(false);
        Physical_New_Number.gameObject.SetActive(false);
        PhysicalLimit_New_Number.gameObject.SetActive(false);
        Team_Old_Level.text = CharacterRecorder.instance.level.ToString();
        Team_New_Level.text = _NewLevel;

        if (CharacterRecorder.instance.staminaOld != 0)
        {
            Physical_Old_Number.text = CharacterRecorder.instance.staminaOld.ToString();
        }
        else
        {
            Physical_Old_Number.text = CharacterRecorder.instance.stamina.ToString();
        }
        Physical_New_Number.text = _PhysicalNewNumber;
        CharacterRecorder.instance.stamina = int.Parse(_PhysicalNewNumber);

        PhysicalLimit_Old_Number.text = CharacterRecorder.instance.staminaCap.ToString();
        PhysicalLimit_New_Number.text = _PhysicalLimitNewNumber;
        if (int.Parse(_PhysicalLimitNewNumber) < CharacterRecorder.instance.staminaCap)
        {
            PhysicalLimit_New_Number.text = CharacterRecorder.instance.staminaCap.ToString(); 
        }
        CharacterRecorder.instance.staminaCap = int.Parse(_PhysicalLimitNewNumber);

        Hero_Old_LevelLimit.text = CharacterRecorder.instance.level.ToString();
        Hero_New_LevelLimit.text = _NewLevel;
        CharacterRecorder.instance.level = int.Parse(_NewLevel);
        
        //CreatLevelUpWillOpenItems();
        Invoke("DelayShowNewLabels",1.0f);
        Invoke("CreatLevelUpWillOpenItems", 1.5f);
        
       // Destroy(Effect, 1.1f);
        //Invoke("ClosePlane", 3f);
    }
    void SetInfoNoneWillItems(string _NewLevel, string _PhysicalNewNumber, string _PhysicalLimitNewNumber)
    {
        TeamNewLevel.gameObject.SetActive(false);
        PhysicalNewNumber.gameObject.SetActive(false);
        PhysicalLimitNewNumber.gameObject.SetActive(false);

        TeamOldLevel.text = CharacterRecorder.instance.level.ToString();
        TeamNewLevel.text = _NewLevel;

        if (CharacterRecorder.instance.staminaOld != 0)
        {
            PhysicalOldNumber.text = CharacterRecorder.instance.staminaOld.ToString();
        }
        else
        {
            PhysicalOldNumber.text = CharacterRecorder.instance.stamina.ToString();
        }
        PhysicalNewNumber.text = _PhysicalNewNumber;
        CharacterRecorder.instance.stamina = int.Parse(_PhysicalNewNumber);

        PhysicalLimitOldNumber.text = CharacterRecorder.instance.staminaCap.ToString();
        PhysicalLimitNewNumber.text = _PhysicalLimitNewNumber;
        CharacterRecorder.instance.staminaCap = int.Parse(_PhysicalLimitNewNumber);

        //HeroOldLevelLimit.text = CharacterRecorder.instance.level.ToString();
        //HeroNewLevelLimit.text = _NewLevel;
        CharacterRecorder.instance.level = int.Parse(_NewLevel);

        Invoke("DelayShowNewLabelsNoneItems", 1.0f);
    }
    void CreatLevelUpWillOpenItems()
    {
        //BetterList<NewGuide> newGuideWillOpenList = TextTranslator.instance.FindNewGuideWillOpenListByCurLevel(CharacterRecorder.instance.level);
        //TextTranslator.instance.newGuideWillOpenList = newGuideWillOpenList;
       /* for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }*/
        CleraUIGride();
        for (int i = 0; i < TextTranslator.instance.newGuideWillOpenList.size; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, levelUpItem);
            obj.GetComponent<LevelUpItem>().SetLevelUpItem(lastGateId,TextTranslator.instance.newGuideWillOpenList[i]);
        }
        uiGride.GetComponent<UIGrid>().repositionNow = true;
    }
    void CleraUIGride()
    {
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }
    void DelayShowNewLabels()
    {
        Team_New_Level.gameObject.SetActive(true);
        Physical_New_Number.gameObject.SetActive(true);
        PhysicalLimit_New_Number.gameObject.SetActive(true);
    }
    void DelayShowNewLabelsNoneItems()
    {
        TeamNewLevel.gameObject.SetActive(true);
        PhysicalNewNumber.gameObject.SetActive(true);
        PhysicalLimitNewNumber.gameObject.SetActive(true);
    }
    void ClosePlane()
    {
        UIManager.instance.DestroyPanel("LevelUpWindow");
    }

}

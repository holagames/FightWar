using UnityEngine;
using System.Collections;

public class ItemIconWithName: MonoBehaviour 
{
    [SerializeField]
    private UIAtlas avatarAtlas;
    [SerializeField]
    private UIAtlas itemAtlas;
    //---------
    [SerializeField]
    private UISprite frame;
    [SerializeField]
    private UISprite icon;
    [SerializeField]
    private UISprite junXianSprite;
    [SerializeField]
    private UILabel junXuanLabel;
    [SerializeField]
    private UILabel levelLabel;
    [SerializeField]
    private UILabel nameLabel;
    [SerializeField]
    private GameObject gride;
    [SerializeField]
    private UISprite pinJieSprite;
    private int addNum = 0;
    private string nameLabelColor = "";
	// Use this for initialization

	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    //本账号战队
    public void SetItemIconWithName(bool isFateOpen,int _ItemOrRoleID)
    {
       
       Item _oneItem = TextTranslator.instance.GetItemByID(_ItemOrRoleID);
       if (_ItemOrRoleID > 60000)
       {
         //  Debug.LogError("_ItemOrRoleID.." + _ItemOrRoleID);
           SetHeroItem(_ItemOrRoleID);
       }
       else
       {
           SetItem(isFateOpen,_ItemOrRoleID);
       }
       if (GameObject.Find("CardDetailInfoWindow") != null || GameObject.Find("LegionMemberItemDetail") != null)
       {
           nameLabel.text = "";
       }
    }
    //其他账号战队
    public void SetItemIconWithName(RoleInfoOfTargetPlayer _RoleInfoOfTargetPlayer)
    {
        icon.atlas = avatarAtlas;

        this.name = _RoleInfoOfTargetPlayer.roleId.ToString();
        icon.spriteName = _RoleInfoOfTargetPlayer.roleId.ToString();
        levelLabel.text = _RoleInfoOfTargetPlayer.roleLevel.ToString();
        SetRankInfo(_RoleInfoOfTargetPlayer.roleJunXian, junXianSprite, junXuanLabel);
        //   addNum = TextTranslator.instance.SetHeroNameColor(nameLabel, frame, pinJieSprite, _oneHero.name, _oneHero.classNumber);
        addNum = TextTranslator.instance.SetHeroNameColor(frame, pinJieSprite, _RoleInfoOfTargetPlayer.color);
        for (int i = 0; i < addNum; i++)
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        _UIGrid.animateSmoothly = true;
        nameLabel.text = "";
    }

#region//英雄数据
    private void SetHeroItem(int _ItemOrRoleID)
    {
        Hero _oneHero = CharacterRecorder.instance.GetHeroByRoleID(_ItemOrRoleID);

        icon.atlas = avatarAtlas;
        if (_oneHero != null)
        {
            this.name = _oneHero.cardID.ToString();
            icon.name = _oneHero.cardID.ToString();
            icon.spriteName = _oneHero.cardID.ToString();
            levelLabel.text = _oneHero.level.ToString();
            SetRankInfo(_oneHero.rank, junXianSprite, junXuanLabel);
            //旧的
          /*  GradeColor(_oneHero.classNumber);
            nameLabel.text = nameLabelColor + _oneHero.name;*/
            addNum = TextTranslator.instance.SetHeroNameColor(nameLabel, frame, pinJieSprite,_oneHero.name, _oneHero.classNumber);
            for (int i = 0; i < addNum; i++)
            {
                GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
                obj.SetActive(true);
                //obj.GetComponent<UISprite>().spriteName = string.Format("zbkuang{0}", _oneHero.classNumber);
            }
            UIGrid _UIGrid = gride.GetComponent<UIGrid>();
            _UIGrid.sorting = UIGrid.Sorting.Horizontal;
            _UIGrid.pivot = UIWidget.Pivot.Center;
            _UIGrid.animateSmoothly = true;
        }
        else
        {
            Debug.Log("缘分物品Id:" + _ItemOrRoleID);
            HeroInfo _HeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(_ItemOrRoleID);
            this.name = _ItemOrRoleID.ToString();
            icon.name = _ItemOrRoleID.ToString() + 1;
            icon.spriteName = _ItemOrRoleID.ToString() + 1;
            frame.spriteName = "yxdi0";
            levelLabel.text ="";
            if (GameObject.Find("FateWindow") != null)
            {
                nameLabel.text = _HeroInfo.heroName;
            }
            else
            {
                nameLabel.text = "";
            }
            junXuanLabel.text = "";
            junXianSprite.spriteName = "";
        }
    }
    private void SetHeroItem(Hero _oneRoleInfo)
    {
        this.name = _oneRoleInfo.cardID.ToString();//_oneRoleInfo.characterRoleID.ToString();
        icon.atlas = avatarAtlas;
        icon.name = _oneRoleInfo.cardID.ToString();//_oneRoleInfo.characterRoleID.ToString();
        icon.spriteName = _oneRoleInfo.cardID.ToString();
        levelLabel.text = _oneRoleInfo.level.ToString();
        nameLabel.text = _oneRoleInfo.name;
        //Debug.LogError(_oneRoleInfo.rank);//军衔
        //junXianSprite.spriteName = string.Format("rank0{0}", _oneRoleInfo.rank + 1);
        SetRankInfo(_oneRoleInfo.rank, junXianSprite, junXuanLabel);
        GradeColor(_oneRoleInfo.classNumber);//旧的
        


 /*       frame.spriteName = string.Format("yxdi{0}", _oneRoleInfo.classNumber - 1); //"yxdi0";//品质
        if (_oneRoleInfo.classNumber > 3)
        {
            frame.spriteName = "yxdi" + (_oneRoleInfo.classNumber - 2).ToString();
        }*/
        for (int i = 0; i < addNum; i++)
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
            //obj.GetComponent<UISprite>().spriteName = string.Format("zbkuang{0}", _oneRoleInfo.classNumber);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        //_UIGrid.Reposition(); 
        _UIGrid.animateSmoothly = true;
    }
    //设置军衔
    void SetRankInfo(int _rank, UISprite _UISprite, UILabel _myUILabel)
    {
       // _UISprite.spriteName = string.Format("rank{0}", _rank.ToString("00"));
        _UISprite.transform.GetChild(_rank - 1).gameObject.SetActive(true);
        switch (_rank)
        {
            case 1:
                _myUILabel.text = "下士";
                break;
            case 2:
                _myUILabel.text = "中士";
                break;
            case 3:
                _myUILabel.text = "上士";
                break;
            case 4:
                _myUILabel.text = "少尉";
                break;
            case 5:
                _myUILabel.text = "中尉";
                break;
            case 6:
                _myUILabel.text = "上尉";
                break;
            case 7:
                _myUILabel.text = "少校";
                break;
            case 8:
                _myUILabel.text = "中校";
                break;
            case 9:
                _myUILabel.text = "上校";
                break;
            case 10:
                _myUILabel.text = "少将";
                break;
            case 11:
                _myUILabel.text = "中将";
                break;
            case 12:
                _myUILabel.text = "上将";
                break;
            default:
                break;
        }
    }
    void GradeColor(int Num)
    {
        switch (Num)
        {
            case 1:
                frame.spriteName = "yxdi0";
                break;
            case 2:
                frame.spriteName = "yxdi1";
                nameLabelColor =  "[3ee817]";
                break;
            case 3:
                frame.spriteName = "yxdi1";
                addNum = 1;
                pinJieSprite.spriteName = "zbkuang2";
                nameLabelColor = "[3ee817]";
                break;
            case 4:
                frame.spriteName = "yxdi2";
                nameLabelColor = "[249bd2]";
                break;
            case 5:
                frame.spriteName = "yxdi2";
                addNum = 1;
                pinJieSprite.spriteName = "zbkuang3";
                nameLabelColor = "[249bd2]";
                break;
            case 6:
                frame.spriteName = "yxdi2";
                addNum = 2;
                pinJieSprite.spriteName = "zbkuang3";
                nameLabelColor = "[249bd2]";
                break;
            case 7:
                frame.spriteName = "yxdi3";
                nameLabelColor = "[bb44ff]";
                break;
            case 8:
                frame.spriteName = "yxdi3";
                addNum = 1;
                pinJieSprite.spriteName = "zbkuang4";
                nameLabelColor = "[bb44ff]";
                break;
            case 9:
                frame.spriteName = "yxdi3";
                addNum = 2;
                pinJieSprite.spriteName = "zbkuang4";
                nameLabelColor = "[bb44ff]";
                break;
            case 10:
                frame.spriteName = "yxdi3";
                addNum = 3;
                pinJieSprite.spriteName = "zbkuang4";
                nameLabelColor = "[bb44ff]";
                break;
            case 11:
                frame.spriteName = "yxdi4";
                nameLabelColor = "[ff8c04]";
                break;
            case 12:
                frame.spriteName = "yxdi4";
                addNum = 1;
                pinJieSprite.spriteName = "zbkuang5";
                nameLabelColor = "[ff8c04]";
                break;
            default:
                break;
        }
    }
#endregion

    #region 饰品处理

    private void SetItem(bool isFateOpen,int codeId)
    {
        string frameName = "";
        Item _ItemInBag =  TextTranslator.instance.GetItemByID(codeId);
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(codeId);
        //if (_ItemInBag != null)
        if (isFateOpen)
        {
            frameName = string.Format("Grade{0}", mItemInfo.itemGrade);
            frame.spriteName = frameName;
            icon.atlas = itemAtlas;
            icon.spriteName = codeId.ToString();
            levelLabel.text = "";//_OneEquipInfo.equipLevel.ToString();
            junXuanLabel.text = "";
            junXianSprite.spriteName = "";

            SetNameLabelColor(mItemInfo.itemGrade);
            nameLabel.text = nameLabelColor + mItemInfo.itemName;
        }
        else
        {
            
            frameName = string.Format("Grade{0}", mItemInfo.itemGrade);
            frame.spriteName = frameName;
            icon.atlas = itemAtlas;
            icon.spriteName = string.Format("{0}1", codeId);//codeId.ToString();
            levelLabel.text = "";//_OneEquipInfo.equipLevel.ToString();
            junXuanLabel.text = "";
            junXianSprite.spriteName = "";

            nameLabel.text = nameLabelColor + mItemInfo.itemName;
        }
        
    }

    public void SetEquipItemInStrengh(Hero mHero, int _position)
    {
        DestroyGride();
        int mEquipCode = mHero.equipList[_position - 1].equipCode;
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(mEquipCode);

        for (int i = 0; i < GetNumAdd(mItemInfo.itemGrade, mHero.equipList[_position - 1].equipColorNum); i++)
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
            obj.GetComponent<UISprite>().spriteName = string.Format("zbkuang{0}", mItemInfo.itemGrade);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        //_UIGrid.Reposition(); 
        _UIGrid.animateSmoothly = true;
    }
    void DestroyGride()
    {
        for (int i = gride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gride.transform.GetChild(i).gameObject);
        }
    }
    int GetNumAdd(int _itemGrade, int _colorNum)
    {
        int addNum = 0;
        switch (_itemGrade)
        {
            case 1:
                switch (_colorNum)
                {
                    case 0: addNum = 0; break;
                    case 1: addNum = 0; break;
                    case 2: addNum = 1; break;
                }
                break;
            case 2:
                switch (_colorNum)
                {
                    case 3: addNum = 0; break;
                    case 4: addNum = 1; break;
                    case 5: addNum = 2; break;
                }
                break;
            case 3:
                switch (_colorNum)
                {
                    case 6: addNum = 0; break;
                    case 7: addNum = 1; break;
                    case 8: addNum = 2; break;
                    case 9: addNum = 3; break;
                }
                break;
            case 4:
                switch (_colorNum)
                {
                    case 10: addNum = 0; break;
                    case 11: addNum = 1; break;
                    case 12: addNum = 2; break;
                    case 13: addNum = 3; break;
                    case 14: addNum = 4; break;
                }
                break;
            case 5:
                switch (_colorNum)
                {
                    case 15: addNum = 0; break;
                    case 16: addNum = 1; break;
                    case 17: addNum = 2; break;
                    case 18: addNum = 3; break;
                    case 19: addNum = 4; break;
                    case 20: addNum = 5; break;
                }
                break;
            case 6:
                switch (_colorNum)
                {
                    case 21: addNum = 0; break;
                    case 22: addNum = 1; break;
                }
                break;
        }
        return addNum;
    }
    void SetNameLabelColor(int _itemGrade)
    {
        switch (_itemGrade)
        {
            case 1: nameLabelColor = "[ffffff]"; break;
            case 2: nameLabelColor = "[3ee817]"; break;
            case 3: nameLabelColor = "[8ccef2]"; break;
            case 4: nameLabelColor = "[bb44ff]"; break;
            case 5: nameLabelColor = "[ff8c04]"; break;
            case 6: nameLabelColor = "[fb2d50]"; break;
        }
    }
#endregion
}

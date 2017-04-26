using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AboutHeroInfoWindow : MonoBehaviour
{
    public UILabel heroName;
    /// <summary>
    /// 角色的职业
    /// </summary>
    public UISprite heroTypeIcon;
    public GameObject heroRankHorizontal;
    public UILabel heroFocre;
    public GameObject CombinButton;
    public GameObject RebirthButton;

    public GameObject heroGrid;
    public GameObject heroItemPrefab;

    public GameObject ExitBtn;
    /// <summary>
    /// 装备的集合
    /// </summary>
    public List<GameObject> ListEquip = new List<GameObject>();

    private Hero curHero = null;
    private HeroInfo _heroInfo = null;
    private int CharacterRoleID = -1;
    private List<HeroInfo> myHeroInfoList = new List<HeroInfo>();
    private List<Hero> myHeroList = new List<Hero>();
    private List<GameObject> gridList = new List<GameObject>();

    private RoleInfoOfTargetPlayer mTargetHero = null;

    private bool isOnPressed = false;

    void Awake()
    {
        for (int i = 0; i < heroGrid.transform.childCount; i++)
        {
            gridList.Add(heroGrid.transform.GetChild(i).gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(ExitBtn).onClick == null)
        {
            UIEventListener.Get(ExitBtn).onClick += delegate(GameObject go)
            {
                PictureCreater.instance.DestroyAllComponent();
                DestroyImmediate(this.gameObject);
            };
        }
        CreateHeroLest();
    }

    public void CreateHeroLest()
    {
        AddMyheroList();
        FightForcePaixu();
        DestroyGridList();
        for (int i = 0; i < TextTranslator.instance.targetPlayerInfo.roleList.size; i++)
        {
            RoleInfoOfTargetPlayer targetPlayer = TextTranslator.instance.targetPlayerInfo.roleList[i];

            Hero hero = CharacterRecorder.instance.GetHeroByRoleID(targetPlayer.roleId);
            GameObject go = NGUITools.AddChild(heroGrid, heroItemPrefab);
            go.SetActive(true);
            go.GetComponent<ItemIconWithName>().SetItemIconWithName(TextTranslator.instance.targetPlayerInfo.roleList[i]);

            go.name = TextTranslator.instance.targetPlayerInfo.roleList[i].characterId.ToString();

            if (UIEventListener.Get(go).onClick == null)
            {
                UIEventListener.Get(go).onClick += delegate(GameObject go1)
                {
                    if (CharacterRecorder.instance.AboutHeroInfoId.ToString() == go1.name)
                    {
                        return;
                    }
                    CharacterRecorder.instance.AboutHeroInfoId = int.Parse(go1.name);
                    NetworkHandler.instance.SendProcess(string.Format("1029#{0};", CharacterRecorder.instance.AboutHeroInfoId));
                };
            }
            if (go.GetComponent<UIToggle>() == null)
            {
                go.AddComponent<UIToggle>();
            }
            go.GetComponent<UIToggle>().group = 2;
            UIToggle checkStatus = go.GetComponent<UIToggle>();
            //给控件绑定选择及取消选择事件
            EventDelegate.Add(checkStatus.onChange, () =>
            {
                if (UIToggle.current.value)
                {
                    go.transform.Find("Select").gameObject.SetActive(true);
                }
                else
                {
                    go.transform.Find("Select").gameObject.SetActive(false);
                }
            });
            gridList.Add(go);
        }
        heroGrid.GetComponent<UIGrid>().Reposition();
        for (int i = 0; i < gridList.Count; i++)
        {
            if (gridList[i].name == CharacterRecorder.instance.AboutHeroInfoId.ToString())
            {
                gridList[i].GetComponent<UIToggle>().value = true;
            }
        }


        //初始化进入
        SendInitHero(CharacterRecorder.instance.AboutHeroInfoId);
    }

    void SendInitHero(int roleId)
    {
        // Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
        NetworkHandler.instance.SendProcess(string.Format("1029#{0};", roleId));
    }

    void DestroyGridList()
    {
        for (int i = 0; i < gridList.Count; i++)
        {
            DestroyImmediate(gridList[i]);
        }
        gridList.Clear();
        heroGrid.GetComponent<UIGrid>().Reposition();
    }
    /// <summary>
    /// 添加属于自己的角色到myHeroList集合里面
    /// </summary>
    void AddMyheroList()
    {
        myHeroInfoList.Clear();
        foreach (var h in TextTranslator.instance.heroInfoList)
        {
            if (h.heroID < 65000)
            {
                foreach (var item in CharacterRecorder.instance.ownedHeroList)
                {
                    if (h.heroID == item.cardID)
                    {
                        myHeroInfoList.Add(h);
                        break;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 角色按照战斗力排序
    /// </summary>
    void FightForcePaixu()
    {
        myHeroInfoList.Sort(delegate(HeroInfo x, HeroInfo y)
        {
            if (x != null && y != null)
            {
                return CharacterRecorder.instance.GetHeroByRoleID(y.heroID).force.CompareTo(CharacterRecorder.instance.GetHeroByRoleID(x.heroID).force);
            }
            else if (x != null)
            {
                return 0.CompareTo(CharacterRecorder.instance.GetHeroByRoleID(x.heroID) == null ? 0 : CharacterRecorder.instance.GetHeroByRoleID(x.heroID).force);
            }
            else
            {
                return (CharacterRecorder.instance.GetHeroByRoleID(y.heroID) == null ? 0 : CharacterRecorder.instance.GetHeroByRoleID(y.heroID).force).CompareTo(0);
            }

        });
    }
    /// <summary>
    /// 设置角色信息
    /// </summary>
    /// <param name="heroInfo">角色信息</param>
    /// <param name="equip1">装备1</param>
    /// <param name="equip2">装备2</param>
    /// <param name="equip3">装备3</param>
    /// <param name="equip4">装备4</param>
    /// <param name="equip5">装备5</param>
    /// <param name="equip6">装备6</param>
    public void SetHeroInfo_1029(string heroInfo, string equip1, string equip2, string equip3, string equip4, string equip5, string equip6)
    {
        string[] heroData0 = heroInfo.Split('!');
        string[] heroData = heroData0[0].Split('$');
        mTargetHero = null;
        foreach (var item in TextTranslator.instance.targetPlayerInfo.roleList)
        {
            if (int.Parse(heroData[0]) == item.roleId)
            {
                mTargetHero = item;
            }
        }
        if (mTargetHero == null)
        {
            UIManager.instance.OpenPromptWindow("角色信息错误！", PromptWindow.PromptType.Hint, null, null);
            PictureCreater.instance.DestroyAllComponent();
            DestroyImmediate(this.gameObject);
            return;
        }
        //Debug.LogError("h.characterRoleID:::" + mTargetHero.characterId);
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(heroData[0]));
        //HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(heroData[0]));
        _heroInfo = hinfo;

        //设置角色的名字
        TextTranslator.instance.SetHeroNameColor(heroName.GetComponent<UILabel>(), _heroInfo.heroName, int.Parse(heroData[3]));
        //设置军衔
        heroRankHorizontal.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(int.Parse(heroData[2]));
        //设置角色的类型
        SetHeroType(_heroInfo.heroCarrerType);
        heroTypeIcon.transform.localPosition = new Vector3(-1.0f * heroName.GetComponent<UILabel>().localSize.x / 2, -2.0f, 0);
        //设置战力
        heroFocre.text = heroData[5];
        //生成3D角色
        StartCoroutine(CreatModel(_heroInfo, int.Parse(heroData[6])));
        //设置装备信息
        //Debug.LogError("ListEquip::" + ListEquip.Count);

        string[] equipData = equip1.Split('$');
        SetEquip(0, int.Parse(equipData[0]), int.Parse(equipData[1]), int.Parse(equipData[2]), int.Parse(equipData[3]));

        string[] equipData1 = equip2.Split('$');
        SetEquip(1, int.Parse(equipData1[0]), int.Parse(equipData1[1]), int.Parse(equipData1[2]), int.Parse(equipData1[3]));

        string[] equipData2 = equip3.Split('$');
        SetEquip(2, int.Parse(equipData2[0]), int.Parse(equipData2[1]), int.Parse(equipData2[2]), int.Parse(equipData2[3]));

        string[] equipData3 = equip4.Split('$');
        SetEquip(3, int.Parse(equipData3[0]), int.Parse(equipData3[1]), int.Parse(equipData3[2]), int.Parse(equipData3[3]));

        string[] equipData4 = equip5.Split('$');
        SetEquip(4, int.Parse(equipData4[0]), int.Parse(equipData4[1]), int.Parse(equipData4[2]), int.Parse(equipData4[3]));

        string[] equipData5 = equip6.Split('$');
        SetEquip(5, int.Parse(equipData5[0]), int.Parse(equipData5[1]), int.Parse(equipData5[2]), int.Parse(equipData5[3]));
    }

    public void SetEquip(int index, int id, int jinglian, int level, int classNum)
    {
        //Debug.LogError("id:::::" + id);
        GameObject go = ListEquip[index];
        go.transform.Find("LabelEquipLevel").gameObject.GetComponent<UILabel>().text = level.ToString();
        go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = id.ToString();

        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(id);
        go.GetComponent<UISprite>().spriteName = string.Format("Grade{0}", mItemInfo.itemGrade);
        if (index < 4)
        {

            GameObject uiGrid = go.transform.Find("Grid").gameObject;
            GameObject pin = go.transform.Find("SpritePinJie").gameObject;
            int addNum = GetNumAdd(mItemInfo.itemGrade, classNum, pin);
            List<GameObject> grid = new List<GameObject>();
            for (int i = 0; i < uiGrid.transform.childCount; i++)
            {
                grid.Add(uiGrid.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < grid.Count; i++)
            {
                DestroyImmediate(grid[i]);
            }
            for (int i = 0; i < addNum; i++)
            {
                GameObject pin0 = NGUITools.AddChild(uiGrid, pin);
                pin0.SetActive(true);

            }
            uiGrid.GetComponent<UIGrid>().Reposition();
        }
        else
        {

        }
    }
    int GetNumAdd(int _itemGrade, int _colorNum, GameObject go)
    {
        int addNum = 0;
        switch (_itemGrade)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "zbkuang1";
                switch (_colorNum)
                {       
                    case 0: addNum = 0; break;
                    case 1: addNum = 0; break;
                    case 2: addNum = 1; break;
                }
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "zbkuang2";
                switch (_colorNum)
                {
                    case 3: addNum = 0; break;
                    case 4: addNum = 1; break;
                    case 5: addNum = 2; break;
                }
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "zbkuang3";
                switch (_colorNum)
                {
                    case 6: addNum = 0; break;
                    case 7: addNum = 1; break;
                    case 8: addNum = 2; break;
                    case 9: addNum = 3; break;
                }
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "zbkuang4";
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
                go.GetComponent<UISprite>().spriteName = "zbkuang5";
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
                go.GetComponent<UISprite>().spriteName = "zbkuang6";
                switch (_colorNum)
                {
                    case 21: addNum = 0; break;
                    case 22: addNum = 1; break;
                }
                break;
        }
        return addNum;
    }
    /// <summary>
    /// 设置角色的装备信息
    /// </summary>
    /// <param name="_OneEquipInfo"></param>
    public void SetEquip(Hero.EquipInfo _OneEquipInfo)
    {
        GameObject go = ListEquip[_OneEquipInfo.equipPosition - 1];
        if (_OneEquipInfo.equipPosition < 5)
        {
            go.GetComponent<EquipItemInStreng>().SetEquipItemInStrengh(curHero, _OneEquipInfo.equipPosition);
            //SetEquipStrenghRedPoint(_OneEquipInfo, go);
        }

        //StartCoroutine(CloseEffect(go, "WearEquipEffect"));
        if (_OneEquipInfo.equipCode != 0)
        {
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_OneEquipInfo.equipCode);
            go.GetComponent<UISprite>().spriteName = string.Format("Grade{0}", mItemInfo.itemGrade);
            go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = _OneEquipInfo.equipCode.ToString();
            go.transform.Find("LabelEquipLevel").gameObject.GetComponent<UILabel>().text = _OneEquipInfo.equipLevel.ToString();
        }
        else
        {
            //Debug.Log("装备有空格!!!!!!!!!!!!!!");
            go.GetComponent<UISprite>().spriteName = string.Format("Grade{0}", 1);
            go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = "add";
            go.transform.Find("LabelEquipLevel").gameObject.GetComponent<UILabel>().text = "";
        }
    }
    void OnPress(bool isPressed)
    {
        this.isOnPressed = isPressed;
    }
    void OnDrag(Vector2 delta)
    {
        if (this.isOnPressed)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.FindChild("Role").Rotate(new Vector3(0, 1, 0) * delta.x * -0.85f);
        }
    }
    /// <summary>
    /// 生成3D角色
    /// </summary>
    /// <param name="h"></param>
    /// <returns></returns>
    IEnumerator CreatModel(HeroInfo h, int weaponClass)
    {
        PictureCreater.instance.DestroyAllComponent();
        //                                                                                                                                                           1002, weaponClass, 1, weaponClass, 1, 1, 0);
        int i = PictureCreater.instance.CreateRole(h.heroID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1002, weaponClass, 1, weaponClass, 1, 1, 0, "");
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.parent = gameObject.transform;
        //角色旋转、播动画
        //GameObject _roleWindow = GameObject.Find("AboutHeroInfoWindow");
        //if (_roleWindow != null)
        //{
        //    _roleWindow.GetComponent<PlayAniOrRotation>().ResetTargetRole(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.FindChild("Role").gameObject, h);
        //}
        int modelPositionY = -400;//-1370 飞机的话
        if (h.heroID == 60201)//|| h.cardID == 60200
        {
            modelPositionY = -680;
        }
        if ((Screen.width * 1f / Screen.height) > 1.7f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(7540, modelPositionY, 13339);
        }
        else if ((Screen.width * 1f / Screen.height) < 1.4f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(7540, modelPositionY, 13339);
        }
        else
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(7540, modelPositionY, 13339);
        }
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(500, 500, 500) * TextTranslator.instance.GetHeroInfoByHeroID(PictureCreater.instance.ListRolePicture[0].RoleID).heroScale;
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.Rotate(new Vector3(0, 65, 0));
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(400, 400, 400);
        PictureCreater.instance.ListRolePicture[0].RolePictureObject.transform.Rotate(new Vector3(0, 65, 0));
        yield return new WaitForSeconds(0.1f);
    }

    /// <summary>
    /// 根据角色ID 获取角色
    /// </summary>
    /// <param name="heroId"></param>
    /// <returns></returns>
    Hero GetHero(int heroId)
    {
        foreach (var item in CharacterRecorder.instance.ownedHeroList)
        {
            if (item.cardID == heroId)
            {
                return item;
            }
        }
        return null;
    }
    /// <summary>
    /// 设置英雄职业
    /// </summary>
    /// <param name="_herotype"></param>
    void SetHeroType(int _herotype)
    {
        if (_herotype == 1 || _herotype == 2 || _herotype == 3)
        {
            heroTypeIcon.spriteName = string.Format("heroType{0}", _herotype);
        }
        else
        {
            //Debug.LogError("英雄职业类型参数错误" + _herotype);
        }
    }
}

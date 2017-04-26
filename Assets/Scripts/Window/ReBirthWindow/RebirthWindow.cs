using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RebirthWindow : MonoBehaviour
{
    public GameObject roleGrid;

    public GameObject helpBtn;
    public GameObject helpBtnInfo;

    public GameObject stoneRebirth;
    public GameObject diamondRebirth;
    public GameObject rebirthInfo;
    //public GameObject successRebirth;

    public GameObject roleRebirthBtn;
    public GameObject roleRebirthInfo;
    public List<GameObject> roleRebirthItemList = new List<GameObject>();

    public GameObject outfitRebirthBtn;
    public GameObject outfitRebirthInfo;
    public List<GameObject> outfitRebirthItemList = new List<GameObject>();

    public GameObject roleMsg;
    public GameObject BGSpriteRole;
    public UITexture roleTexture;
    public UISprite roleType;
    public UISprite roleAttribute;
    public UILabel roleNameLabel;

    public GameObject rebirthHeroItemPrefab;
    public GameObject rebirthItemPrefab;
    public GameObject rebirthSurePrefab;
    public GameObject rebirthSuccessPrefab;

    public GameObject stoneGrid;
    public GameObject diamondGrid;

    public GameObject camera3D;

    List<HeroInfo> myHeroList = new List<HeroInfo>();
    /// <summary>
    /// 记录当前点击的角色ID号
    /// </summary>
    public int presentHeroId = 0;
    private bool isHightRebirth = false;//是否高级重生
    private delegate void delegatePresentFunction(int del);
    /// <summary>
    /// 当前钻石重生的Grid
    /// </summary>
    private GameObject parentsDiamondGrid;
    /// <summary>
    /// 当前钻石重生的面板
    /// </summary>
    private GameObject presentDiamondPanel;
    /// <summary>
    /// 当前钻石重生时候的委托函数
    /// </summary>
    private delegatePresentFunction presentDiamondFunction;
    /// <summary>
    /// 当前重生石重生的Grid
    /// </summary>
    private GameObject parentsStoneGrid;
    /// <summary>
    /// 当前重生石重生的面板
    /// </summary>
    private GameObject presentStonePanel;
    /// <summary>
    /// 当前重生石重生时候的委托函数
    /// </summary>
    private delegatePresentFunction presentStoneFunction;
    /// <summary>
    /// -1为没有重生，0表示重生石重生，1表示钻石重生
    /// </summary>
    private int rebirthType = -1;
    /// <summary>
    /// 记录重生类型名
    /// </summary>
    private int rebirthTypeIndex = 0;

    private List<GameObject> presentGridObj = new List<GameObject>();
    /// <summary>
    /// 存储角色的列表
    /// </summary>
    private List<GameObject> presentHeroList = new List<GameObject>();
    /// <summary>
    /// 存储当前返回的Item的ID号
    /// </summary>
    private Dictionary<string, int> returnItemCode = new Dictionary<string, int>();

    private int totalDiamondNum = 0;
    /// <summary>
    /// 存储当前的3D角色的集合
    /// </summary>
    private List<GameObject> role3DList = new List<GameObject>();
    /// <summary>
    /// 存储1912协议返回的字符串数组
    /// </summary>
    private List<string> agreement_1912 = new List<string>();
    /// <summary>
    /// 判断是否可以座驾重生
    /// </summary>
    private bool isLuxuryCar = false;
    /// <summary>
    /// 当前培养的次数
    /// </summary>
    private int trainCount = 0;

    private int useDiamond = 0;
    private string useItem = "";
    private int dimondAll = 0;
    int openFirst = 0;
    /// <summary>
    /// 设置角色的大小参数
    /// </summary>
    public float heroScaleMax = 1.2f;

    private bool isRoleClick = false;
    private bool isOutfitClick = false;

    private List<int> eventRoleNumList = new List<int>();
    private List<int> eventOutfitNumList = new List<int>();
    void Start()
    {
        SetWindowInfo(CharacterRecorder.instance.RebirthRoleId);
    }

    void InitEventOnClick()
    {
        #region 帮助按钮helpBtn点击事件
        if (UIEventListener.Get(helpBtn).onClick == null)
        {
            UIEventListener.Get(helpBtn).onClick += delegate(GameObject go)
            {
                SetHelpBtnInfo();
            };
        }
        #endregion
        #region roleRebirthBtn按钮的点击事件
        if (UIEventListener.Get(roleRebirthBtn).onClick == null)
        {
            UIEventListener.Get(roleRebirthBtn).onClick += delegate(GameObject go)
            {
                if (roleRebirthInfo.activeSelf)
                {
                    roleRebirthInfo.SetActive(false);
                }
                else
                {
                    isRoleClick = true;

                    if (outfitRebirthInfo.activeSelf)
                    {
                        outfitRebirthInfo.SetActive(false);
                        roleRebirthInfo.SetActive(true);
                        //SetRoleRebirthInfo(presentHeroId);
                    }
                    else
                    {
                        roleRebirthInfo.SetActive(true);
                        //SetRoleRebirthInfo(presentHeroId);
                    }

                    //SetRoleRebirthInfo(presentHeroId);
                    Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
                    NetworkHandler.instance.SendProcess(string.Format("1025#{0};", hero.characterRoleID));
                }
            };
        }
        #endregion
        #region outfitRebirthBtn按钮的点击事件
        if (UIEventListener.Get(outfitRebirthBtn).onClick == null)
        {
            UIEventListener.Get(outfitRebirthBtn).onClick += delegate(GameObject go)
            {

                if (outfitRebirthInfo.activeSelf)
                {
                    outfitRebirthInfo.SetActive(false);
                }
                else
                {
                    isOutfitClick = true;

                    if (roleRebirthInfo.activeSelf)
                    {
                        roleRebirthInfo.SetActive(false);
                        outfitRebirthInfo.SetActive(true);
                    }
                    else
                    {
                        outfitRebirthInfo.SetActive(true);
                    }

                    Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
                    //SetOutfitRebirthInfo(presentHeroId);
                    NetworkHandler.instance.SendProcess(string.Format("3201#{0};", hero.characterRoleID));
                }
            };
        }
        #endregion
    }
    /// <summary>
    /// 设置英雄重生选项的图标显示
    /// </summary>
    public void SetRoleRebirthInfo(int heroId)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(heroId);
        HeroInfo heroInfo = TextTranslator.instance.GetHeroInfoByHeroID(heroId);
        eventRoleNumList.Clear();
        for (int i = 0; i < 4; i++)
        {
            eventRoleNumList.Add(0);
        }
        if (TextTranslator.instance.GetItemCountByID(10802) >= 1 && hero.skillLevel >= 2)//满足技能重生石图标10802>0
        {
            // roleRebirthItemList[0].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang1";
            //roleRebirthItemList[0].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            roleRebirthItemList[0].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang3";
            roleRebirthItemList[0].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[e9ffff]技能重生";
            roleRebirthItemList[0].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.17f, 0.35f, 0.55f, 1.0f);
        }
        else
        {
            if (hero.skillLevel < 2)
            {
                eventRoleNumList[0] = 11;
            }
            else
            {
                eventRoleNumList[0] = 1;
            }
            //roleRebirthItemList[0].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang5";
            roleRebirthItemList[0].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            roleRebirthItemList[0].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[abbbc9]技能重生";
            roleRebirthItemList[0].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.4f, 0.37f, 0.42f, 1.0f);
        }
        if (TextTranslator.instance.GetItemCountByID(10803) >= 1 && trainCount >= 10)//培养重生石10803>0
        {
            //roleRebirthItemList[1].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang1";
            //roleRebirthItemList[1].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            roleRebirthItemList[1].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang3";
            roleRebirthItemList[1].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[e9ffff]培养重生";
            roleRebirthItemList[1].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.17f, 0.35f, 0.55f, 1.0f);
        }
        else
        {
            if (trainCount < 10)
            {
                eventRoleNumList[1] = 11;
            }
            else
            {
                eventRoleNumList[1] = 1;
            }

            //roleRebirthItemList[1].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang5";
            roleRebirthItemList[1].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            roleRebirthItemList[1].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[abbbc9]培养重生";
            roleRebirthItemList[1].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.4f, 0.37f, 0.42f, 1.0f);
        }
        if (CharacterRecorder.instance.lunaGem >= 200 && hero.classNumber >= 2)//满足品质重生钻石>0
        {
            //roleRebirthItemList[2].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang2";
            //roleRebirthItemList[2].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            roleRebirthItemList[2].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang3";
            roleRebirthItemList[2].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[e9ffff]品质重生";
            roleRebirthItemList[2].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.17f, 0.35f, 0.55f, 1.0f);
        }
        else
        {
            if (hero.classNumber < 2)
            {
                eventRoleNumList[2] = 11;
            }
            else
            {
                eventRoleNumList[2] = 1;
            }
            //roleRebirthItemList[2].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang5";
            roleRebirthItemList[2].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            roleRebirthItemList[2].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[abbbc9]品质重生";
            roleRebirthItemList[2].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.4f, 0.37f, 0.42f, 1.0f);
        }
        if (CharacterRecorder.instance.lunaGem >= 200 && hero.level >= 10)//经验重生钻石>0
        {
            //roleRebirthItemList[3].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang2";
            //roleRebirthItemList[3].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            roleRebirthItemList[3].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang3";
            roleRebirthItemList[3].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[e9ffff]经验重生";
            roleRebirthItemList[3].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.17f, 0.35f, 0.55f, 1.0f);
        }
        else
        {
            if (hero.level < 10)
            {
                eventRoleNumList[3] = 11;
            }
            else
            {
                eventRoleNumList[3] = 1;
            }
            //roleRebirthItemList[3].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang5";
            roleRebirthItemList[3].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            roleRebirthItemList[3].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[abbbc9]经验重生";
            roleRebirthItemList[3].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.4f, 0.37f, 0.42f, 1.0f);
        }
        //设置英雄重生列表的点击事件
        SetRoleRebirthListOnClick(heroId, eventRoleNumList);
    }
    /// <summary>
    /// 设置装备重生选项的图标显示
    /// </summary>
    public void SetOutfitRebirthInfo(int heroId)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);

        eventOutfitNumList.Clear();
        for (int i = 0; i < 6; i++)
        {
            eventOutfitNumList.Add(0);
        }
        if (CharacterRecorder.instance.lunaGem >= 200 && IsSureEquipRebirth(presentHeroId) == 0)//装备强化重生钻石>0
        {
            //outfitRebirthItemList[0].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang2";
            //outfitRebirthItemList[0].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[0].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang3";
            outfitRebirthItemList[0].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[e9ffff]装备强化重生";
            outfitRebirthItemList[0].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.17f, 0.35f, 0.55f, 1.0f);
        }
        else
        {
            if (IsSureEquipRebirth(presentHeroId) != 0)
            {
                eventOutfitNumList[0] = 11;
            }
            else
            {
                eventOutfitNumList[0] = 1;
            }
            //outfitRebirthItemList[0].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang5";
            outfitRebirthItemList[0].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[0].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[abbbc9]装备强化重生";
            outfitRebirthItemList[0].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.4f, 0.37f, 0.42f, 1.0f);
        }
        if (TextTranslator.instance.GetItemCountByID(10804) >= 1 && IsSureEquipInfoRebirth(presentHeroId) == 0)//装备精炼重生石10804>0
        {
            //outfitRebirthItemList[1].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang1";
            //outfitRebirthItemList[1].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[1].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang3";
            outfitRebirthItemList[1].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[e9ffff]装备精炼重生";
            outfitRebirthItemList[1].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.17f, 0.35f, 0.55f, 1.0f);
        }
        else
        {
            if (IsSureEquipInfoRebirth(presentHeroId) != 0)
            {
                eventOutfitNumList[1] = 11;
            }
            else
            {
                eventOutfitNumList[1] = 1;
            }

            //outfitRebirthItemList[1].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang5";
            outfitRebirthItemList[1].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[1].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[abbbc9]装备精炼重生";
            outfitRebirthItemList[1].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.4f, 0.37f, 0.42f, 1.0f);
        }
        if (TextTranslator.instance.GetItemCountByID(10805) >= 1 && IsSureInfoEquipRebirth(presentHeroId) == 0)//满足勋章&手册强化重生（重生石图标10805）>0
        {
            //outfitRebirthItemList[2].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang1";
            //outfitRebirthItemList[2].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[2].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang3";
            outfitRebirthItemList[2].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[e9ffff]勋章&手册强化重生";
            outfitRebirthItemList[2].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.17f, 0.35f, 0.55f, 1.0f);
        }
        else
        {
            if (IsSureInfoEquipRebirth(presentHeroId) != 0)
            {
                eventOutfitNumList[2] = 11;
            }
            else
            {
                eventOutfitNumList[2] = 1;
            }

            //outfitRebirthItemList[2].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang5";
            outfitRebirthItemList[2].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[2].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[abbbc9]勋章&手册强化重生";
            outfitRebirthItemList[2].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.4f, 0.37f, 0.42f, 1.0f);
        }
        if (TextTranslator.instance.GetItemCountByID(10806) >= 1 && IsSureInfoEquipRebirth2(presentHeroId) == 0)//勋章&手册精炼重生（重生石图标10806）>0
        {
            //outfitRebirthItemList[3].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang1";
            //outfitRebirthItemList[3].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[3].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang3";
            outfitRebirthItemList[3].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[e9ffff]勋章&手册精炼重生";
            outfitRebirthItemList[3].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.17f, 0.35f, 0.55f, 1.0f);
        }
        else
        {
            if (IsSureInfoEquipRebirth2(presentHeroId) != 0)
            {
                eventOutfitNumList[3] = 11;
            }
            else
            {
                eventOutfitNumList[3] = 1;
            }
            //outfitRebirthItemList[3].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang5";
            outfitRebirthItemList[3].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[3].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[abbbc9]勋章&手册精炼重生";
            outfitRebirthItemList[3].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.4f, 0.37f, 0.42f, 1.0f);
        }
        if (CharacterRecorder.instance.lunaGem >= 200 && isLuxuryCar)//满足座驾重生（钻石）>0
        {
            //outfitRebirthItemList[4].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang2";
            //outfitRebirthItemList[4].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[4].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang3";
            outfitRebirthItemList[4].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[e9ffff]座驾重生";
            outfitRebirthItemList[4].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.17f, 0.35f, 0.55f, 1.0f);
        }
        else
        {
            if (!isLuxuryCar)
            {
                eventOutfitNumList[4] = 11;
            }
            else
            {
                eventOutfitNumList[4] = 1;
            }
            //outfitRebirthItemList[4].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang5";
            outfitRebirthItemList[4].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[4].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[abbbc9]座驾重生";
            outfitRebirthItemList[4].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.4f, 0.37f, 0.42f, 1.0f);
        }
        if (CharacterRecorder.instance.lunaGem >= 200 && IsSureRareTreasureRebirth(presentHeroId) == 0)//秘宝重生（钻石）>0
        {
            //outfitRebirthItemList[5].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang2";
            //outfitRebirthItemList[5].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[5].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang3";
            outfitRebirthItemList[5].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[e9ffff]秘宝重生";
            outfitRebirthItemList[5].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.17f, 0.35f, 0.55f, 1.0f);
        }
        else
        {
            if (IsSureRareTreasureRebirth(presentHeroId) != 0)
            {
                eventOutfitNumList[5] = 11;
            }
            else
            {
                eventOutfitNumList[5] = 1;
            }
            //outfitRebirthItemList[5].transform.Find("ResBgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang5";
            outfitRebirthItemList[5].transform.Find("BgSprite").GetComponent<UISprite>().spriteName = "chongsheng_kuang4";
            outfitRebirthItemList[5].transform.FindChild("NameLabel").GetComponent<UILabel>().text = "[abbbc9]秘宝重生";
            outfitRebirthItemList[5].transform.FindChild("NameLabel").GetComponent<UILabel>().effectColor = new Color(0.4f, 0.37f, 0.42f, 1.0f);
        }
        SetOutfitRebithListOnClick(heroId, eventOutfitNumList);
    }
    /// <summary>
    /// 角色按照战斗力排序
    /// </summary>
    void FightForcePaixu()
    {
        myHeroList.Sort(delegate(HeroInfo x, HeroInfo y)
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
    /// 添加属于自己的角色到myHeroList集合里面
    /// </summary>
    void AddMyheroList()
    {
        myHeroList.Clear();
        foreach (var h in TextTranslator.instance.heroInfoList)
        {
            if (h.heroID < 65000)
            {
                foreach (var item in CharacterRecorder.instance.ownedHeroList)
                {
                    if (h.heroID == item.cardID)
                    {
                        myHeroList.Add(h);
                        break;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 初始化设置面板的信息
    /// </summary>
    public void SetWindowInfo(int roleId)
    {
        //  presentHeroId = roleId;
        DestroyRoleGridObj();
        AddMyheroList();
        FightForcePaixu();
        foreach (HeroInfo heroInfo in myHeroList)
        {
            GameObject rebirthHeroItem = NGUITools.AddChild(roleGrid, rebirthHeroItemPrefab);
            rebirthHeroItem.name = heroInfo.heroID.ToString();
            presentHeroList.Add(rebirthHeroItem);
            if (rebirthHeroItem.GetComponent<RebirthHeroItem>() == null)
            {
                rebirthHeroItem.AddComponent<RebirthHeroItem>();
            }
            rebirthHeroItem.GetComponent<RebirthHeroItem>().SetRebirthHeroIntemInfo(heroInfo.heroID);
        }
        roleGrid.GetComponent<UIGrid>().Reposition();
        for (int i = 0; i < myHeroList.Count; i++)
        {
            if (myHeroList[i].heroID == roleId)
            {
                roleGrid.transform.GetChild(i).GetComponent<UIToggle>().value = true;
            }
        }
        //初始化按钮点击事件
        InitEventOnClick();
    }
    /// <summary>
    /// 销毁角色面板的角色Grid的gameObject
    /// </summary>
    public void DestroyRoleGridObj()
    {
        if (presentHeroList.Count > 0)
        {
            for (int i = 0; i < presentHeroList.Count; i++)
            {
                DestroyImmediate(presentHeroList[i]);
            }
        }
        presentHeroList.Clear();
        roleGrid.GetComponent<UIGrid>().Reposition();
    }
    /// <summary>
    /// 销毁3D角色的Object
    /// </summary>
    public void Destroy3DRoleObj()
    {
        for (int i = 0; i < role3DList.Count; i++)
        {
            DestroyImmediate(role3DList[i]);
        }
        role3DList.Clear();
    }
    /// <summary>
    /// 设置显示英雄的信息面板
    /// </summary>
    /// <param name="roleId"></param>
    public void SetRoleInfoPanel(int roleId)
    {
        presentHeroId = roleId;
        //  parentsGrid = 
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
        //presentHeroId = hero.characterRoleID;
        HeroInfo heroInfo = TextTranslator.instance.GetHeroInfoByHeroID(roleId);
        //设置角色的Texture图片
        //roleTexture.mainTexture = Resources.Load("Loading/" + heroInfo.heroID, typeof(Texture)) as Texture;
        //roleTexture.MakePixelPerfect();
        //roleTexture.transform.localScale = Vector3.one * 0.7f;

        if (openFirst == 0)
        {
            openFirst++;
            //实例化一个3D的角色
            Invoke("CreateRole3DObj", 0.4f);
        }
        else
        {
            //实例化一个3D的角色
            Invoke("CreateRole3DObj", 0);
        }

        //角色类型
        roleType.spriteName = "heroType" + heroInfo.heroCarrerType;
        //设置稀有度
        SetRarityIcon(heroInfo.heroRarity);
        //角色名称
        roleNameLabel.text = heroInfo.heroName;
        TextTranslator.instance.SetHeroNameColor(roleNameLabel, heroInfo.heroName, hero.classNumber);

        //设置角色信息的位置
        float roleMsgX = 100.0f - roleNameLabel.transform.GetComponent<UILabel>().localSize.x / 2;
        roleMsg.transform.localPosition = new Vector3(roleMsgX, roleMsg.transform.localPosition.y, roleMsg.transform.localPosition.z);

        if (roleRebirthBtn.activeSelf)
        {
            isRoleClick = true;
            NetworkHandler.instance.SendProcess(string.Format("1025#{0};", hero.characterRoleID));
        }
        if (outfitRebirthBtn.activeSelf)
        {
            isOutfitClick = true;
            NetworkHandler.instance.SendProcess(string.Format("3201#{0};", hero.characterRoleID));
        }
    }

    public void CreateRole3DObj()
    {
        Destroy3DRoleObj();
        GameObject role3D = GameObject.Instantiate(Resources.Load("Prefab/Role/" + presentHeroId) as GameObject) as GameObject;

        //int j = PictureCreater.instance.CreateRole(presentHeroId, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1, 1002, 1, 1003, 1, 1, 0);
        //GameObject role3D = PictureCreater.instance.ListRolePicture[j].RoleObject;
        role3D.transform.parent = camera3D.transform;
        role3D.name = presentHeroId.ToString();

        foreach (Component c in role3D.GetComponentsInChildren(typeof(MeshRenderer), true))
        {
            if (c.name.IndexOf("biyan") > -1)
            {
                // SetPicture[RoleIndex].RoleCloseEye = false;
                c.gameObject.SetActive(false);
                break;
            }
        }
        HeroInfo heroInfo = TextTranslator.instance.GetHeroInfoByHeroID(presentHeroId);
        float heroScale = heroInfo.heroScale * heroScaleMax;
        role3D.transform.localScale = Vector3.one * heroScale;
        if (presentHeroId == 60101 || presentHeroId == 60018 || presentHeroId == 60102 || presentHeroId == 60029)
        {
            role3D.transform.localPosition = new Vector3(0.71f, -0.44f, 5.13f);
            if (presentHeroId == 60101)
            {
                role3D.transform.Rotate(new Vector3(0, 160, 0));
                //role3D.transform.localScale = Vector3.one * 1.5f;
            }
            else
            {
                role3D.transform.Rotate(new Vector3(0, 180, 0));
                //role3D.transform.localScale = Vector3.one * 2f;
            }
        }
        else if (presentHeroId == 60103 || presentHeroId == 60104 || presentHeroId == 60200)
        {
            role3D.transform.localPosition = new Vector3(0.71f, -0.44f, 5.13f);
            if (presentHeroId == 60200)
            {
                role3D.transform.Rotate(new Vector3(0, 180, 0));
            }
            else
            {
                role3D.transform.Rotate(new Vector3(0, 145, 0));
            }

            //role3D.transform.localScale = Vector3.one * 1.5f;
        }
        else if (presentHeroId == 60201)
        {
            role3D.transform.localPosition = new Vector3(0.71f, -0.44f, 5.13f);
            role3D.transform.Rotate(new Vector3(20, 180, 0));
            //role3D.transform.localScale = Vector3.one * 0.8f;
        }
        else
        {
            role3D.transform.localPosition = new Vector3(0.71f, -0.44f, 5.13f);
            role3D.transform.Rotate(new Vector3(0, 180, 0));
            //role3D.transform.localScale = Vector3.one * 2.2f;
        }

        role3DList.Add(role3D);
    }
    /// <summary>
    /// 设置帮助按钮信息
    /// </summary>
    public void SetHelpBtnInfo()
    {
        if (helpBtnInfo.activeSelf)
        {
            return;
        }
        else
        {
            helpBtnInfo.SetActive(true);
            GameObject exitBtn = helpBtnInfo.transform.Find("All/ExitBtn").gameObject;

            //GameObject helpInfo = helpBtnInfo.transform.Find("HelpInfoLabel").gameObject;
            // helpInfo.GetComponent<UILabel>().text = "重生帮助按钮信息.";

            if (UIEventListener.Get(exitBtn).onClick == null)
            {
                UIEventListener.Get(exitBtn).onClick += delegate(GameObject go)
                {
                    if (!helpBtnInfo.activeSelf)
                    {
                        return;
                    }
                    helpBtnInfo.SetActive(false);
                };
            }
        }
    }

    /// <summary>
    /// 设置英雄重生选项列表的点击事件
    /// </summary>
    public void SetRoleRebirthListOnClick(int heroId, List<int> eventNum)
    {
        #region 英雄重生信息列表的点击事件
        #region 技能重生
        if (UIEventListener.Get(roleRebirthItemList[0]).onClick == null)
        {
            UIEventListener.Get(roleRebirthItemList[0]).onClick += delegate(GameObject go)
            {
                if (!roleRebirthInfo.activeSelf)
                {
                    return;
                }
                //技能重生事件
                if (eventNum[0] == 1)//重生石图标10802 不足
                {
                    UIManager.instance.OpenPromptWindow("重生石不足，无法进行重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                else if (eventNum[0] == 11)
                {
                    UIManager.instance.OpenPromptWindow("英雄技能等级需要达到2级才可重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }

                int roleId = presentHeroId;
                Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
                if (hero.skillLevel >= 2)
                {
                    Debug.LogError("允许技能重生");
                    roleRebirthInfo.SetActive(false);
                    rebirthType = 0;
                    rebirthTypeIndex = 4;
                    parentsStoneGrid = stoneGrid;
                    presentStonePanel = stoneRebirth;
                    presentStoneFunction = SetStoneRebirthPanel;
                    NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, 1));
                }
            };
        }
        #endregion
        #region 培养重生
        if (UIEventListener.Get(roleRebirthItemList[1]).onClick == null)
        {
            UIEventListener.Get(roleRebirthItemList[1]).onClick += delegate(GameObject go)
            {
                if (!roleRebirthInfo.activeSelf)
                {
                    return;
                }
                if (eventNum[1] == 1)//重生石10803 不足
                {
                    UIManager.instance.OpenPromptWindow("重生石不足，无法进行重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                else if (eventNum[1] == 11)
                {
                    UIManager.instance.OpenPromptWindow("英雄培养大于等于10次才可重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }

                //Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
                //培养重生事件
                //NetworkHandler.instance.SendProcess(string.Format("1025#{0};", hero.characterRoleID));
                TrainRebirth();
            };
        }
        #endregion
        #region 品质重生
        if (UIEventListener.Get(roleRebirthItemList[2]).onClick == null)
        {
            UIEventListener.Get(roleRebirthItemList[2]).onClick += delegate(GameObject go)
            {

                if (!roleRebirthInfo.activeSelf)
                {
                    return;
                }
                if (eventNum[2] == 1)//品质重生（钻石）不足
                {
                    UIManager.instance.OpenPromptWindow("重生所需钻石不足，请充值！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                else if (eventNum[2] == 11)
                {
                    UIManager.instance.OpenPromptWindow("英雄品质达到绿色才可重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
                //品质重生事件
                if (hero.classNumber >= 2)//英雄品质大于等于绿色（品质2）
                {
                    roleRebirthInfo.SetActive(false);
                    rebirthType = 1;
                    rebirthTypeIndex = 3;
                    parentsDiamondGrid = diamondGrid;
                    presentDiamondPanel = diamondRebirth;
                    presentDiamondFunction = SetDiamondRebirthPanel;
                    int hight = 0;
                    if (isHightRebirth)
                    {
                        hight = 1;
                    }
                    NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, hight));
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("英雄品质达到绿色才可重生！", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        #endregion
        #region 经验重生
        if (UIEventListener.Get(roleRebirthItemList[3]).onClick == null)
        {
            UIEventListener.Get(roleRebirthItemList[3]).onClick += delegate(GameObject go)
            {
                Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
                if (!roleRebirthInfo.activeSelf)
                {
                    return;
                }
                if (eventNum[3] == 1)//经验重生（钻石） 不足
                {
                    UIManager.instance.OpenPromptWindow("重生所需钻石不足，请充值！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                else if (eventNum[3] == 11)
                {
                    UIManager.instance.OpenPromptWindow("英雄需要达到10级才可重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }

                //经验重生事件
                if (hero.level >= 10)//英雄等级大于等于10级
                {
                    Debug.LogError("允许经验重生");
                    roleRebirthInfo.SetActive(false);
                    rebirthType = 1;//表示钻石重生
                    rebirthTypeIndex = 1;
                    parentsDiamondGrid = diamondGrid;
                    presentDiamondPanel = diamondRebirth;
                    presentDiamondFunction = SetDiamondRebirthPanel;
                    int hight = 0;
                    if (isHightRebirth)
                    {
                        hight = 1;
                    }
                    NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, hight));
                    //NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};", hero.characterRoleID, rebirthTypeIndex));
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("英雄需要达到10级才可重生！", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        #endregion
        #endregion
    }

    /// <summary>
    /// 接收到协议1911返回的数并处理
    /// </summary>
    /// <param name="len">协议的长度</param>
    public void ReceivedAgreement_1911(List<string> agreement, int diamondNum, string itemNumber)
    {
        GameObject presentPanel;
        GameObject presentPanelGrid;
        delegatePresentFunction presentFunction;

        if (rebirthType == 0)//重生石重生
        {
            presentPanel = presentStonePanel;
            presentPanelGrid = parentsStoneGrid;
            presentFunction = presentStoneFunction;
        }
        else if (rebirthType == 1)//钻石重生
        {
            presentPanel = presentDiamondPanel;
            presentPanelGrid = parentsDiamondGrid;
            presentFunction = presentDiamondFunction;
        }
        else//没有重生类型
        {
            return;
        }
        //rebirthType = -1;
        useDiamond = diamondNum;
        useItem = itemNumber;
        presentPanel.SetActive(true);
        presentFunction(presentHeroId);
        DestroyGridAllChild();
        presentPanelGrid.transform.parent.localPosition = Vector3.zero;
        presentPanelGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
        returnItemCode.Clear();
        for (int i = 0; i < agreement.Count; i++)
        {
            string[] dataSplit = agreement[i].Split('$');
            GameObject go = NGUITools.AddChild(presentPanelGrid, rebirthItemPrefab);
            presentGridObj.Add(go);
            go.name = dataSplit[0];
            int returnItemCount = int.Parse(dataSplit[1]);
            go.GetComponent<RebirthItem>().SetRebirthItemInfo(int.Parse(dataSplit[0]), returnItemCount, "RebirthGet");
            returnItemCode.Add(go.name, returnItemCount);
        }
        presentPanelGrid.GetComponent<UIGrid>().Reposition();
    }
    /// <summary>
    /// 重生失败
    /// </summary>
    public void RebirthFailed_1912()
    {

    }
    /// <summary>
    /// 接收返回1912协议返回
    /// </summary>
    /// <param name="agreement">协议集合</param>
    public void ReceivedAgreement_1912(List<string> agreement, int allDimond)
    {
        agreement_1912 = agreement;
        dimondAll = allDimond;
        //检查动画是否播放完成      
        Invoke("RebirthSuccessInfo", 1.0f);
        AudioEditer.instance.PlayOneShot("ui_recieve");
    }
    public void RebirthSuccessInfo()
    {
        bool isComplete = true;
        GameObject uiRoot = GameObject.Find("UIRoot");
        if (uiRoot != null)
        {
            GameObject rebirthSuccessPanel = NGUITools.AddChild(uiRoot, rebirthSuccessPrefab);
            rebirthSuccessPanel.layer = 11;

            if (rebirthSuccessPanel != null)
            {
                rebirthSuccessPanel.GetComponent<RebirthSuccess>().StartSuccessCoroutine(isComplete, agreement_1912, presentHeroId, rebirthTypeIndex, dimondAll);
            }
        }
        //UIManager.instance.OpenPanel("RebirthSuccess", false);

    }
    public void PlayRebirthAnimation()
    {
        GameObject uiRoot = GameObject.Find("UIRoot");
        if (uiRoot != null)
        {
            GameObject successObj = NGUITools.AddChild(uiRoot, rebirthSurePrefab);
            successObj.layer = 11;
            successObj.transform.localPosition = new Vector3(98.6f, -90.0f, 0);

            Destroy(successObj, 2.0f);
        }
    }
    /// <summary>
    /// 销毁parentsGrid里面的所有子对象
    /// </summary>
    public void DestroyGridAllChild()
    {
        for (int i = 0; i < presentGridObj.Count; i++)
        {
            DestroyImmediate(presentGridObj[i]);
        }
        presentGridObj.Clear();
    }
    /// <summary>
    /// 判断是否可以装备重生
    /// </summary>
    /// <param name="roleId">角色的ID</param>
    /// <returns>0表示可以重生， 不为表示不可以重生</returns>
    int IsSureEquipRebirth(int roleId)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
        int equipCount = 0;
        //foreach (Hero.EquipInfo equip in hero.equipList)
        for (int i = 0; i < hero.equipList.size; i++)
        {
            Hero.EquipInfo equip = hero.equipList[i];
            if (i > 3)
            {
                break;
            }
            if (equip.equipLevel < 10)
            {
                equipCount++;
                break;
            }
        }
        return equipCount;
    }
    /// <summary>
    /// 判断是否可以装备精炼重生
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>0表示可以， 其他表示不可以</returns>
    int IsSureEquipInfoRebirth(int roleId)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
        //装备精炼重生（重生石图标10804）、
        int equipCount = 0;
        //foreach (Hero.EquipInfo equip in hero.equipList)
        for (int i = 0; i < hero.equipList.size; i++)
        {
            Hero.EquipInfo equip = hero.equipList[i];
            if (i > 3)
            {
                break;
            }
            if (equip.equipClass < 1)
            {
                equipCount++;
                break;
            }
        }
        return equipCount;
    }
    /// <summary>
    /// 判断是否可以勋章&手册强化
    /// </summary>
    /// <param name="roleId">角色的ID</param>
    /// <returns>0表示可以，  其他不可以</returns>
    int IsSureInfoEquipRebirth(int roleId)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
        //满足勋章&手册强化重生（重生石图标10805）>0
        int equipCount = 0;
        for (int i = 0; i < hero.equipList.size; i++)
        {
            Hero.EquipInfo equip = hero.equipList[i];
            if (i < 4)
            {
                continue;
            }
            if (equip.equipLevel < 10)
            {
                equipCount++;
                break;
            }
        }
        return equipCount;
    }
    /// <summary>
    /// 判断是否可以勋章&手册精炼重生
    /// </summary>
    /// <param name="roleId">角色的Id</param>
    /// <returns>0表示可以，  其他不可以</returns>
    int IsSureInfoEquipRebirth2(int roleId)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
        //勋章&手册精炼重生（重生石图标10806）>0
        int equipCount = 0;
        for (int i = 0; i < hero.equipList.size; i++)
        {
            Hero.EquipInfo equip = hero.equipList[i];
            if (i < 4)
            {
                continue;
            }
            if (equip.equipClass < 3)
            {
                equipCount++;
                break;
            }
        }
        return equipCount;
    }
    /// <summary>
    /// p判断是否可以秘宝重生
    /// </summary>
    /// <param name="roleid">角色的Id</param>
    /// <returns>0表示可以 ， 其他不可以</returns>
    int IsSureRareTreasureRebirth(int roleid)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
        //秘宝重生（钻石）>0
        int hihoCount = -1;
        for (int i = 0; i < TextTranslator.instance.RareTreasureOpenDic.Count; i++)
        {
            RareTreasureOpen hihou = TextTranslator.instance.RareTreasureOpenDic[i + 1];
            if (hihou.state == 2)
            {
                hihoCount = 0;
                break;
            }
        }
        return hihoCount;
    }
    /// <summary>
    /// 设置装备重生选项列表的点击事件
    /// </summary>
    public void SetOutfitRebithListOnClick(int heroId, List<int> eventNumList)
    {
        #region 装备重生信息列表点击事件
        #region 装备强化重生
        if (UIEventListener.Get(outfitRebirthItemList[0]).onClick == null)
        {
            UIEventListener.Get(outfitRebirthItemList[0]).onClick += delegate(GameObject go)
            {
                if (!outfitRebirthInfo.activeSelf)
                {
                    return;
                }
                if (eventNumList[0] == 1)//装备强化重生（钻石）、不足
                {
                    UIManager.instance.OpenPromptWindow("重生所需钻石不足，请充值！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                else if (eventNumList[0] == 11)
                {
                    UIManager.instance.OpenPromptWindow("4件装备等级需要达到10级才可重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                //装备强化重生（钻石）、
                int roleId = presentHeroId;
                Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
                int equipCount = 0;
                //foreach (Hero.EquipInfo equip in hero.equipList)
                for (int i = 0; i < hero.equipList.size; i++)
                {
                    Hero.EquipInfo equip = hero.equipList[i];
                    if (i > 3)
                    {
                        break;
                    }
                    if (equip.equipLevel < 10)
                    {
                        equipCount++;
                        break;
                    }
                }
                if (equipCount == 0)
                {
                    outfitRebirthInfo.SetActive(false);
                    Debug.LogError("允许装备强化重生.");
                    rebirthType = 1;
                    rebirthTypeIndex = 5;
                    parentsDiamondGrid = diamondGrid;
                    presentDiamondPanel = diamondRebirth;
                    presentDiamondFunction = SetDiamondRebirthPanel;
                    int hight = 0;
                    if (isHightRebirth)
                    {
                        hight = 1;
                    }
                    NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, hight));
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("4件装备等级需要达到10级才可重生！", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        #endregion
        #region 装备精炼重生
        if (UIEventListener.Get(outfitRebirthItemList[1]).onClick == null)
        {
            UIEventListener.Get(outfitRebirthItemList[1]).onClick += delegate(GameObject go)
            {

                if (!outfitRebirthInfo.activeSelf)
                {
                    return;
                }
                if (eventNumList[1] == 1)//装备精炼重生（重生石图标10804）、
                {
                    UIManager.instance.OpenPromptWindow("重生石不足，无法进行重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                else if (eventNumList[1] == 11)
                {
                    UIManager.instance.OpenPromptWindow("4件装备精炼等级需要达到1级才可重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }

                int roleId = presentHeroId;
                Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
                //装备精炼重生（重生石图标10804）、
                int equipCount = 0;
                //foreach (Hero.EquipInfo equip in hero.equipList)
                for (int i = 0; i < hero.equipList.size; i++)
                {
                    Hero.EquipInfo equip = hero.equipList[i];
                    if (i > 3)
                    {
                        break;
                    }
                    if (equip.equipClass < 1)
                    {
                        equipCount++;
                        break;
                    }
                }
                if (equipCount == 0)
                {
                    Debug.LogError("允许装备精炼重生");
                    outfitRebirthInfo.SetActive(false);
                    rebirthType = 0;
                    rebirthTypeIndex = 6;
                    parentsStoneGrid = stoneGrid;
                    presentStonePanel = stoneRebirth;
                    presentStoneFunction = SetStoneRebirthPanel;
                    NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, 1));
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("4件装备精炼等级需要达到1级才可重生！", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        #endregion
        #region 勋章&手册强化重生
        if (UIEventListener.Get(outfitRebirthItemList[2]).onClick == null)
        {
            UIEventListener.Get(outfitRebirthItemList[2]).onClick += delegate(GameObject go)
            {
                if (!outfitRebirthInfo.activeSelf)
                {
                    return;
                }
                if (eventNumList[2] == 1)//满足勋章&手册强化重生（重生石图标10805）>0
                {
                    UIManager.instance.OpenPromptWindow("重生石不足，无法进行重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                else if (eventNumList[2] == 11)
                {
                    UIManager.instance.OpenPromptWindow("勋章和手册强化等级需要达到10级才可重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }

                int roleId = presentHeroId;
                Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
                //满足勋章&手册强化重生（重生石图标10805）>0
                int equipCount = 0;
                for (int i = 0; i < hero.equipList.size; i++)
                {
                    Hero.EquipInfo equip = hero.equipList[i];
                    if (i < 4)
                    {
                        continue;
                    }
                    if (equip.equipLevel < 10)
                    {
                        equipCount++;
                        break;
                    }
                }
                if (equipCount == 0)
                {
                    Debug.LogError("勋章和手册强化允许重生！");
                    outfitRebirthInfo.SetActive(false);
                    rebirthType = 0;
                    rebirthTypeIndex = 7;
                    parentsStoneGrid = stoneGrid;
                    presentStonePanel = stoneRebirth;
                    presentStoneFunction = SetStoneRebirthPanel;
                    NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, 1));
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("勋章和手册强化等级需要达到10级才可重生！", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        #endregion
        #region 勋章&手册精炼重生
        if (UIEventListener.Get(outfitRebirthItemList[3]).onClick == null)
        {
            UIEventListener.Get(outfitRebirthItemList[3]).onClick += delegate(GameObject go)
            {
                if (!outfitRebirthInfo.activeSelf)
                {
                    return;
                }
                if (eventNumList[3] == 1)
                {
                    UIManager.instance.OpenPromptWindow("重生石不足，无法进行重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                else if (eventNumList[3] == 11)
                {
                    UIManager.instance.OpenPromptWindow("勋章和手册精炼等级需要达到3级才可重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }

                int roleId = presentHeroId;
                Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
                //勋章&手册精炼重生（重生石图标10806）>0
                int equipCount = 0;
                for (int i = 0; i < hero.equipList.size; i++)
                {
                    Hero.EquipInfo equip = hero.equipList[i];
                    if (i < 4)
                    {
                        continue;
                    }
                    if (equip.equipClass < 3)
                    {
                        equipCount++;
                        break;
                    }
                }
                if (equipCount == 0)
                {
                    Debug.LogError("允许勋章&手册精炼重生（重生石图标10806）");
                    outfitRebirthInfo.SetActive(false);
                    rebirthType = 0;
                    rebirthTypeIndex = 8;
                    parentsStoneGrid = stoneGrid;
                    presentStonePanel = stoneRebirth;
                    presentStoneFunction = SetStoneRebirthPanel;
                    NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, 1));
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("勋章和手册精炼等级需要达到3级才可重生！", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        #endregion
        #region 座驾重生
        if (UIEventListener.Get(outfitRebirthItemList[4]).onClick == null)
        {
            UIEventListener.Get(outfitRebirthItemList[4]).onClick += delegate(GameObject go)
            {
                Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
                if (!outfitRebirthInfo.activeSelf)
                {
                    return;
                }
                if (eventNumList[4] == 1)
                {
                    UIManager.instance.OpenPromptWindow("重生所需钻石不足，请充值！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                else if (eventNumList[4] == 11)
                {
                    UIManager.instance.OpenPromptWindow("激活任意一个座驾才可重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }

                //满足座驾重生（钻石）>0
                LuxuryCarRebirth();
            };
        }
        #endregion
        #region 秘宝重生
        if (UIEventListener.Get(outfitRebirthItemList[5]).onClick == null)
        {
            UIEventListener.Get(outfitRebirthItemList[5]).onClick += delegate(GameObject go)
            {
                Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
                if (!outfitRebirthInfo.activeSelf)
                {
                    return;
                }
                if (eventNumList[5] == 1)
                {
                    UIManager.instance.OpenPromptWindow("重生所需钻石不足，请充值！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                else if (eventNumList[5] == 11)
                {
                    UIManager.instance.OpenPromptWindow("至少穿戴1个秘宝才可重生！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }

                //秘宝重生（钻石）>0
                int hihoCount = -1;
                for (int i = 0; i < TextTranslator.instance.RareTreasureOpenDic.Count; i++)
                {
                    RareTreasureOpen hihou = TextTranslator.instance.RareTreasureOpenDic[i + 1];
                    if (hihou.state == 2)
                    {
                        hihoCount = 0;
                        break;
                    }
                }
                if (hihoCount == 0)
                {
                    Debug.LogError("允许秘宝重生（钻石）");
                    outfitRebirthInfo.SetActive(false);
                    rebirthType = 1;
                    rebirthTypeIndex = 10;
                    parentsDiamondGrid = diamondGrid;
                    presentDiamondPanel = diamondRebirth;
                    presentDiamondFunction = SetDiamondRebirthPanel;
                    int hight = 0;
                    if (isHightRebirth)
                    {
                        hight = 1;
                    }
                    NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, hight));
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("至少穿戴1个秘宝才可重生！", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        #endregion
        #endregion
    }
    /// <summary>
    /// 协议1025返回  
    /// </summary>
    /// <param name="count">培养次数</param>
    public void TrainRebirth(int count)
    {
        Debug.LogError("TrainRebirth: " + isRoleClick);
        if (isRoleClick)
        {
            trainCount = count;
            isRoleClick = false;

            SetRoleRebirthInfo(presentHeroId);
        }
    }
    /// <summary>
    /// 培养重生检测函数
    /// </summary>
    /// <param name="trainCount">培养的次数</param>
    public void TrainRebirth()
    {
        if (trainCount >= 10)//判断该英雄的培养次数是否大于等于10次
        {
            Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
            rebirthType = 0;
            rebirthTypeIndex = 2;
            parentsStoneGrid = stoneGrid;
            presentStonePanel = stoneRebirth;
            presentStoneFunction = SetStoneRebirthPanel;
            Debug.LogError("允许培养能重生");
            roleRebirthInfo.SetActive(false);
            NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, 1));
        }
        else
        {
            UIManager.instance.OpenPromptWindow("英雄培养大于等于10次才可重生！", PromptWindow.PromptType.Hint, null, null);
        }
    }
    /// <summary>
    /// 得到是否可以座驾重生
    /// </summary>
    /// <param name="receiveMsg"></param>
    public void LuxuryCarRebirth(string[] receiveMsg)
    {
        Debug.LogError("LuxuryCarRebirth:  " + isOutfitClick);
        if (isOutfitClick)
        {
            isOutfitClick = false;
            //bool isLuxuryCar = false;
            for (int i = 0; i < receiveMsg.Length - 2; i++)
            {
                string[] SuccseID = receiveMsg[i].Split('$');
                if (SuccseID[1] == "1")//有车
                {
                    isLuxuryCar = true;
                    break;
                }
                else
                {
                    isLuxuryCar = false;
                }
            }

            SetOutfitRebirthInfo(presentHeroId);
        }
    }
    /// <summary>
    /// 座驾重生检测函数
    /// </summary>
    /// <param name="receiveMsg">是否拥有座驾的信心</param>
    public void LuxuryCarRebirth()
    {
        //允许豪车重生
        if (isLuxuryCar)
        {
            Debug.LogError("允许座驾重生!");
            outfitRebirthInfo.SetActive(false);
            isLuxuryCar = false;
            Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
            rebirthType = 1;
            rebirthTypeIndex = 9;
            parentsDiamondGrid = diamondGrid;
            presentDiamondPanel = diamondRebirth;
            presentDiamondFunction = SetDiamondRebirthPanel;
            int hight = 0;
            if (isHightRebirth)
            {
                hight = 1;
            }
            NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, hight));
        }
        else
        {
            UIManager.instance.OpenPromptWindow("激活任意一个座驾才可重生！", PromptWindow.PromptType.Hint, null, null);
        }
    }
    /// <summary>
    /// 设置钻石重生面板信息
    /// </summary>
    /// <param name="roleId">英雄ID号</param>
    public void SetDiamondRebirthPanel(int roleId)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
        UILabel expInfoLabel = diamondRebirth.transform.Find("All/ExpInfoLabel").GetComponent<UILabel>();
        string name = GetPanelTopName(rebirthTypeIndex);
        if (name == "座驾")
        {
            expInfoLabel.text = string.Format("[c1f2fb]还原英雄 {0} [c1f2fb]{1}，可还原道具:", TextTranslator.instance.SetHeroNameColor(hero.name, hero.classNumber), string.Format("的{0}", name));
        }
        else if (name != "经验")
        {
            expInfoLabel.text = string.Format("[c1f2fb]还原英雄 {0} [c1f2fb]{1}，可还原道具:", TextTranslator.instance.SetHeroNameColor(hero.name, hero.classNumber), string.Format("的{0}等级", name));
        }
        else
        {
            expInfoLabel.text = string.Format("[c1f2fb]还原英雄 {0} [c1f2fb]的等级，可还原道具:", TextTranslator.instance.SetHeroNameColor(hero.name, hero.classNumber));
        }
        diamondRebirth.transform.FindChild("All/ExpCheckbox").FindChild("Label").GetComponent<UILabel>().text = "[c1f2fb]高级重生[00ff00](全部返还)";
        //初始化经验重生面板的按钮点击事件
        InitDiamondbirthPanelOnClick();
    }
    /// <summary>
    /// 设置钻石重生面板的点击按钮事件
    /// </summary>
    public void InitDiamondbirthPanelOnClick()
    {
        //确定按钮
        GameObject sureBtn = diamondRebirth.transform.Find("All/SureBtn").gameObject;
        //重生消耗的钻石数量
        GameObject diamondCount = sureBtn.transform.Find("AccCountLabel").gameObject;
        //高级重生选择框
        GameObject expCheckbox = diamondRebirth.transform.Find("All/ExpCheckbox").gameObject;
        GameObject checkMark = expCheckbox.transform.Find("Checkmark").gameObject;
        UILabel topName = diamondRebirth.transform.Find("All/BgSprite/NameLabel").GetComponent<UILabel>();
        GameObject leftSprite = diamondRebirth.transform.Find("All/BgSprite/LeftSprite").gameObject;
        GameObject rightSprite = diamondRebirth.transform.Find("All/BgSprite/RightSprite").gameObject;
        //设置面板的名称位置
        topName.text = GetPanelTopName(rebirthTypeIndex) + "重生";
        //设置面板Top的位置
        SetPanelTopPosition(leftSprite, topName.gameObject, rightSprite);
        if (!isHightRebirth)
        {
            // isHightRebirth = false;//初始化判断是否为高级重生
            checkMark.SetActive(false);
            //    totalDiamondNum = 200;
        }
        else
        {
            checkMark.SetActive(true);
        }
        totalDiamondNum = useDiamond;
        diamondCount.GetComponent<UILabel>().text = totalDiamondNum.ToString();
        #region 钻石重生的确定按钮事件
        if (UIEventListener.Get(sureBtn).onClick == null)
        {
            UIEventListener.Get(sureBtn).onClick += delegate(GameObject go)
            {
                diamondRebirth.SetActive(false);
                rebirthInfo.SetActive(true);
                SetRebirthInfoPanel(presentHeroId, totalDiamondNum);
            };
        }
        #endregion
        #region 是否选择高级重生
        if (UIEventListener.Get(expCheckbox).onClick == null)
        {
            UIEventListener.Get(expCheckbox).onClick = delegate(GameObject go)
            {
                Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
                if (checkMark.activeSelf)//取消高级重生
                {
                    // totalDiamondNum = 200;
                    isHightRebirth = false;
                    checkMark.SetActive(false);
                    NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, 0));
                }
                else//高级重生
                {
                    isHightRebirth = true;
                    checkMark.SetActive(true);
                    //totalDiamondNum = 0;
                    //高级重生时候消耗的总的钻石
                    //foreach (KeyValuePair<string, int> itemId in returnItemCode)
                    //{
                    //    TextTranslator.ItemInfo itemInfo = TextTranslator.instance.GetItemByItemCode(int.Parse(itemId.Key));
                    //    //item 表的 RelifeValue的值
                    //    totalDiamondNum += Mathf.CeilToInt(itemInfo.RelifeValue / 10000.0f * itemId.Value);
                    //}
                    //totalDiamondNum += 200;
                    NetworkHandler.instance.SendProcess(string.Format("1911#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, 1));
                }
                //diamondCount.GetComponent<UILabel>().text = string.Format("{0}", totalDiamondNum);
                StartCoroutine(OnEquipDataUpEffect(diamondCount.GetComponent<UILabel>()));
            };
        }
        #endregion
        GameObject exitBtn = diamondRebirth.transform.Find("All/ExitBtn").gameObject;
        if (UIEventListener.Get(exitBtn).onClick == null)
        {
            UIEventListener.Get(exitBtn).onClick += delegate(GameObject go)
            {
                diamondRebirth.SetActive(false);
            };
        }
    }
    /// <summary>
    /// 设置重生信息面板的信息
    /// </summary>
    /// <param name="roleId">英雄的ID号</param>
    public void SetRebirthInfoPanel(int roleId, int num)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
        UILabel infoLabel = rebirthInfo.transform.Find("All/InfoLabel").GetComponent<UILabel>();
        string name = GetPanelTopName(rebirthTypeIndex);
        if (name != "经验")
        {
            infoLabel.text = string.Format("[c1f2fb]将英雄 {0} [c1f2fb]的{1}等级还原至初始状态，是否继续？", TextTranslator.instance.SetHeroNameColor(hero.name, hero.classNumber), name);
        }
        else
        {
            infoLabel.text = string.Format("[c1f2fb]将英雄 {0} [c1f2fb]的等级还原至初始状态，是否继续？", TextTranslator.instance.SetHeroNameColor(hero.name, hero.classNumber));
        }
        GameObject expendLabel = rebirthInfo.transform.Find("All/XiaohaoLabel").gameObject;
        GameObject expendSprite = expendLabel.transform.GetChild(0).gameObject;
        GameObject expendLabelItem = expendLabel.transform.GetChild(1).gameObject;
        expendSprite.GetComponent<UISprite>().spriteName = GetRebirthSpriteName(rebirthTypeIndex);
        //设置面板Top的位置
        UILabel topName = rebirthInfo.transform.Find("All/BgSprite/NameLabel").GetComponent<UILabel>();
        GameObject leftSprite = rebirthInfo.transform.Find("All/BgSprite/LeftSprite").gameObject;
        GameObject rightSprite = rebirthInfo.transform.Find("All/BgSprite/RightSprite").gameObject;
        UILabel useNumber = rebirthInfo.transform.FindChild("All/FanhuanLabel").GetChild(1).GetComponent<UILabel>();
        if (rebirthType == 1 && !isHightRebirth)
        {
            useNumber.text = " 80%";
        }
        else
        {
            useNumber.text = "100%";
        }
        //设置面板的名称位置
        topName.text = GetPanelTopName(rebirthTypeIndex) + "重生";
        //设置面板Top的位置
        SetPanelTopPosition(leftSprite, topName.gameObject, rightSprite);
        if (rebirthType == 0)//重生石重生
        {
            expendSprite.transform.localScale = Vector3.one * 0.5f;
            expendLabelItem.GetComponent<UILabel>().text = string.Format("{0}", num);
        }
        else if (rebirthType == 1)//钻石重生
        {
            expendSprite.transform.localScale = Vector3.one * 0.7f;
            expendLabelItem.GetComponent<UILabel>().text = string.Format("{0}", num);
        }

        //初始化RebirthInfoPanel的按钮点击事件
        InitRebirthInfoPanelOnClick();
    }
    /// <summary>
    /// 设置RebirthInfoPanel的信息
    /// </summary>
    public void InitRebirthInfoPanelOnClick()
    {
        GameObject sureBtn = rebirthInfo.transform.Find("All/SureBtn").gameObject;
        if (UIEventListener.Get(sureBtn).onClick == null)
        {
            UIEventListener.Get(sureBtn).onClick += delegate(GameObject go)
            {
                Hero hero = CharacterRecorder.instance.GetHeroByRoleID(presentHeroId);
                int hightNum = -1;
                if (isHightRebirth)
                {
                    hightNum = 1;
                }
                else
                {
                    hightNum = 0;
                }
                if (rebirthType == 0)//表示重生石重生
                {
                    hightNum = 1;
                }
                rebirthInfo.SetActive(false);
                this.gameObject.transform.FindChild("MaskSprite").gameObject.SetActive(true);
                PlayRebirthAnimation();
                NetworkHandler.instance.SendProcess(string.Format("1912#{0};{1};{2};", hero.characterRoleID, rebirthTypeIndex, hightNum));
            };
        }
        GameObject exitBtn = rebirthInfo.transform.Find("All/ExitBtn").gameObject;
        if (UIEventListener.Get(exitBtn).onClick == null)
        {
            UIEventListener.Get(exitBtn).onClick += delegate(GameObject go)
            {
                rebirthInfo.SetActive(false);
            };
        }
        GameObject cancelBtn = rebirthInfo.transform.Find("All/CancelBtn").gameObject;
        if (UIEventListener.Get(cancelBtn).onClick == null)
        {
            UIEventListener.Get(cancelBtn).onClick += delegate(GameObject go)
            {
                rebirthInfo.SetActive(false);
            };
        }
    }
    /// <summary>
    /// 设置重生石重生面板界面
    /// </summary>
    /// <param name="roleId">当前角色的ID号</param>
    public void SetStoneRebirthPanel(int roleId)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
        UILabel stoneInfoLabel = stoneRebirth.transform.Find("All/StoneInfoLabel").GetComponent<UILabel>();
        string name = GetPanelTopName(rebirthTypeIndex);
        stoneInfoLabel.text = string.Format("[c1f2fb]还原英雄 {0} [c1f2fb]{1}，可还原道具:", TextTranslator.instance.SetHeroNameColor(hero.name, hero.classNumber), string.Format("的{0}等级", name));
        //初始化重生石重生面板的按钮点击事件
        InitStoneRebirthPanelonClick();
    }
    /// <summary>
    /// 初始化重生石重生面板的按钮点击事件
    /// </summary>
    public void InitStoneRebirthPanelonClick()
    {
        GameObject sureBtn = stoneRebirth.transform.Find("All/SureBtn").gameObject;
        UISprite iconSprite = sureBtn.transform.Find("AccSprite").GetComponent<UISprite>();
        GameObject otherLabel = stoneRebirth.transform.Find("All/OtherLabel").gameObject;
        iconSprite.spriteName = GetRebirthSpriteName(rebirthTypeIndex);
        UILabel topName = stoneRebirth.transform.Find("All/BgSprite/NameLabel").GetComponent<UILabel>();
        GameObject leftSprite = stoneRebirth.transform.Find("All/BgSprite/LeftSprite").gameObject;
        GameObject rightSprite = stoneRebirth.transform.Find("All/BgSprite/RightSprite").gameObject;
        //设置面板的名称位置
        topName.text = GetPanelTopName(rebirthTypeIndex) + "重生";
        //设置面板Top的位置
        SetPanelTopPosition(leftSprite, topName.gameObject, rightSprite);

        if (topName.text != "培养")
        {
            otherLabel.SetActive(false);
        }
        else
        {
            otherLabel.SetActive(true);
        }

        if (UIEventListener.Get(sureBtn).onClick == null)
        {
            UIEventListener.Get(sureBtn).onClick += delegate(GameObject go)
            {
                stoneRebirth.SetActive(false);
                rebirthInfo.SetActive(true);
                SetRebirthInfoPanel(presentHeroId, 1);
            };
        }
        GameObject exitBtn = stoneRebirth.transform.Find("All/ExitBtn").gameObject;
        if (UIEventListener.Get(exitBtn).onClick == null)
        {
            UIEventListener.Get(exitBtn).onClick += delegate(GameObject go)
            {
                stoneRebirth.SetActive(false);
            };
        }
    }
    /// <summary>
    ///  设置稀有度
    /// </summary>
    /// <param name="_heroRarity"></param>
    void SetRarityIcon(int _heroRarity)
    {
        switch (_heroRarity)
        {
            case 1:
                roleAttribute.spriteName = "word4";
                break;
            case 2:
                roleAttribute.spriteName = "word5";
                break;
            case 3:
                roleAttribute.spriteName = "word6";
                break;
            case 4:
                roleAttribute.spriteName = "word7";
                break;
            case 5:
                roleAttribute.spriteName = "word8";
                break;
            case 6:
                roleAttribute.spriteName = "word9";
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 获取重生所使用的道具的图标的名称
    /// </summary>
    /// <param name="index">重生类型</param>
    /// <returns>重生使用的道具名</returns>
    public string GetRebirthSpriteName(int index)
    {
        string name = null;
        switch (index)
        {
            case 2:
                name = "10803";
                break;
            case 4:
                name = "10802";
                break;
            case 6:
                name = "10804";
                break;
            case 7:
                name = "10805";
                break;
            case 8:
                name = "10806";
                break;
            default:
                name = "90002";
                break;
        }
        return name;
    }
    /// <summary>
    /// 获取当前面板的重生名称
    /// </summary>
    /// <param name="index">重生类型</param>
    /// <returns>面板的Top名字</returns>
    public string GetPanelTopName(int index)
    {
        string name = null;
        switch (index)
        {
            case 1:
                name = "经验";
                break;
            case 2:
                name = "培养";
                break;
            case 3:
                name = "品质";
                break;
            case 4:
                name = "技能";
                break;
            case 5:
                name = "装备强化";
                break;
            case 6:
                name = "装备精炼";
                break;
            case 7:
                name = "勋章&手册强化";
                break;
            case 8:
                name = "勋章&手册精炼";
                break;
            case 9:
                name = "座驾";
                break;
            case 10:
                name = "秘宝";
                break;
            default:
                break;
        }
        return name;
    }
    /// <summary>
    /// 设置三个对象的本地坐标，以center为中心
    /// </summary>
    /// <param name="left">左边的对象（具有UISprite组件（右对齐））</param>
    /// <param name="center">中间的对象（具有UILabel组件（中心对其））</param>
    /// <param name="right">右边的对象（具有UISprite组件（左对齐）</param>
    public void SetPanelTopPosition(GameObject left, GameObject center, GameObject right)
    {
        float centerPositionX = center.GetComponent<UILabel>().localSize.x;
        float centerLocalPositionX = center.transform.localPosition.x;
        left.transform.localPosition = new Vector3(centerLocalPositionX - 8 - centerPositionX / 2, left.transform.localPosition.y, left.transform.localPosition.z);
        right.transform.localPosition = new Vector3(centerLocalPositionX + 8 + centerPositionX / 2, right.transform.localPosition.y, right.transform.localPosition.z);
    }
    /// <summary>
    /// 变大醒目提醒
    /// </summary>
    /// <param name="_myLabel">需要变大的UILabel</param>
    /// <returns>协程</returns>
    public IEnumerator OnEquipDataUpEffect(UILabel _myLabel)
    {
        _myLabel.color = new Color(62 / 255f, 232 / 255f, 23 / 255f, 255 / 255f);
        TweenScale _TweenScale = _myLabel.gameObject.GetComponent<TweenScale>();
        if (_TweenScale == null)
        {
            _TweenScale = _myLabel.gameObject.AddComponent<TweenScale>();
        }
        _TweenScale.from = Vector3.one;
        _TweenScale.to = 1.2f * Vector3.one;
        _TweenScale.duration = 0.3f;
        _TweenScale.PlayForward();
        yield return new WaitForSeconds(0.3f);
        _TweenScale.duration = 0.1f;
        _TweenScale.PlayReverse();
        Color backColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _myLabel.color = backColor;
    }
}

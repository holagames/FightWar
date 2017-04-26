using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleEquipWindow : MonoBehaviour
{
    public GameObject MyGrid;
    public GameObject MyGridEquip;
    public GameObject MyGridWear;

    public GameObject MyEquipItem;
    public GameObject MyWearItem;

    public GameObject CanEquipItem1;
    public GameObject CanEquipItem2;
    public GameObject CanEquipItem3;
    public GameObject CanEquipItem4;

    List<GameObject> ListEquipItem = new List<GameObject>();
    List<GameObject> ListWearItem = new List<GameObject>();

    int _AttType = 0;
    int _Type = 0;
    int _Position = 0;

    void OnEnable()
    {
        PictureCreater.instance.DestroyAllComponent();
    }

    void Start()
    {
        if (UIEventListener.Get(GameObject.Find("EquipCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("EquipCloseButton")).onClick += delegate(GameObject go)
            {
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetHeroClick(-1);
                UIManager.instance.BackUI();
            };
        }

        if (UIEventListener.Get(GameObject.Find("Tab1Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab1Button")).onClick += delegate(GameObject go)
            {
                for (int i = 0; i < ListEquipItem.Count; i++)
                {
                    DestroyImmediate(ListEquipItem[i]);
                }
                ListEquipItem.Clear();
                SetInfo(_AttType, _Type, _Position);
            };
        }

        if (UIEventListener.Get(GameObject.Find("Tab2Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab2Button")).onClick += delegate(GameObject go)
            {
                for (int i = 0; i < ListEquipItem.Count; i++)
                {
                    DestroyImmediate(ListEquipItem[i]);
                }
                ListEquipItem.Clear();
                //SetWearPart();
            };
        }
    }



    public void SetInfo(int AttType, int Type, int Position)//type: 1.人类  2.坦克  3.飞机
    {
        for (int k = 0; k < ListEquipItem.Count; k++)
        {
            DestroyImmediate(ListEquipItem[k]);
        }
        ListEquipItem.Clear();

        _AttType = AttType;
        _Type = Type;
        _Position = Position;
        int i = 0;
        //Debug.LogError(AttType + "       " + Type + "      " + Position);
        CanEquipItem1.SetActive(false);
        CanEquipItem2.SetActive(false);
        //CanEquipItem3.SetActive(true);
        //CanEquipItem4.SetActive(true);
        foreach (var item in TextTranslator.instance.bagItemList)
        {
            if (item.itemCode > 50000 && item.itemCode < 60000 && item.itemCount != 0)
            {
                string itemCode = item.itemCode.ToString();
                switch (Type)
                {
                    case 1:
                        //Debug.LogError(itemCode + "  *******  " + itemCode.Substring(1,1) + "    ********    " + itemCode.Substring(2,1));
                        if (AttType == 1 && itemCode.Substring(1, 1) == "0" && itemCode.Substring(2, 1) == Position.ToString())
                        {
                            GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                            go.name = "EquipItem" + item.itemCode;
                            go.AddComponent<RoleEquipItem>();
                            go.GetComponent<RoleEquipItem>().Init(item.itemCode, item.itemGrade, item.itemCount, item.equipID, i);
                            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                            ListEquipItem.Add(go);
                            i++;
                        }
                        else if (AttType == 2 && itemCode.Substring(1, 1) == "3" && itemCode.Substring(2, 1) == Position.ToString())
                        {
                            GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                            go.name = "EquipItem" + item.itemCode;
                            go.AddComponent<RoleEquipItem>();
                            go.GetComponent<RoleEquipItem>().Init(item.itemCode, item.itemGrade, item.itemCount, item.equipID, i);
                            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                            ListEquipItem.Add(go);
                            i++;
                        }
                        break;
                    case 2:
                        if (AttType == 1 && itemCode.Substring(1, 1) == "1" && itemCode.Substring(2, 1) == Position.ToString())
                        {
                            GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                            go.name = "EquipItem" + item.itemCode;
                            go.AddComponent<RoleEquipItem>();
                            go.GetComponent<RoleEquipItem>().Init(item.itemCode, item.itemGrade, item.itemCount, item.equipID, i);
                            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                            ListEquipItem.Add(go);
                            i++;
                        }
                        else if (AttType == 2 && itemCode.Substring(1, 1) == "4" && itemCode.Substring(2, 1) == Position.ToString())
                        {
                            GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                            go.name = "EquipItem" + item.itemCode;
                            go.AddComponent<RoleEquipItem>();
                            go.GetComponent<RoleEquipItem>().Init(item.itemCode, item.itemGrade, item.itemCount, item.equipID, i);
                            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                            ListEquipItem.Add(go);
                            i++;
                        }
                        break;
                    case 3:
                        if (AttType == 1 && itemCode.Substring(1, 1) == "2" && itemCode.Substring(2, 1) == Position.ToString())
                        {
                            GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                            go.name = "EquipItem" + item.itemCode;
                            go.AddComponent<RoleEquipItem>();
                            go.GetComponent<RoleEquipItem>().Init(item.itemCode, item.itemGrade, item.itemCount, item.equipID, i);
                            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                            ListEquipItem.Add(go);
                            i++;
                        }
                        else if (AttType == 2 && itemCode.Substring(1, 1) == "5" && itemCode.Substring(2, 1) == Position.ToString())
                        {
                            GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                            go.name = "EquipItem" + item.itemCode;
                            go.AddComponent<RoleEquipItem>();
                            go.GetComponent<RoleEquipItem>().Init(item.itemCode, item.itemGrade, item.itemCount, item.equipID, i);
                            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                            ListEquipItem.Add(go);
                            i++;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        string substring = "";
        switch (Type)
        {
            case 1:
                //Debug.LogError(itemCode + "  *******  " + itemCode.Substring(1,1) + "    ********    " + itemCode.Substring(2,1));
                if (AttType == 1)
                {
                    substring = "50" + Position.ToString();
                }
                else if (AttType == 2)
                {
                    substring = "53" + Position.ToString();
                }
                break;
            case 2:
                if (AttType == 1)
                {
                    substring = "51" + Position.ToString();
                }
                else if (AttType == 2)
                {
                    substring = "54" + Position.ToString();
                }
                break;
            case 3:
                if (AttType == 1)
                {
                    substring = "52" + Position.ToString();
                }
                else if (AttType == 2)
                {
                    substring = "55" + Position.ToString();
                }
                break;
            default:
                break;
        }

        //CanEquipItem1.transform.FindChild("LabelName").GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(int.Parse(substring + "01"));
        //CanEquipItem1.transform.FindChild("LabelDesc").GetComponent<UILabel>().text = TextTranslator.instance.GetItemDescriptionByItemCode(int.Parse(substring + "01"));
        //CanEquipItem1.transform.FindChild("SpriteGrade").FindChild("SpriteItem").GetComponent<UISprite>().spriteName = substring + "01";
        //CanEquipItem2.transform.FindChild("LabelName").GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(int.Parse(substring + "02"));
        //CanEquipItem2.transform.FindChild("LabelDesc").GetComponent<UILabel>().text = TextTranslator.instance.GetItemDescriptionByItemCode(int.Parse(substring + "02"));
        //CanEquipItem2.transform.FindChild("SpriteGrade").FindChild("SpriteItem").GetComponent<UISprite>().spriteName = substring + "02";
        CanEquipItem3.SetActive(false);
        CanEquipItem4.SetActive(false);
        //CanEquipItem3.transform.FindChild("LabelName").GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(int.Parse(substring + "03"));
        //CanEquipItem3.transform.FindChild("LabelDesc").GetComponent<UILabel>().text = TextTranslator.instance.GetItemDescriptionByItemCode(int.Parse(substring + "03"));
        //CanEquipItem3.transform.FindChild("SpriteGrade").FindChild("SpriteItem").GetComponent<UISprite>().spriteName = substring + "03";
        //CanEquipItem4.transform.FindChild("LabelName").GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(int.Parse(substring + "04"));
        //CanEquipItem4.transform.FindChild("LabelDesc").GetComponent<UILabel>().text = TextTranslator.instance.GetItemDescriptionByItemCode(int.Parse(substring + "04"));
        //CanEquipItem4.transform.FindChild("SpriteGrade").FindChild("SpriteItem").GetComponent<UISprite>().spriteName = substring + "04";

        MyGrid.GetComponent<UIGrid>().cellHeight = 170 * (i / 3f + 0.8f);
        MyGridEquip.GetComponent<UIGrid>().Reposition();
    }

    public void SetWearPart()
    {
        foreach (var item in CharacterRecorder.instance.ownedHeroList)
        {
            int i = 0;
            foreach (var h in item.equipList)
            {
                if (h.equipCode > 0)
                {
                    string itemCode = h.equipCode.ToString();
                    switch (_Type)
                    {
                        case 1:
                            //Debug.LogError(itemCode + "  *******  " + itemCode.Substring(1,1) + "    ********    " + itemCode.Substring(2,1));
                            if (_AttType == 1 && itemCode.Substring(1, 1) == "0" && itemCode.Substring(2, 1) == _Position.ToString())
                            {
                                //Debug.LogError(111111111111111111);
                                GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                                go.name = "EquipItem" + h.equipCode;
                                go.AddComponent<RoleEquipItem>();
                                go.GetComponent<RoleEquipItem>().Init(h.equipCode, h.equipClass, 1, h.equipID, i);
                                go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                                ListEquipItem.Add(go);
                                i++;
                            }
                            else if (_AttType == 2 && itemCode.Substring(1, 1) == "3" && itemCode.Substring(2, 1) == _Position.ToString())
                            {
                                GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                                go.name = "EquipItem" + h.equipCode;
                                go.AddComponent<RoleEquipItem>();
                                go.GetComponent<RoleEquipItem>().Init(h.equipCode, h.equipClass, 1, h.equipID, i);
                                go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                                ListEquipItem.Add(go);
                                i++;
                            }
                            break;
                        case 2:
                            if (_AttType == 1 && itemCode.Substring(1, 1) == "1" && itemCode.Substring(2, 1) == _Position.ToString())
                            {
                                GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                                go.name = "EquipItem" + h.equipCode;
                                go.AddComponent<RoleEquipItem>();
                                go.GetComponent<RoleEquipItem>().Init(h.equipCode, h.equipClass, 1, h.equipID, i);
                                go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                                ListEquipItem.Add(go);
                                i++;
                            }
                            else if (_AttType == 2 && itemCode.Substring(1, 1) == "4" && itemCode.Substring(2, 1) == _Position.ToString())
                            {
                                GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                                go.name = "EquipItem" + h.equipCode;
                                go.AddComponent<RoleEquipItem>();
                                go.GetComponent<RoleEquipItem>().Init(h.equipCode, h.equipClass, 1, h.equipID, i);
                                go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                                ListEquipItem.Add(go);
                                i++;
                            }
                            break;
                        case 3:
                            if (_AttType == 1 && itemCode.Substring(1, 1) == "2" && itemCode.Substring(2, 1) == _Position.ToString())
                            {
                                GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                                go.name = "EquipItem" + h.equipCode;
                                go.AddComponent<RoleEquipItem>();
                                go.GetComponent<RoleEquipItem>().Init(h.equipCode, h.equipClass, 1, h.equipID, i);
                                go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                                ListEquipItem.Add(go);
                                i++;
                            }
                            else if (_AttType == 2 && itemCode.Substring(1, 1) == "5" && itemCode.Substring(2, 1) == _Position.ToString())
                            {
                                GameObject go = NGUITools.AddChild(MyGridEquip, MyEquipItem);
                                go.name = "EquipItem" + h.equipCode;
                                go.AddComponent<RoleEquipItem>();
                                go.GetComponent<RoleEquipItem>().Init(h.equipCode, h.equipClass, 1, h.equipID, i);
                                go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("EquipScrollView").GetComponent<UIScrollView>();

                                ListEquipItem.Add(go);
                                i++;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        CanEquipItem1.SetActive(false);
        CanEquipItem2.SetActive(false);
        CanEquipItem3.SetActive(false);
        CanEquipItem4.SetActive(false);
    }
}

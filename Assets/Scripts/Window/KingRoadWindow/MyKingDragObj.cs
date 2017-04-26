using UnityEngine;
using System.Collections;

public class MyKingDragObj : UIDragDropItem
{
    //public GameObject point;
    //public GameObject Mask;
    //public int CharacterRoleID;

    //Transform mTrans;
    //GameObject _Surface;
    //string CharactID;
    //private Transform ParentObj;
    public GameObject ParentObj;
    //protected override void OnDragStart()
    //{
    //    base.OnDragStart();
    //    //this.ParentObj = this.gameObject.transform.parent;
    //}

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        //_Surface = surface;
        //Debug.LogError(surface.name+"   " +this.gameObject.name);

        if (!cloneOnDrag)
        {
            //surface.name == "SpriteItem1" || surface.name == "SpriteItem2" || surface.name == "SpriteItem3" || surface.name == "SpriteItem4" ||surface.name == "SpriteItem5"
            if (surface.tag=="CanMove")//成为子物体
            {
                GameObject clone = NGUITools.AddChild(surface, ParentObj);
                clone.transform.parent = surface.transform;
                clone.transform.localPosition = Vector3.zero;
                clone.transform.localScale = Vector3.one;
                clone.name = this.ParentObj.name;
                clone.transform.Find(this.ParentObj.name).localPosition = Vector3.zero;
                clone.GetComponent<BoxCollider>().enabled = false;
                clone.transform.Find(clone.name).GetComponent<BoxCollider>().enabled = false;
                clone.transform.Find(clone.name).GetComponent<MyKingDragObj>().enabled = false;
                //this.gameObject.GetComponent<MyKingDragObj>().cloneOnDrag = true;
                surface.GetComponent<BoxCollider>().enabled = false;


                KingRoadFightWindow KW = GameObject.Find("KingRoadFightWindow").GetComponent<KingRoadFightWindow>();

                for (int i = 0; i < 5; i++) 
                {
                    if (surface.name == KW.MyHeroArr[i].name) 
                    {
                        KW.MyFightArr[i].SetActive(true);
                        for (int j = 0; j < CharacterRecorder.instance.ownedHeroList.size; j++)
                        {
                            if (CharacterRecorder.instance.ownedHeroList[j].cardID.ToString() == clone.name)
                            {
                                KW.MyFightArr[i].transform.Find("PowerNumber").GetComponent<UILabel>().text = CharacterRecorder.instance.ownedHeroList[j].force.ToString();
                                break;
                            }
                        }
                        break;
                    }
                }

                for (int i = KW.HeadHeroList.Count-1; i>=0; i--)
                {
                    if (this.ParentObj.name == KW.HeadHeroList[i].name) 
                    {
                        Destroy(this.ParentObj);
                        KW.HeadHeroList.RemoveAt(i); 
                    }
                }
                Destroy(this.gameObject);
                Debug.LogError(KW.HeadHeroList.Count);
                KW.HeadGrid.GetComponent<UIGrid>().Reposition();
            }
            else
            {
                this.transform.parent = ParentObj.transform;
                this.transform.localPosition = Vector3.zero;
                this.transform.localScale = Vector3.one;
            }
        }
    }
/*    protected override void OnDragEnd()
    {
        base.OnDragEnd();
        if (cloneOnDrag)
        {
            KingRoadFightWindow KW = GameObject.Find("KingRoadFightWindow").GetComponent<KingRoadFightWindow>();
            for (int i = 0; i < KW.MyHeroArr.Length - 1; i++) 
            {
                
            }
                if (count < 5)//队列允许5英雄角色
                {
                    if (_Surface.name == "SpriteAvatarBG0" || _Surface.name == "SpriteAvatarBG1" || _Surface.name == "SpriteAvatarBG2" || _Surface.name == "SpriteAvatarBG3" ||
                        _Surface.name == "SpriteAvatarBG4" || _Surface.name == "SpriteAvatarBG5" || _Surface.name == "SpriteAvatarBG6" || _Surface.name == "SpriteAvatarBG7" ||
                        _Surface.name == "SpriteAvatarBG8" || _Surface.name == "SpriteAvatarBG9" || _Surface.name == "SpriteAvatarBG10" || _Surface.name == "SpriteAvatarBG11" || _Surface.name == "SpriteAvatarBG12")
                    {

                        GameObject clone = NGUITools.AddChild(_Surface, this.gameObject);
                        clone.transform.localPosition = Vector3.zero;
                        clone.transform.localScale = Vector3.one;
                        clone.name = CharacterRoleID.ToString();
                        clone.GetComponent<UISprite>().enabled = true;
                        clone.GetComponent<BoxCollider>().enabled = true;
                        clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                        clone.GetComponent<MyDragObj>().enabled = true;
                        clone.GetComponent<MyDragObj>().cloneOnDrag = false;
                        clone.GetComponent<RankCaptain>().enabled = true;
                        clone.transform.Find("LabelCaptain").GetComponent<UILabel>().enabled = true;
                        clone.tag = "lineup";
                        Mask.SetActive(true);
                        point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<UISprite>().color = Color.gray;
                        point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().size = Vector3.zero;
                        //point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().enabled = false;//yy

                    }
                    else if (_Surface.tag == "lineup")
                    {
                        GameObject clone = NGUITools.AddChild(_Surface.transform.parent.gameObject, this.gameObject);
                        clone.transform.localPosition = Vector3.zero;
                        clone.transform.localScale = Vector3.one;
                        clone.name = CharacterRoleID.ToString();
                        clone.GetComponent<UISprite>().enabled = true;
                        clone.GetComponent<BoxCollider>().enabled = true;
                        clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                        clone.GetComponent<MyDragObj>().enabled = true;
                        clone.GetComponent<MyDragObj>().cloneOnDrag = false;
                        clone.GetComponent<RankCaptain>().enabled = true;

                        clone.transform.Find("LabelCaptain").GetComponent<UILabel>().enabled = true;
                        clone.tag = "lineup";

                        Mask.SetActive(true);
                        point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<UISprite>().color = Color.gray;
                        point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().size = Vector3.zero;
                        //point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().enabled = false;//yy

                        MyDragObj mdo = _Surface.GetComponent<MyDragObj>();
                        mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<UISprite>().color = Color.white;
                        mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<BoxCollider>().size = new Vector3(50, 50, 50);
                        mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<BoxCollider>().enabled = true;
                        mdo.point.transform.FindChild("Mask").gameObject.SetActive(false);
                        Destroy(_Surface);
                    }
                }
                else if (_Surface.tag == "lineup")
                {
                    UIManager.instance.OpenPromptWindow("最多五名英雄上阵", PromptWindow.PromptType.Hint, null, null);
                    GameObject clone = NGUITools.AddChild(_Surface.transform.parent.gameObject, this.gameObject);
                    clone.transform.localPosition = Vector3.zero;
                    clone.transform.localScale = Vector3.one;
                    clone.name = CharacterRoleID.ToString();
                    clone.GetComponent<UISprite>().enabled = true;
                    clone.GetComponent<BoxCollider>().enabled = true;
                    clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                    clone.GetComponent<MyDragObj>().enabled = true;
                    clone.GetComponent<MyDragObj>().cloneOnDrag = false;
                    clone.GetComponent<RankCaptain>().enabled = true;

                    clone.transform.Find("LabelCaptain").GetComponent<UILabel>().enabled = true;
                    clone.tag = "lineup";

                    Mask.SetActive(true);
                    point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<UISprite>().color = Color.gray;
                    point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().size = Vector3.zero;
                    //point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().enabled = false;//yy

                    MyDragObj mdo = _Surface.GetComponent<MyDragObj>();
                    mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<UISprite>().color = Color.white;
                    mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<BoxCollider>().size = new Vector3(50, 50, 50);
                    mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<BoxCollider>().enabled = true;
                    mdo.point.transform.FindChild("Mask").gameObject.SetActive(false);
                    Destroy(_Surface);
                }
        }
    }*/
}

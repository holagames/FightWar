using UnityEngine;
using System.Collections;

public class LegionDragObj : UIDragDropItem
{

    public GameObject point;
    public GameObject Mask;
    public int CharacterRoleID;

    Transform mTrans;
    GameObject _Surface;
    string CharactID;

    protected override void OnDragStart()
    {
        base.OnDragStart();
        mTrans = this.gameObject.transform.parent;
        if (this.gameObject.name == mTrans.name)
        {
            this.gameObject.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        }
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        _Surface = surface;

        if (!cloneOnDrag)
        {
            if (surface.name == "SpriteAvatarBG0" || surface.name == "SpriteAvatarBG1" || surface.name == "SpriteAvatarBG2" || surface.name == "SpriteAvatarBG3" ||
                surface.name == "SpriteAvatarBG4" || surface.name == "SpriteAvatarBG5" || surface.name == "SpriteAvatarBG6" || surface.name == "SpriteAvatarBG7" ||
                surface.name == "SpriteAvatarBG8" || surface.name == "SpriteAvatarBG9" || surface.name == "SpriteAvatarBG10" || surface.name == "SpriteAvatarBG11" ||
                surface.name == "SpriteAvatarBG12")//成为子物体
            {
                this.transform.parent = surface.transform;
                this.transform.localPosition = Vector3.zero;
                this.transform.localScale = Vector3.one;
                GameObject rpw = GameObject.Find("RankPositionWindow");
            }
            else if (surface.tag == "lineup")//交换
            {
                Transform parent = surface.transform.parent;
                surface.transform.parent = this.transform.parent;
                surface.transform.localPosition = Vector3.zero;
                this.transform.parent = parent;
                this.transform.localPosition = Vector3.zero;
            }
            else
            {

                if (GetChildNum() > 1)//GetChildNum() > 1
                {
                    this.transform.parent = point.transform;    //返回
                    this.transform.localPosition = Vector3.zero;
                    this.transform.localScale = Vector3.one;
                    Destroy(gameObject);
                    Mask.SetActive(false);
                    point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<UISprite>().color = Color.white;
                    point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().enabled = true;//yy
                    point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().size = new Vector3(50, 50, 50);
                }
                else
                {
                    this.transform.parent = mParent;
                    this.transform.localPosition = Vector3.zero;
                    this.transform.localScale = Vector3.one;
                }

            }
        }

    }
    private int GetChildNum()
    {
        int count = 0;
        for (int i = 0; i < 13; i++)
        {
            if (GameObject.Find("SpriteAvatarBG" + i).transform.childCount > 0)
            {
                count++;
            }
        }
        return count;
    }
    protected override void OnDragEnd()
    {
        base.OnDragEnd();
        if (cloneOnDrag)
        {
            int count = 0;
            for (int i = 0; i < 13; i++)
            {
                if (GameObject.Find("SpriteAvatarBG" + i).transform.childCount > 0)
                {
                    count++;
                }
            }

            if (count < 6)//队列允许5英雄角色
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
                    clone.GetComponent<LegionDragObj>().enabled = true;
                    clone.GetComponent<LegionDragObj>().cloneOnDrag = false;
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
                    clone.GetComponent<LegionDragObj>().enabled = true;
                    clone.GetComponent<LegionDragObj>().cloneOnDrag = false;
                    clone.GetComponent<RankCaptain>().enabled = true;

                    clone.transform.Find("LabelCaptain").GetComponent<UILabel>().enabled = true;
                    clone.tag = "lineup";

                    Mask.SetActive(true);
                    point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<UISprite>().color = Color.gray;
                    point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().size = Vector3.zero;
                    //point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().enabled = false;//yy

                    LegionDragObj mdo = _Surface.GetComponent<LegionDragObj>();
                    mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<UISprite>().color = Color.white;
                    mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<BoxCollider>().size = new Vector3(50, 50, 50);
                    mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<BoxCollider>().enabled = true;
                    mdo.point.transform.FindChild("Mask").gameObject.SetActive(false);
                    Destroy(_Surface);
                }
            }
            else if (_Surface.tag == "lineup")
            {
                //UIManager.instance.OpenPromptWindow("最多六名英雄上阵", PromptWindow.PromptType.Hint, null, null);
                GameObject clone = NGUITools.AddChild(_Surface.transform.parent.gameObject, this.gameObject);
                clone.transform.localPosition = Vector3.zero;
                clone.transform.localScale = Vector3.one;
                clone.name = CharacterRoleID.ToString();
                clone.GetComponent<UISprite>().enabled = true;
                clone.GetComponent<BoxCollider>().enabled = true;
                clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                clone.GetComponent<LegionDragObj>().enabled = true;
                clone.GetComponent<LegionDragObj>().cloneOnDrag = false;
                clone.GetComponent<RankCaptain>().enabled = true;

                clone.transform.Find("LabelCaptain").GetComponent<UILabel>().enabled = true;
                clone.tag = "lineup";

                Mask.SetActive(true);
                point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<UISprite>().color = Color.gray;
                point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().size = Vector3.zero;
                //point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().enabled = false;//yy

                LegionDragObj mdo = _Surface.GetComponent<LegionDragObj>();
                mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<UISprite>().color = Color.white;
                mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<BoxCollider>().size = new Vector3(50, 50, 50);
                mdo.point.transform.FindChild(mdo.CharacterRoleID.ToString()).GetComponent<BoxCollider>().enabled = true;
                mdo.point.transform.FindChild("Mask").gameObject.SetActive(false);
                Destroy(_Surface);
            }
        }

        bool IsHaveRole = false;
        for (int i = 0; i < 13; i++)
        {
            if (GameObject.Find("SpriteAvatarBG" + i).transform.childCount > 0)
            {
                if (GameObject.Find("SpriteAvatarBG" + i).transform.GetChild(0).name == point.name)
                {
                    IsHaveRole = true;
                    break;
                }

            }
        }
        if (!IsHaveRole)
        {
            point.transform.FindChild(CharacterRoleID.ToString()).GetComponent<BoxCollider>().enabled = true;
            //point.transform.FindChild("Mask").gameObject.SetActive(false);
        }
    }
}

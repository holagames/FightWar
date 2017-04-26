using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TacticsDragObj: UIDragDropItem
{
    public GameObject point;
    //public GameObject Mask;
    public int skillID;

    GameObject _Surface;
    string CharactID;

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        _Surface = surface;
        


        if (!cloneOnDrag)
        {
            //Debug.LogError(surface.name + "   " + this.gameObject.name);
            if (surface.name == "MySkill1" || surface.name == "MySkill2" || surface.name == "MySkill3")
            {
                this.transform.parent = surface.transform;
                this.transform.localPosition = Vector3.zero;
                this.transform.localScale = Vector3.one;
            }
            else if (surface.tag == "lineup")
            {
                Transform parent = surface.transform.parent;
                surface.transform.parent = this.transform.parent;
                surface.transform.localPosition = Vector3.zero;
                this.transform.parent = parent;
                this.transform.localPosition = Vector3.zero;
            }
            else
            {
                this.transform.parent = point.transform;
                this.transform.localPosition = Vector3.zero;
                this.transform.localScale = Vector3.one;
                Destroy(gameObject);
                //Mask.SetActive(false);
                point.transform.FindChild(skillID.ToString()).GetComponent<UISprite>().color = Color.white;
                point.transform.FindChild(skillID.ToString()).GetComponent<BoxCollider>().size = new Vector3(80, 80, 80);

            }
        }
    }

    protected override void OnDragEnd()
    {
        base.OnDragEnd();
        if (cloneOnDrag)
        {
           /* int count = 0;
            for (int i = 1; i < 3; i++)
            {
                if (GameObject.Find("MySkill" + i).transform.childCount > 0)
                {
                    count++;
                }
            } */

            //if (count < 3)
            Debug.Log(_Surface.name);
            if (_Surface.transform.FindChild("Icon") == null)
            {
                return;
            }
            TacticsWindow _TacticsWindow = GameObject.Find("TacticsWindow").GetComponent<TacticsWindow>();
            if (_Surface.transform.FindChild("Icon").GetComponent<UISprite>().spriteName != "ui4icon2")
            {
                if (_Surface.name == "MySkill1" || _Surface.name == "MySkill2" || _Surface.name == "MySkill3")
                {
                    GameObject clone = NGUITools.AddChild(_Surface, this.gameObject);
                    clone.transform.localPosition = Vector3.zero;
                    clone.transform.localScale = Vector3.one;
                    clone.name = skillID.ToString();
                    clone.GetComponent<UISprite>().enabled = true;
                    clone.GetComponent<UISprite>().MakePixelPerfect();
                    /*clone.GetComponent<BoxCollider>().enabled = true;
                    clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                    clone.GetComponent<TacticsDragObj>().enabled = true;
                    clone.GetComponent<TacticsDragObj>().cloneOnDrag = false; */
                    clone.GetComponent<BoxCollider>().enabled = false;
                    clone.tag = "lineup";
                    //Mask.SetActive(true);
                    List<GameObject> _tacticsItemList = _TacticsWindow.tacticsItemList;
                    for (int i = 0; i < _tacticsItemList.Count;i++ )
                    {
                        _tacticsItemList[i].GetComponent<TacticsItem>().SetColorBackToNormal();
                    }
                    point.transform.FindChild(skillID.ToString()).GetComponent<UISprite>().color = Color.gray;
                    point.transform.FindChild(skillID.ToString()).GetComponent<BoxCollider>().size = Vector3.zero;
                }
                switch(_Surface.name)
                {
                    case "MySkill1": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", skillID, _TacticsWindow.myOwnTacticList[1], _TacticsWindow.myOwnTacticList[2])); break;
                    case "MySkill2": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], skillID, _TacticsWindow.myOwnTacticList[2])); break;
                    case "MySkill3": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], _TacticsWindow.myOwnTacticList[1], skillID)); break;
                }
            }           
            else if (_Surface.tag == "lineup")
            {              
                
                GameObject clone = NGUITools.AddChild(_Surface.transform.parent.gameObject, this.gameObject);
                clone.transform.localPosition = Vector3.zero;
                clone.transform.localScale = Vector3.one;
                clone.name = skillID.ToString();
                clone.GetComponent<UISprite>().enabled = true;
                clone.GetComponent<BoxCollider>().enabled = true;
                clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                clone.GetComponent<TacticsDragObj>().enabled = true;
                clone.GetComponent<TacticsDragObj>().cloneOnDrag = false;
                clone.tag = "lineup";
                
                //.SetActive(true);
                point.transform.FindChild(skillID.ToString()).GetComponent<UISprite>().color = Color.gray;
                point.transform.FindChild(skillID.ToString()).GetComponent<BoxCollider>().size = Vector3.zero;

                TacticsDragObj mdo = _Surface.GetComponent<TacticsDragObj>();
                mdo.point.transform.FindChild(mdo.skillID.ToString()).GetComponent<UISprite>().color = Color.white;
                mdo.point.transform.FindChild(mdo.skillID.ToString()).GetComponent<BoxCollider>().size = new Vector3(80, 80, 80);
                Destroy(_Surface);
            }

        }
    }


}

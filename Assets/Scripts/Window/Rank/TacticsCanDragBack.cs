using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TacticsCanDragBack: UIDragDropItem
{

    public GameObject point;
    public int skillID;

    Transform mTrans;
    GameObject _Surface;
    string CharactID;
    //protected override void StartDragging()
    //{
    //    base.StartDragging();
    //    if (cloneOnDrag)
    //    {
    //        GameObject clone = NGUITools.AddChild(transform.parent.gameObject, gameObject);
    //        clone.transform.localPosition = transform.localPosition;
    //        clone.transform.localRotation = transform.localRotation;
    //        clone.transform.localScale = transform.localScale;
    //        clone.name = "啦啦啦啦";
    //    }
    //}

    protected override void OnDragStart()
    {
        base.OnDragStart();
        mTrans = this.gameObject.transform.parent;
        if (this.gameObject.name == mTrans.name)
        {
            this.gameObject.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        }
    }
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        _Surface = surface;
       // Debug.LogError(surface.name+"   " +this.gameObject.name);


        if (!cloneOnDrag)
        {
            if (surface.name == "MySkill1" || surface.name == "MySkill2" || surface.name == "MySkill3")
            {
                if (_Surface.transform.FindChild("Icon") != null && _Surface.transform.FindChild("Icon").GetComponent<UISprite>().spriteName != "ui4icon2")//没锁
                {
                    TacticsWindow _TacticsWindow = GameObject.Find("TacticsWindow").GetComponent<TacticsWindow>();
                    switch (surface.name)
                    {
                        case "MySkill1":
                            switch (this.transform.parent.name)
                            {
                                case "MySkill2": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", this.name, 0, _TacticsWindow.myOwnTacticList[2])); break;
                                case "MySkill3": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", this.name, _TacticsWindow.myOwnTacticList[1], 0)); break;
                            }
                            break;
                        case "MySkill2":
                            switch (this.transform.parent.name)
                            {
                                case "MySkill1": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", 0, this.name, _TacticsWindow.myOwnTacticList[2])); break;
                                case "MySkill3": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], this.name, 0)); break;
                            }
                            break;
                        case "MySkill3":
                            switch (this.transform.parent.name)
                            {
                                case "MySkill1": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", 0, _TacticsWindow.myOwnTacticList[1], this.name)); break;
                                case "MySkill2": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], 0, this.name)); break;
                            }
                            break;
                    }
                    //Debug.LogError("已使用战术 移到 另一个位置");
                    this.transform.parent = surface.transform;
                    this.transform.localPosition = Vector3.zero;
                    this.transform.localScale = Vector3.one;
                }
                else
                {
                    //有锁
                    this.transform.localPosition = Vector3.zero;
                }
            }
            else if (surface.tag == "lineup")
            {
                TacticsWindow _TacticsWindow = GameObject.Find("TacticsWindow").GetComponent<TacticsWindow>();
                switch (surface.transform.parent.name)
                {
                    case "MySkill1":
                        switch (this.transform.parent.name)
                        {
                            case "MySkill2": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", this.name, surface.name, _TacticsWindow.myOwnTacticList[2])); break;
                            case "MySkill3": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", this.name, _TacticsWindow.myOwnTacticList[1], surface.name)); break;
                        }
                        break;
                    case "MySkill2":
                        switch (this.transform.parent.name)
                        {
                            case "MySkill1": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", surface.name, this.name, _TacticsWindow.myOwnTacticList[2])); break;
                            case "MySkill3": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], this.name, surface.name)); break;
                        }
                        break;
                    case "MySkill3":
                        switch (this.transform.parent.name)
                        {
                            case "MySkill1": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", surface.name,_TacticsWindow.myOwnTacticList[1], this.name)); break;
                            case "MySkill2": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], surface.name ,this.name)); break;
                        }
                        break;
                }
                //Debug.LogError("已使用战术交换");
                Transform parent = surface.transform.parent;
                surface.transform.parent = this.transform.parent;
                surface.transform.localPosition = Vector3.zero;
                this.transform.parent = parent;
                this.transform.localPosition = Vector3.zero;
            }
            else
            {
                //Debug.LogError("拖回去前 父物体 name ..." + this.transform.parent.name);
                TacticsWindow _TacticsWindow = GameObject.Find("TacticsWindow").GetComponent<TacticsWindow>();
                switch (this.transform.parent.name)
                {
                    case "MySkill1": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", 0, _TacticsWindow.myOwnTacticList[1], _TacticsWindow.myOwnTacticList[2])); break;
                    case "MySkill2": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], 0, _TacticsWindow.myOwnTacticList[2])); break;
                    case "MySkill3": NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], _TacticsWindow.myOwnTacticList[1],0)); break;
                }

                this.transform.parent = point.transform;
                this.transform.localPosition = Vector3.zero;
                this.transform.localScale = Vector3.one;
                Destroy(gameObject);
                GameObject skillIconObj = point.transform.FindChild(skillID.ToString()).gameObject;
                skillIconObj.GetComponent<UISprite>().color = Color.white;
                skillIconObj.GetComponent<BoxCollider>().size = new Vector3(100, 100, 100);
                skillIconObj.GetComponent<BoxCollider>().enabled = true;
            }
        }
        
        //else
        //{            
        //    this.transform.localPosition = Vector3.zero;
        //    this.transform.localScale = Vector3.one;
        //}
    }

    protected override void OnDragEnd()
    {
        base.OnDragEnd();
        if (cloneOnDrag)
        {
            point.transform.FindChild(skillID.ToString()).GetComponent<BoxCollider>().enabled = true;
            Debug.Log(_Surface.name);
            TacticsWindow _TacticsWindow = GameObject.Find("TacticsWindow").GetComponent<TacticsWindow>();
            if (_Surface.transform.FindChild("Icon") != null && _Surface.transform.FindChild("Icon").GetComponent<UISprite>().spriteName != "ui4icon2")//没锁
            {
                if (_Surface.name == "MySkill1" || _Surface.name == "MySkill2" || _Surface.name == "MySkill3")
                {

                    GameObject clone = NGUITools.AddChild(_Surface, this.gameObject);
                    clone.transform.localPosition = Vector3.zero;
                    clone.transform.localScale = Vector3.one;
                    clone.name = skillID.ToString();
                    clone.GetComponent<UISprite>().enabled = true;
                    clone.GetComponent<UISprite>().MakePixelPerfect();
                    clone.GetComponent<BoxCollider>().enabled = true;
                    clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                    clone.GetComponent<TacticsCanDragBack>().enabled = true;
                    clone.GetComponent<TacticsCanDragBack>().cloneOnDrag = false;
                    clone.tag = "lineup";

                  /*  List<GameObject> _tacticsItemList = _TacticsWindow.tacticsItemList;
                    for (int i = 0; i < _tacticsItemList.Count; i++)
                    {
                        int _mySkillId = _tacticsItemList[i].GetComponent<TacticsItem>()._ManualSkill.skillID;
                        if (_mySkillId != skillID)
                        {
                            _tacticsItemList[i].GetComponent<TacticsItem>().SetColorBackToNormal();
                        }
                        else
                        {
                        }
                       // _tacticsItemList[i].GetComponent<TacticsItem>().SetColorBackToNormal();
                    }*/
                  //  point.transform.FindChild(skillID.ToString()).GetComponent<UISprite>().color = Color.gray;
                  //  point.transform.FindChild(skillID.ToString()).GetComponent<BoxCollider>().size = Vector3.zero;
                }
                int _comapreTarget1 = 0;
                int _comapreTarget2 = 0;
                switch (_Surface.name)
                {
                    case "MySkill1":
                        _comapreTarget1 = _TacticsWindow.myOwnTacticList[1];
                        _comapreTarget2 = _TacticsWindow.myOwnTacticList[2];
                        NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", skillID, _TacticsWindow.myOwnTacticList[1], _TacticsWindow.myOwnTacticList[2])); break;
                    case "MySkill2": 
                        _comapreTarget1 = _TacticsWindow.myOwnTacticList[0];
                        _comapreTarget2 = _TacticsWindow.myOwnTacticList[2];
                        NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], skillID, _TacticsWindow.myOwnTacticList[2]));
                        if (CharacterRecorder.instance.GuideID[29] == 6)
                        {
                            CharacterRecorder.instance.GuideID[29] += 1;
                            StartCoroutine(SceneTransformer.instance.NewbieGuide());
                        }
                        break;
                    case "MySkill3": 
                        _comapreTarget1 = _TacticsWindow.myOwnTacticList[0];
                        _comapreTarget2 = _TacticsWindow.myOwnTacticList[1];
                        NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], _TacticsWindow.myOwnTacticList[1], skillID)); break;
                }
                List<GameObject> _tacticsItemList = _TacticsWindow.tacticsItemList;
                for (int i = 0; i < _tacticsItemList.Count; i++)
                {
                    int _mySkillId = _tacticsItemList[i].GetComponent<TacticsItem>()._ManualSkill.skillID;
                    if (_mySkillId != skillID && _mySkillId != _comapreTarget1 && _mySkillId != _comapreTarget2)
                    {
                        _tacticsItemList[i].GetComponent<TacticsItem>().SetColorBackToNormal();
                    }
                }
            }           
            else if (_Surface.tag == "lineup")
            {
             //   Debug.LogError("lineup?????");
                GameObject clone = NGUITools.AddChild(_Surface.transform.parent.gameObject, this.gameObject);
                clone.transform.localPosition = Vector3.zero;
                clone.transform.localScale = Vector3.one;
                clone.name = skillID.ToString();
                clone.GetComponent<UISprite>().enabled = true;
                clone.GetComponent<UISprite>().MakePixelPerfect();
                clone.GetComponent<BoxCollider>().enabled = true;
                clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                clone.GetComponent<TacticsCanDragBack>().enabled = true;
                clone.GetComponent<TacticsCanDragBack>().cloneOnDrag = false;
                clone.tag = "lineup";
              /*  List<GameObject> _tacticsItemList = _TacticsWindow.tacticsItemList;
                for (int i = 0; i < _tacticsItemList.Count; i++)
                {
                    int _mySkillId = _tacticsItemList[i].GetComponent<TacticsItem>()._ManualSkill.skillID;
                    if (_mySkillId != skillID )
                    {
                        _tacticsItemList[i].GetComponent<TacticsItem>().SetColorBackToNormal();
                    }
                   // _tacticsItemList[i].GetComponent<TacticsItem>().SetColorBackToNormal();
                }*/
              //  point.transform.FindChild(skillID.ToString()).GetComponent<UISprite>().color = Color.gray;
              //  point.transform.FindChild(skillID.ToString()).GetComponent<BoxCollider>().size = Vector3.zero;
                int _comapreTarget1 = 0;
                int _comapreTarget2 = 0;
                switch (_Surface.transform.parent.name)
                {
                    case "MySkill1":
                        _comapreTarget1 = _TacticsWindow.myOwnTacticList[1];
                        _comapreTarget2 = _TacticsWindow.myOwnTacticList[2];
                        NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", skillID, _TacticsWindow.myOwnTacticList[1], _TacticsWindow.myOwnTacticList[2])); break;
                    case "MySkill2": 
                        _comapreTarget1 = _TacticsWindow.myOwnTacticList[0];
                        _comapreTarget2 = _TacticsWindow.myOwnTacticList[2];
                        NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], skillID, _TacticsWindow.myOwnTacticList[2])); break;
                    case "MySkill3": 
                        _comapreTarget1 = _TacticsWindow.myOwnTacticList[0];
                        _comapreTarget2 = _TacticsWindow.myOwnTacticList[1];
                        NetworkHandler.instance.SendProcess(string.Format("1032#{0}${1}${2};", _TacticsWindow.myOwnTacticList[0], _TacticsWindow.myOwnTacticList[1], skillID)); break;
                }
                List<GameObject> _tacticsItemList = _TacticsWindow.tacticsItemList;
                for (int i = 0; i < _tacticsItemList.Count; i++)
                {
                    int _mySkillId = _tacticsItemList[i].GetComponent<TacticsItem>()._ManualSkill.skillID;
                    if (_mySkillId != skillID && _mySkillId != _comapreTarget1 && _mySkillId != _comapreTarget2)
                    {
                        _tacticsItemList[i].GetComponent<TacticsItem>().SetColorBackToNormal();
                    }
                }

               // Debug.LogError("_SurfaceName.." + _Surface.name);
                Destroy(_Surface);
                /*TacticsCanDragBack _TacticsCanDragBack = _Surface.GetComponent<TacticsCanDragBack>();
                _TacticsCanDragBack.point.transform.FindChild(_TacticsCanDragBack.skillID.ToString()).GetComponent<UISprite>().color = Color.white;
                _TacticsCanDragBack.point.transform.FindChild(_TacticsCanDragBack.skillID.ToString()).GetComponent<BoxCollider>().size = new Vector3(80, 80, 80);
                Destroy(_Surface);*/
            }

        }
    }


}

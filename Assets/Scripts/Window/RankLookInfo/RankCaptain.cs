using UnityEngine;
using System.Collections;

public class RankCaptain : MonoBehaviour {

    private GameObject parent;
	void Start () {
        if (UIEventListener.Get(this.gameObject).onClick == null && GameObject.Find("RankPositionWindow")!=null) 
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                parent=this.gameObject.transform.parent.gameObject;
                if (parent.name == "SpriteAvatarBG0" || parent.name == "SpriteAvatarBG1" || parent.name == "SpriteAvatarBG2" || parent.name == "SpriteAvatarBG3" ||
                parent.name == "SpriteAvatarBG4" || parent.name == "SpriteAvatarBG5" || parent.name == "SpriteAvatarBG6" || parent.name == "SpriteAvatarBG7" ||
                parent.name == "SpriteAvatarBG8" || parent.name == "SpriteAvatarBG9" || parent.name == "SpriteAvatarBG10" || parent.name == "SpriteAvatarBG11" ||
                parent.name == "SpriteAvatarBG12") 
                {
                    GameObject rpw = GameObject.Find("RankPositionWindow");
                    rpw.GetComponent<RankPositionWindow>().SetCaptain(this.gameObject.name);
                }
            };  
        }

        if (UIEventListener.Get(this.gameObject).onClick == null && GameObject.Find("LegionPositionWindow") != null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                parent = this.gameObject.transform.parent.gameObject;
                if (parent.name == "SpriteAvatarBG0" || parent.name == "SpriteAvatarBG1" || parent.name == "SpriteAvatarBG2" || parent.name == "SpriteAvatarBG3" ||
                parent.name == "SpriteAvatarBG4" || parent.name == "SpriteAvatarBG5" || parent.name == "SpriteAvatarBG6" || parent.name == "SpriteAvatarBG7" ||
                parent.name == "SpriteAvatarBG8" || parent.name == "SpriteAvatarBG9" || parent.name == "SpriteAvatarBG10" || parent.name == "SpriteAvatarBG11" ||
                parent.name == "SpriteAvatarBG12")
                {
                    GameObject rpw = GameObject.Find("LegionPositionWindow");
                    rpw.GetComponent<LegionPositionWindow>().SetCaptain(this.gameObject.name);
                }
            };
        }
	}
	
}

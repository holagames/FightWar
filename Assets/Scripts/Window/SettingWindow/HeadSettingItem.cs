using UnityEngine;
using System.Collections;

public class HeadSettingItem : MonoBehaviour
{
    public UITexture myHeadTexture;
    private int cardIDHead;
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                HeadSettingWindow.selectHead = cardIDHead;
            };
        }
	}
    public void SetHeadSettingItem(Hero _mHero)
    {
        cardIDHead = _mHero.cardID;
        
        myHeadTexture.mainTexture = Resources.Load(string.Format("Head/{0}", _mHero.cardID), typeof(Texture)) as Texture;
        if (_mHero.cardID == CharacterRecorder.instance.headIcon)
        {
            this.GetComponent<UIToggle>().startsActive = true;
        }
    }

}

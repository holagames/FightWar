using UnityEngine;
using System.Collections;

public class WillOpenWindow: MonoBehaviour 
{
    public UILabel nameLabel;
    public UILabel desLevel;
    public UISprite spriteIcon;
    [SerializeField]
    private UITexture levelTexture;
    public GameObject maskButton;
	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(maskButton).onClick = delegate(GameObject go)
        {
            ClosePlane();
        };
        CreatLevelUpWillOpenItems();
	}
    void CreatLevelUpWillOpenItems()
    {
       // NewGuide _NewGuide = TextTranslator.instance.FindNewGuideNextWillOpenByCurLevel(CharacterRecorder.instance.level);
        NewGuide _NewGuide = TextTranslator.instance.FindNewGuideNextWillOpenByCurLevel(CharacterRecorder.instance.lastGateID - 1);
        if (_NewGuide == null)
        {
            return;
        }
        nameLabel.text = _NewGuide.Name;
        desLevel.text =  _NewGuide.MapDes.Replace("[$]", "\n");
        //spriteIcon.spriteName = _NewGuide.MainIcon;
        levelTexture.mainTexture = Resources.Load(string.Format("NewGuide/{0}", _NewGuide.MainIcon), typeof(Texture)) as Texture;
    }


    void ClosePlane()
    {
        //UIManager.instance.DestroyPanel("WillOpenWindow");
        UIManager.instance.BackUI();
    }

}

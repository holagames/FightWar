using UnityEngine;
using System.Collections;

public class LevelUpItem : MonoBehaviour 
{
    [SerializeField]
    private UILabel nameLabel;
    [SerializeField]
    private UILabel desLabel;
    [SerializeField]
    private UILabel levelLabel;
    [SerializeField]
    private UISprite levelIcon;
    [SerializeField]
    private UITexture levelTexture;
    [SerializeField]
    private GameObject hadOpenObj;
    [SerializeField]
    private GameObject hadOpenEffect;
	// Use this for initialization
	void Start () 
    {
	
	}
    public void SetLevelUpItem(int lastGateId,NewGuide _NewGuide)
    {
       // levelIcon.spriteName = _NewGuide.LevelUpTipIcon;
        levelTexture.mainTexture = Resources.Load(string.Format("NewGuide/{0}", _NewGuide.MainIcon), typeof(Texture)) as Texture;
        nameLabel.text = _NewGuide.Name;
        desLabel.text = _NewGuide.Des;
        int openLevel = _NewGuide.Level;
        string openStr = "";
      /*  if (CharacterRecorder.instance.level < openLevel)
        {
            openStr = string.Format("{0}级开启",openLevel);
            hadOpenObj.SetActive(false);
        }
        else 
        {
            //openStr = "已开启";
            openStr = string.Format("{0}级开启", openLevel);
            hadOpenObj.SetActive(true);
            Invoke("DelayShowHadOpenEffect", 2.0f);
        }*/
        //Debug.LogError(lastGateId);
        if (lastGateId > openLevel && SceneTransformer.instance.NowGateID <= openLevel)
        {
            //openStr = "已开启";
            //Debug.LogError(lastGateId + "  " + openLevel + "   " + CharacterRecorder.instance.lastGateID + "  " + SceneTransformer.instance.NowGateID);

            openStr = string.Format("通关第{0}关开启", openLevel - 10000);
            hadOpenObj.SetActive(true);
            Invoke("DelayShowHadOpenEffect", 2.0f);
        }
        else
        {
            openStr = string.Format("通关第{0}关开启", openLevel - 10000);
            hadOpenObj.SetActive(false);
        }
        levelLabel.text = openStr;
    }

    void DelayShowHadOpenEffect()
    {
        hadOpenEffect.layer = 11;
        foreach (Component c in hadOpenEffect.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }
        hadOpenEffect.SetActive(true);
    }
}

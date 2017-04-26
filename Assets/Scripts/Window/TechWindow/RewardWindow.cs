using UnityEngine;
using System.Collections;

public class RewardWindow : MonoBehaviour
{
    public GameObject UpButton;
    public GameObject CloseButton;
    public UILabel ItemDes;
    public UILabel TitelLabel;
   // public UILabel LevelDes;
    public UILabel ItemName;
    public GameObject LockLabel;
   // public GameObject ExpFrame;
    public GameObject ItemFrameObj;
    public GameObject NeedCostObj;
    public UILabel EXPlabel;
    public UILabel GoldCost;
    public UILabel DiamondCost;
    public UILabel MaxLabel;
    public UILabel DeblockingLabel;
    public int Nextid;
    private int lastLevel;
    private bool isMAX = false;
    public GameObject EffcetItem;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                GameObject.Find("TechWindow").transform.Find("RewardWindow").gameObject.SetActive(false);
            };
        }
        UIEventListener.Get(UpButton).onClick += delegate(GameObject go)
        {
            if (isMAX)
            {
                UIManager.instance.OpenPromptWindow("已经满级了,亲升级其它技能吧", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                if (CharacterRecorder.instance.GuideID[11] == 7)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                CharacterRecorder.instance.ChangeAttribute = true;
                NetworkHandler.instance.SendProcess("1602#" + Nextid + ";");
            }
        };

    }
    // Update is called once per frame
    public void GetTreeInfo(bool isLock, int ID, int PointLevel,int effcetid)
    {
        EffcetItem.SetActive(false);
        if (effcetid == 1)
        {
            EffcetItem.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_recieve");
        }
        TechTree TechTreeDic;
        if (PointLevel < 10)
        {
            TechTreeDic = TextTranslator.instance.GetTechTreerByIcon(ID, PointLevel + 1);
        }
        else
        {
            TechTreeDic = TextTranslator.instance.GetTechTreerByIcon(ID, PointLevel);//传入icon和等级
        }
        if (isLock)
        {
            ItemFrameObj.transform.Find("SpriteIcon").GetComponent<UISprite>().spriteName = "H" + ID.ToString();
            EXPlabel.gameObject.SetActive(false);
            LockLabel.SetActive(true);
            DeblockingLabel.gameObject.SetActive(true);
            ItemName.text = TechTreeDic.Name;
            TitelLabel.text = TechTreeDic.Name;
            NeedCostObj.SetActive(false);
            //LevelDes.text = "[00FFFF]等级:[-][ffffff]???";
            MaxLabel.gameObject.SetActive(false);
            UpButton.transform.Find("Background").gameObject.SetActive(false);
            //UpButton.transform.Find("Blackground").gameObject.SetActive(true);
            if (ID < 200 && ID != 100)
            {
                lastLevel = GameObject.Find("TechTree").GetComponent<TechTreeList>().ElemList[(ID % 100) - 1].GetComponent<TreeItem>().PointLevel;
            }
            else if (ID > 300)
            {
                if (ID < 304)
                {
                    lastLevel = GameObject.Find("TechTree").GetComponent<TechTreeList>().TruList[0].GetComponent<TreeItem>().PointLevel;
                }
                else
                {
                    lastLevel = GameObject.Find("TechTree").GetComponent<TechTreeList>().TruList[(ID % 100) - 3].GetComponent<TreeItem>().PointLevel;
                }
            }
            else if (ID > 200 && ID < 300)
            {
                if (ID < 204)
                {
                    lastLevel = GameObject.Find("TechTree").GetComponent<TechTreeList>().InterList[0].GetComponent<TreeItem>().PointLevel;
                }
                else
                {
                    lastLevel = GameObject.Find("TechTree").GetComponent<TechTreeList>().InterList[(ID % 100) - 3].GetComponent<TreeItem>().PointLevel;
                }
            }
            if (ID == 200 || ID == 300)
            {
                DeblockingLabel.text = "战队等级达到" + TechTreeDic.UnLockNeedLevel + "级\n\n前一情报树都升到7级";
            }
            else
            {
                DeblockingLabel.text = "战队等级达到" + TechTreeDic.UnLockNeedLevel + "级\n\n前一情报升到7级（" + lastLevel + "/7）";
            }
            ItemDes.text = TechTreeDic.NoOpenDescription;
        }
        else
        {
            ItemFrameObj.transform.Find("SpriteIcon").GetComponent<UISprite>().spriteName = ID.ToString();
            EXPlabel.gameObject.SetActive(true);
            LockLabel.SetActive(false);
            ItemName.text = TechTreeDic.Name;
            TitelLabel.text = TechTreeDic.Name;
            DeblockingLabel.gameObject.SetActive(false);
            EXPlabel.text = PointLevel.ToString() + "/10";
            //ExpFrame.transform.Find("SpriteIcon").GetComponent<UIScrollBar>().barSize = (float)PointLevel / 10;
           // LevelDes.text = "[00FFFF]等级:[-][ffffff]" + TechTreeDic.UnLockNeedLevel;
            if (PointLevel >= 10 && TechTreeDic.Level >= 10)
            {
                NeedCostObj.SetActive(false);
                //MaxLabel.gameObject.SetActive(true);
                //UpButton.transform.Find("Blackground").gameObject.SetActive(true);
                UpButton.transform.Find("Background").gameObject.SetActive(false);
                ItemDes.text = "该情报已经是最大等级";
                isMAX = true;
            }
            else
            {
                string GoldColor = "";
                string TechColor = "";
                NeedCostObj.SetActive(true);
                MaxLabel.gameObject.SetActive(false);
                if (CharacterRecorder.instance.gold >= TechTreeDic.GoldCost)
                {
                    GoldColor = "[28DF5E]";
                }
                else
                {
                    GoldColor = "[FB2D50]";
                }
                if (int.Parse(GameObject.Find("TechWindow").GetComponent<TechWindow>().HavePonitLabel.text) >= TechTreeDic.LevelUpNeedPoint)
                {
                    TechColor = "[28DF5E]";
                }
                else
                {
                    TechColor = "[FB2D50]";
                }
                GoldCost.text = GoldColor + TechTreeDic.GoldCost.ToString() + "[-]";
                DiamondCost.text = TechColor + (GameObject.Find("TechWindow").GetComponent<TechWindow>().HavePonitLabel.text).ToString() + "[-]"+"/"+TechTreeDic.LevelUpNeedPoint;
                UpButton.transform.Find("Background").gameObject.SetActive(true);
                //UpButton.transform.Find("Blackground").gameObject.SetActive(false);
                Nextid = TechTreeDic.TechTreeID;
                isMAX = false;
                ItemDes.text = TechTreeDic.Des;
            }
        }
        
    }
}

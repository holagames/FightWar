using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MailInfoBoard : MonoBehaviour 
{
    public UILabel Name;
    public UILabel Sender;
    public UILabel MailWord;
    public UILabel MailWordWithoutAttach;
    public GameObject getButton;
    public GameObject HaveGet;
    public GameObject wordPartWithoutAttach;
    public GameObject wordPartWithAttach;
    public GameObject attachPart;

    public GameObject uiGrid;
    public GameObject ItemParfeb;
    public UIAtlas ItemAtlas;
    public UIAtlas AvatarAtlas;

    List<GameObject> itemList = new List<GameObject>();
    private MailItemData oneMailItemData;
	// Use this for initialization
	void Start () 
    {
        
        NetworkHandler.instance.SendProcess(string.Format("9002#{0};", oneMailItemData.mailID));

        if (UIEventListener.Get(GameObject.Find("Closebutton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Closebutton")).onClick += delegate(GameObject obj)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    Destroy(itemList[i]);
                }
                //this.gameObject.SetActive(false);
                UIManager.instance.BackUI();
            };
        }
        UIEventListener.Get(getButton).onClick += delegate(GameObject obj)
        {
            NetworkHandler.instance.SendProcess("9003#" + oneMailItemData.mailID + ";");
        };
	}
    public void SetInfo(MailItemData _oneMailItemData)
    {
        //Debug.LogError("MailId..." + _oneMailItemData.mailID);
        oneMailItemData = _oneMailItemData;
        Name.text = _oneMailItemData.mailName;
        Sender.text = string.Format("{0}", _oneMailItemData.mailSender);
        switch (_oneMailItemData.itemIcon)
        {
            case 0:
                wordPartWithoutAttach.SetActive(true);
                wordPartWithoutAttach.transform.FindChild("SenderLabel").GetComponent<UILabel>().text = string.Format("{0}", _oneMailItemData.mailSender);
                //attachPart.SetActive(false);
                break;
            default:
                //attachPart.SetActive(true);
                break;
        }
        if (oneMailItemData.itemIcon == 0 && oneMailItemData.isRead == 0)
        {
            MailWindow.isFirstRead = true;
        }
    }


    public void SetMailBoardInfo(string word, List<AwardItem> _ListAwardItem)
    {
        if (_ListAwardItem.Count == 0)
        {
            //MailWordWithoutAttach.text = word;
            MailWordWithoutAttach.text = word.Replace("[$]", "\n");
            BoxCollider _BoxCollider = MailWordWithoutAttach.GetComponent<BoxCollider>();
            _BoxCollider.size = new Vector3(MailWordWithoutAttach.width, MailWordWithoutAttach.height, 0);
            _BoxCollider.center = new Vector3(MailWordWithoutAttach.width / 2, -MailWordWithoutAttach.height/2,0);
            wordPartWithoutAttach.SetActive(true);
            wordPartWithoutAttach.transform.FindChild("SenderLabel").GetComponent<UILabel>().text = string.Format("{0}", oneMailItemData.mailSender);
            wordPartWithAttach.SetActive(false);
           /* switch(oneMailItemData.isRead)
            {
                case 0: HaveGet.SetActive(false); getButton.SetActive(false); break;
                case 1: HaveGet.SetActive(true); getButton.SetActive(false); break;
            }*/
            if (MailWindow.isFirstRead)
            {
                HaveGet.SetActive(true); 
                getButton.SetActive(false);
                MailWindow mw = GameObject.Find("MailWindow").GetComponent<MailWindow>();
                mw.UpdateMail();
                MailWindow.isFirstRead = false;
            }
        }
        else
        {
            //MailWord.text = word;
            MailWord.text = word.Replace("[$]", "\n");
            BoxCollider _BoxCollider = MailWord.GetComponent<BoxCollider>();
            _BoxCollider.size = new Vector3(MailWord.width, MailWord.height, 0);
            _BoxCollider.center = new Vector3(MailWord.width / 2, -MailWord.height / 2, 0);
            wordPartWithoutAttach.SetActive(false);
            wordPartWithAttach.SetActive(true);
            //Debug.LogError("Count..." + _ListAwardItem.Count);
            for (int i = 0; i < _ListAwardItem.Count; i++)
            {
                CreatPrafeb(_ListAwardItem[i]);
            }
            switch (oneMailItemData.isGet)
            {
                case 0: HaveGet.SetActive(false);
                    getButton.SetActive(true);
                    break;
                case 1: HaveGet.SetActive(true);
                    getButton.SetActive(false);
                    break;
            }
        }
    }
    public void ResetSetMailBoardInfo()
    {
        HaveGet.SetActive(true);
        getButton.SetActive(false);
        UIManager.instance.BackUI();
    }
    void CreatPrafeb(AwardItem _AwardItem)
    {
        if (_AwardItem.itemCode != 0)
        {
            GameObject go = NGUITools.AddChild(uiGrid, ItemParfeb);
            go.name = _AwardItem.itemCode.ToString();
            itemList.Add(go);
            go.GetComponent<MailAwardItem>().SetMailAwardItem(_AwardItem);
            uiGrid.GetComponent<UIGrid>().Reposition();
        }
        
    }

    
}

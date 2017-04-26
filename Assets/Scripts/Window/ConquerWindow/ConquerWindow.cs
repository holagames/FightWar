using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ConquerWindow : MonoBehaviour
{
    public GameObject HarvestWindow;
    public GameObject CheckGateWindow;
    public GameObject Gate1;
    public GameObject Gate2;
    public GameObject Gate3;
    public GameObject BlackGate2;
    public GameObject BlackGate3;
    public UILabel BlackGate2UILabel;
    public UILabel BlackGate3UILabel;
    public GameObject Gate1Working;
    public GameObject Gate2Working;
    public GameObject Gate3Working;
    public GameObject GetButton;
    public GameObject CantGetButton;
    public GameObject GetButtonObj;
    public GameObject CantGetButtonObj;
    public GameObject QusButton;
    public GameObject QueMessage;
    public List<int> CaptureList = new List<int>();
    public List<int> HeroSelfList = new List<int>();
    public List<GameObject> CaptureObjList = new List<GameObject>();
    //生成模型
    public GameObject HeroItem;
    public GameObject HeroList;
    public GameObject HeroCamera;
    public int MovePosition;
    public int MoveNum = 0;//1大于11231 2小于7492
    public bool isZhuanShen = false;
    public int ZhuanShenid = 0;

    //
    public GameObject Gate2Clock;
    public GameObject Gate3Clock;
    //视察界面
    public GameObject InspectWindow;
    public GameObject InspectGrid;
    public GameObject InspectItem;
    // Use this for initialization
    public GameObject InspectButton;
    public UILabel InspectNumber;
    public GameObject InspectOKButton;
    public List<GameObject> InspectObjList = new List<GameObject>();
    //界面俘虏数量
    public UILabel CaptureNumber;
    public UILabel BeCapturedNumber;
    public GameObject RewardOne;
    public GameObject RewardTwo;
    public GameObject RewardThree;
    //
    public GameObject Gate1Effect;
    public GameObject Gate2Effect;
    //俘虏按钮
    public GameObject CounperButton;
    public GameObject CounperButtonObj;
    //
    public int CounperUpNumber = 0;
    void Start()
    {
        BlackGate2.SetActive(true);
        BlackGate3.SetActive(true);
        Gate2.SetActive(false);
        Gate3.SetActive(false);
        UIEventListener.Get(Gate1).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.GuideID[58] == 4)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            CharacterRecorder.instance.TabeID = 1;
            //Charaterrecorder.instance.TabeID = 1;
            NetworkHandler.instance.SendProcess("6502#1;");
            CheckGateWindow.SetActive(false);
            HarvestWindow.SetActive(true);
        };
        UIEventListener.Get(Gate2).onClick += delegate(GameObject go)
        {

            CharacterRecorder.instance.TabeID = 2;
            NetworkHandler.instance.SendProcess("6502#2;");
            CheckGateWindow.SetActive(false);
            HarvestWindow.SetActive(true);

        };
        UIEventListener.Get(BlackGate2).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPromptWindow(TextTranslator.instance.GetFortressByListId(1, 2).Level + "级开放", PromptWindow.PromptType.Hint, null, null);
        };
        UIEventListener.Get(BlackGate3).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPromptWindow(TextTranslator.instance.GetFortressByListId(1, 3).Level + "级开放", PromptWindow.PromptType.Hint, null, null);

        };
        UIEventListener.Get(Gate3).onClick += delegate(GameObject go)
        {
            CharacterRecorder.instance.TabeID = 3;
            NetworkHandler.instance.SendProcess("6502#3;");
            CheckGateWindow.SetActive(false);
            HarvestWindow.SetActive(true);
        };
        UIEventListener.Get(GetButton).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("6503#0;");
            GetButtonObj.SetActive(false);
            CantGetButtonObj.SetActive(true);
            GetButton.SetActive(false);
            CantGetButton.SetActive(true);
        };
        UIEventListener.Get(CantGetButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPromptWindow("暂时没有可以领取的收益", PromptWindow.PromptType.Hint, null, null);
        };
        UIEventListener.Get(InspectButton).onClick += delegate(GameObject go)
        {
            //InspectWindow.SetActive(true);
            //MessageInfo(InspectWindow);
            if (CaptureObjList.Count < CounperUpNumber)
            {

                HarvestWindow.GetComponent<HarvestWindow>().KengID = 0;
                CharacterRecorder.instance.TabeID = 0;
                NetworkHandler.instance.SendProcess("6015#");
                //NetworkHandler.instance.SendProcess("6002#");
                HarvestWindow.GetComponent<HarvestWindow>().ConquerListWindow.SetActive(true);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("俘虏已满", PromptWindow.PromptType.Hint, null, null);
            }
        };

        //  UIEventListener.Get(CounperButton).onClick += delegate(GameObject go)
        //{
        //    HarvestWindow.GetComponent<HarvestWindow>().KengID = 0;
        //    NetworkHandler.instance.SendProcess("6015#");
        //    NetworkHandler.instance.SendProcess("6002#");
        //    HarvestWindow.GetComponent<HarvestWindow>().ConquerListWindow.SetActive(true);
        //};
        UIEventListener.Get(InspectOKButton).onClick += delegate(GameObject go)
        {
            InspectWindow.SetActive(false);
        };
        UIEventListener.Get(QusButton).onClick += delegate(GameObject go)
        {
            QueMessage.SetActive(true);
            QueMessage.layer = 11;
            QueMessage.transform.Find("Question/Message/Scroll View").gameObject.layer = 11;
            MessageInfo(QueMessage);
        };
        Gate1Working.SetActive(true);
        RewardOne.SetActive(true);
        if (CharacterRecorder.instance.level >= TextTranslator.instance.GetFortressByListId(1, 2).Level)
        {
            Gate2Working.SetActive(true);
            Gate2Clock.SetActive(false);
            BlackGate2.SetActive(false);
            Gate2.SetActive(true);
            RewardTwo.SetActive(true);
        }
        if (CharacterRecorder.instance.level >= TextTranslator.instance.GetFortressByListId(1, 3).Level)
        {
            Gate3Working.SetActive(true);
            Gate3Clock.SetActive(false);
            BlackGate3.SetActive(false);
            Gate3.SetActive(true);
            RewardThree.SetActive(true);
        }

        BlackGate2UILabel.text = TextTranslator.instance.GetFortressByListId(1, 2).Level + "级开放";
        BlackGate3UILabel.text = TextTranslator.instance.GetFortressByListId(1, 3).Level + "级开放";

        GameCenter.leavelName = "ConquerWindow";

    }
    void MessageInfo(GameObject obj)
    {
        UIEventListener.Get(obj.transform.Find("Question/CloseButton").gameObject).onClick += delegate(GameObject go)
        {
            obj.SetActive(false);
        };
        UIEventListener.Get(obj.transform.Find("Black").gameObject).onClick += delegate(GameObject go)
        {
            obj.SetActive(false);
        };

    }

    public void ShowHeroInfo(string dataSplit)
    {
        CaptureList.Clear();
        CaptureObjList.Clear();
        InspectObjList.Clear();
        string[] itemSplit = dataSplit.Split('!');
        int heroNumber = 0;
        for (int i = 0; i < itemSplit.Length - 1; i++)
        {
            string[] iteminfo = itemSplit[i].Split('$');
            CaptureList.Add(int.Parse(iteminfo[3]));
            if (iteminfo[2] == "13" || iteminfo[2] == "14")
            {
                CharacterRecorder.instance.HostageRoleID = int.Parse(iteminfo[1]);
                CharacterRecorder.instance.HostageName = iteminfo[0];
                GameObject HeroObj = Instantiate(HeroItem) as GameObject;
                GameObject ItemObj = Instantiate(Resources.Load("Prefab/Role/" + iteminfo[1], typeof(GameObject))) as GameObject;
                GameObject InspectObj = Instantiate(InspectItem) as GameObject;
                HeroObj.SetActive(true);
                InspectObj.SetActive(true);
                InspectObj.GetComponent<UISprite>().spriteName = iteminfo[1];
                InspectObj.transform.Find("Name").GetComponent<UILabel>().text = iteminfo[0];
                InspectObj.transform.parent = InspectGrid.transform;
                InspectObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(iteminfo[1]));
                InspectObj.transform.localScale *= hi.heroScale;
                InspectObj.transform.localRotation = new Quaternion(0, 0, 0, 0);
                ItemObj.transform.localPosition = new Vector3(0, 0, 0);
                string Area = "";
                switch (int.Parse(iteminfo[4]))
                {
                    case 1:
                        HeroObj.transform.Find("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetFortressByListId(1, 1).NeedItemId.ToString();
                        Area = "[-][249BD2]正在[-][F89113]金币工厂[-][249BD2]工作";
                        break;
                    case 2:
                        HeroObj.transform.Find("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetFortressByListId(1, 2).NeedItemId.ToString();
                        Area = "[-][249BD2]正在[-][F89113]金条工厂[-][249BD2]工作";
                        break;
                    case 3:
                        HeroObj.transform.Find("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetFortressByListId(1, 3).NeedItemId.ToString();
                        Area = "[-][249BD2]正在[-][F89113]金矿工厂[-][249BD2]工作";
                        break;

                }
                InspectObj.transform.Find("WorkArea").GetComponent<UILabel>().text = Area;
                InspectObjList.Add(InspectObj);
                HeroObj.transform.parent = CheckGateWindow.transform;
                HeroObj.transform.localPosition = new Vector3(7500 + 600 * heroNumber, -882, 10511 + 1000 * heroNumber);
                HeroObj.transform.localScale = new Vector3(400, 400, 400);
                HeroObj.transform.localRotation = new Quaternion(0, 0, 0, 0);
                HeroObj.transform.Rotate(new Vector3(0, 90, 0));
                HeroObj.name = iteminfo[1];
                HeroObj.transform.Find("Name").GetComponent<UILabel>().text = iteminfo[0];
                heroNumber += 1;
                ItemObj.transform.parent = HeroObj.transform;
                ItemObj.name = iteminfo[1];
                ItemObj.transform.localPosition = new Vector3(0, 0, 0);
                ItemObj.transform.localScale = new Vector3(2, 2, 2);
                ItemObj.transform.localRotation = new Quaternion(0, 0, 0, 0);
                ItemObj.GetComponent<Animator>().Play("fulu_idle");
                ItemObj.GetComponent<Animator>().SetFloat("ft", 0);
                foreach (Component c in ItemObj.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
                {
                    if (c.name == "Object001" || c.name == "Object002")
                    {
                        c.gameObject.SetActive(false);
                    }
                }

                foreach (Component c in ItemObj.GetComponentsInChildren(typeof(MeshRenderer), true))
                {
                    if (c.name == "Object001" || c.name == "Object002")
                    {
                        c.gameObject.SetActive(false);
                    }
                }
                CaptureObjList.Add(HeroObj);
            }
        }
        if (CaptureObjList.Count == 0)
        {
            CounperButtonObj.SetActive(true);
        }
        else
        {
            CounperButtonObj.SetActive(false);
        }
        InspectNumber.text = CaptureObjList.Count + "人";
        MoveInfo();
    }
    void MoveInfo()
    {
        for (int i = 0; i < CaptureObjList.Count; i++)
        {
            if (CaptureObjList[i].transform.localPosition.x > 11200)
            {
                MoveNum = 1;
                isZhuanShen = true;
                ZhuanShenid = -1;

            }
            else if (CaptureObjList[i].transform.localPosition.x < 7500)
            {
                //CaptureObjList[i].transform.Find("Name").transform.localRotation = new Quaternion(0, 270, 0, 0);
                //CaptureObjList[i].transform.Find("Icon").transform.localRotation = new Quaternion(0, 270, 0, 0);
                MoveNum = 2;
                isZhuanShen = true;
                ZhuanShenid = 1;
            }
            if (MoveNum == 1)
            {
                CaptureObjList[i].transform.localPosition -= new Vector3(10, 0, 0);

            }
            else if (MoveNum == 2)
            {
                CaptureObjList[i].transform.localPosition += new Vector3(10, 0, 0);
            }
            else
            {
                CaptureObjList[i].transform.localRotation = new Quaternion(0, 0, 0, 0);
                CaptureObjList[i].transform.Rotate(new Vector3(0, 90, 0));
                CaptureObjList[i].transform.localPosition += new Vector3(10, 0, 0);
            }
        }
        if (isZhuanShen)
        {
            StartCoroutine(ZhuanShenEffect());
        }
        else
        {
            StartCoroutine(EffectRun());
        }
    }
    IEnumerator EffectRun()
    {
        yield return new WaitForSeconds(0.01f);
        MoveInfo();
    }
    IEnumerator ZhuanShenEffect()
    {
        for (int i = 0; i < CaptureObjList.Count; i++)
        {
            //CaptureObjList[i].transform.localRotation = new Quaternion(0, 0, 0, 0);
            CaptureObjList[i].transform.Find(CaptureObjList[i].name).transform.Rotate(new Vector3(0, 20 * ZhuanShenid, 0));
        }
        yield return new WaitForSeconds(0.005f);
        for (int i = 0; i < CaptureObjList.Count; i++)
        {
            CaptureObjList[i].transform.Find(CaptureObjList[i].name).transform.Rotate(new Vector3(0, 20 * ZhuanShenid, 0));
        }
        yield return new WaitForSeconds(0.005f);
        for (int i = 0; i < CaptureObjList.Count; i++)
        {
            CaptureObjList[i].transform.Find(CaptureObjList[i].name).transform.Rotate(new Vector3(0, 20 * ZhuanShenid, 0));

        }
        yield return new WaitForSeconds(0.005f);
        for (int i = 0; i < CaptureObjList.Count; i++)
        {
            CaptureObjList[i].transform.Find(CaptureObjList[i].name).transform.Rotate(new Vector3(0, 20 * ZhuanShenid, 0));
        }
        yield return new WaitForSeconds(0.005f);
        for (int i = 0; i < CaptureObjList.Count; i++)
        {
            CaptureObjList[i].transform.Find(CaptureObjList[i].name).transform.Rotate(new Vector3(0, 20 * ZhuanShenid, 0));
        }
        yield return new WaitForSeconds(0.005f);
        for (int i = 0; i < CaptureObjList.Count; i++)
        {
            CaptureObjList[i].transform.Find(CaptureObjList[i].name).transform.Rotate(new Vector3(0, 20 * ZhuanShenid, 0));
        }
        yield return new WaitForSeconds(0.005f);
        for (int i = 0; i < CaptureObjList.Count; i++)
        {
            CaptureObjList[i].transform.Find(CaptureObjList[i].name).transform.Rotate(new Vector3(0, 20 * ZhuanShenid, 0));
        }
        yield return new WaitForSeconds(0.005f);
        for (int i = 0; i < CaptureObjList.Count; i++)
        {
            CaptureObjList[i].transform.Find(CaptureObjList[i].name).transform.Rotate(new Vector3(0, 20 * ZhuanShenid, 0));
        }
        yield return new WaitForSeconds(0.005f);
        for (int i = 0; i < CaptureObjList.Count; i++)
        {
            CaptureObjList[i].transform.Find(CaptureObjList[i].name).transform.Rotate(new Vector3(0, 20 * ZhuanShenid, 0));
        }
        yield return new WaitForSeconds(0.005f);
        isZhuanShen = false;
        MoveInfo();
    }
    public void HeroSelfUpInfo(string dataSplit)//需要返回UID不然重复上阵
    {
        HeroSelfList.Clear();
        string[] itemSplit = dataSplit.Split('$');
        for (int i = 0; i < itemSplit.Length - 1; i++)
        {
            HeroSelfList.Add(int.Parse(itemSplit[i]));
        }
    }

    public void CapturedNumberInfo(int Number1, int Number2, string dataSplit, int ButtonCloseID)
    {
        if (CharacterRecorder.instance.level >= TextTranslator.instance.GetFortressByListId(1, 1).Level)
        {

            if (CharacterRecorder.instance.Vip < 3)
            {
                CounperUpNumber = 1;
            }
            else
            {
                CounperUpNumber = 2;
            }
        }

        if (CharacterRecorder.instance.level >= TextTranslator.instance.GetFortressByListId(1, 2).Level)
        {
            if (CharacterRecorder.instance.Vip < 3)
            {
                CounperUpNumber = 2;
            }
            else
            {
                CounperUpNumber = 4;
            }
        }
        if (CharacterRecorder.instance.level >= TextTranslator.instance.GetFortressByListId(1, 3).Level)
        {
            if (CharacterRecorder.instance.Vip < 3)
            {
                CounperUpNumber = 4;
            }
            else
            {
                CounperUpNumber = 6;
            }
        }
        if (CharacterRecorder.instance.isConquerRedPoint)
        {
            GetButtonObj.transform.Find("Red").gameObject.SetActive(true);
        }
        else
        {
            GetButtonObj.transform.Find("Red").gameObject.SetActive(false);
        }
        CaptureNumber.text = Number1.ToString() + "/" + CounperUpNumber.ToString();
        BeCapturedNumber.text = Number2.ToString();
        string[] itemInfo = dataSplit.Split('!');
        for (int i = 0; i < itemInfo.Length - 1; i++)
        {
            string[] rewardItem = itemInfo[i].Split('$');
            switch (rewardItem[0])
            {
                case "1":
                    RewardOne.GetComponent<UISprite>().spriteName = rewardItem[1];
                    RewardOne.transform.Find("Label").GetComponent<UILabel>().text = rewardItem[2];
                    break;
                case "2":
                    RewardTwo.GetComponent<UISprite>().spriteName = rewardItem[1];
                    RewardTwo.transform.Find("Label").GetComponent<UILabel>().text = rewardItem[2];
                    break;
                case "3":
                    RewardThree.GetComponent<UISprite>().spriteName = rewardItem[1];
                    RewardThree.transform.Find("Label").GetComponent<UILabel>().text = rewardItem[2];
                    break;
            }
        }
        if (ButtonCloseID == 1)
        {
            GetButtonObj.SetActive(true);
            CantGetButtonObj.SetActive(false);
            GetButton.SetActive(true);
            CantGetButton.SetActive(false);
        }
        else
        {
            GetButtonObj.SetActive(false);
            CantGetButtonObj.SetActive(true);
            GetButton.SetActive(false);
            CantGetButton.SetActive(true);
        }

    }
}

using UnityEngine;
using System.Collections;

public class HappyBoxItem : MonoBehaviour {

    public UISlider SliderBack;
    public GameObject Box1;
    public GameObject Box2;
    public GameObject Box3;
    public GameObject Box4;
    public UILabel AllIntegral;
    private int IsBox1=0;
    private int IsBox2=0;
    private int IsBox3=0;
    private int IsBox4=0;
	void Start () {
        if (UIEventListener.Get(Box1).onClick == null) 
        {
            UIEventListener.Get(Box1).onClick += delegate(GameObject go)
            {
                if (IsBox1 == 1)
                {
                    NetworkHandler.instance.SendProcess("1205#1;");
                }
                else 
                {
                    UIManager.instance.OpenPanel("ActivenessWindow", false);
                    GameObject.Find("ActivenessWindow").GetComponent<ActivenessWindow>().SetHappyBoxIsOpen(1, IsBox1);
                }
            };
        }
        if (UIEventListener.Get(Box2).onClick == null)
        {
            UIEventListener.Get(Box2).onClick += delegate(GameObject go)
            {
                if (IsBox2 == 1)
                {
                    NetworkHandler.instance.SendProcess("1205#2;");
                }
                else 
                {
                    UIManager.instance.OpenPanel("ActivenessWindow", false);
                    GameObject.Find("ActivenessWindow").GetComponent<ActivenessWindow>().SetHappyBoxIsOpen(2, IsBox2);
                }
            };
        }
        if (UIEventListener.Get(Box3).onClick == null)
        {
            UIEventListener.Get(Box3).onClick += delegate(GameObject go)
            {
                if (IsBox3 == 1)
                {
                    NetworkHandler.instance.SendProcess("1205#3;");
                }
                else 
                {
                    UIManager.instance.OpenPanel("ActivenessWindow", false);
                    GameObject.Find("ActivenessWindow").GetComponent<ActivenessWindow>().SetHappyBoxIsOpen(3, IsBox3);
                }

            };
        }
        if (UIEventListener.Get(Box4).onClick == null)
        {
            UIEventListener.Get(Box4).onClick += delegate(GameObject go)
            {
                if (IsBox4 == 1)
                {
                    NetworkHandler.instance.SendProcess("1205#4;");
                }
                else 
                {
                    UIManager.instance.OpenPanel("ActivenessWindow", false);
                    GameObject.Find("ActivenessWindow").GetComponent<ActivenessWindow>().SetHappyBoxIsOpen(4, IsBox4);
                }
            };
        }
        //SetHappyBoxInfo();
	}

    public void SetHappyBoxInfo()//string Receive
    {
        string Receive = CharacterRecorder.instance.HappyBoxInfo;// "30;1$0$0$1;";//
        Debug.Log("活跃度宝箱"+CharacterRecorder.instance.HappyBoxInfo);
        string[] dataSplit = Receive.Split(';');
        string[] RewardSplit = dataSplit[1].Split('$');
        if (int.Parse(RewardSplit[0])!=2)
        {
            //Box1.transform.Find("BoxClose1").gameObject.SetActive(true);
            Box1.transform.Find("BoxOpen1").gameObject.SetActive(false);
            if (int.Parse(RewardSplit[0]) == 1)
            {
                Box1.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(true);
                Box1.transform.Find("BoxClose1").gameObject.SetActive(false);
            }
            else 
            {
                Box1.transform.Find("BoxClose1").gameObject.SetActive(true);
            }
        }
        else 
        {
            Box1.transform.Find("BoxClose1").gameObject.SetActive(false);
            Box1.transform.Find("BoxOpen1").gameObject.SetActive(true);
            //Box1.transform.Find("BoxOpen1Bg").gameObject.SetActive(true);
            Box1.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
        }//

        if (int.Parse(RewardSplit[1]) != 2) 
        {
            //Box2.transform.Find("BoxClose2").gameObject.SetActive(true);
            Box2.transform.Find("BoxOpen2").gameObject.SetActive(false);
            if (int.Parse(RewardSplit[1]) == 1)
            {
                Box2.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(true);
                Box2.transform.Find("BoxClose2").gameObject.SetActive(false);
            }
            else 
            {
                Box2.transform.Find("BoxClose2").gameObject.SetActive(true);
            }
        }
        else
        {
            Box2.transform.Find("BoxClose2").gameObject.SetActive(false);
            Box2.transform.Find("BoxOpen2").gameObject.SetActive(true);
            //Box2.transform.Find("BoxOpen2Bg").gameObject.SetActive(true);
            Box2.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
        }
        //
        if (int.Parse(RewardSplit[2]) != 2)
        {
            //Box3.transform.Find("BoxClose3").gameObject.SetActive(true);
            Box3.transform.Find("BoxOpen3").gameObject.SetActive(false);
            if (int.Parse(RewardSplit[2]) == 1)
            {
                Box3.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(true);
                Box3.transform.Find("BoxClose3").gameObject.SetActive(false);
            }
            else 
            {
                Box3.transform.Find("BoxClose3").gameObject.SetActive(true);
            }
        }
        else
        {
            Box3.transform.Find("BoxClose3").gameObject.SetActive(false);
            Box3.transform.Find("BoxOpen3").gameObject.SetActive(true);
            //Box3.transform.Find("BoxOpen3Bg").gameObject.SetActive(true);
            Box3.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
        }

        if (int.Parse(RewardSplit[3]) != 2)
        {
            //Box4.transform.Find("BoxClose4").gameObject.SetActive(true);
            Box4.transform.Find("BoxOpen4").gameObject.SetActive(false);
            if (int.Parse(RewardSplit[3]) == 1)
            {
                Box4.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(true);
                Box4.transform.Find("BoxClose4").gameObject.SetActive(false);
            }
            else 
            {
                Box4.transform.Find("BoxClose4").gameObject.SetActive(true);
            }
        }
        else
        {
            Box4.transform.Find("BoxClose4").gameObject.SetActive(false);
            Box4.transform.Find("BoxOpen4").gameObject.SetActive(true);
            //Box4.transform.Find("BoxOpen4Bg").gameObject.SetActive(true);
            Box4.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
        }

        IsBox1 = int.Parse(RewardSplit[0]);
        IsBox2 = int.Parse(RewardSplit[1]);
        IsBox3 = int.Parse(RewardSplit[2]);
        IsBox4 = int.Parse(RewardSplit[3]);
        AllIntegral.text = dataSplit[0];
        SliderBack.value = (int.Parse(dataSplit[0]) / 100f);
        //SliderBack.value = (int.Parse(RewardSplit[0]) + int.Parse(RewardSplit[1]) + int.Parse(RewardSplit[2]) + int.Parse(RewardSplit[3]))/2 * 0.25f;
        //if (int.Parse(RewardSplit[3]) != 0) 
        //{
        //    SliderBack.value = 1f;
        //}
        //else if (int.Parse(RewardSplit[2]) != 0) 
        //{
        //    SliderBack.value = 0.75f;
        //}
        //else if (int.Parse(RewardSplit[1]) != 0)
        //{
        //    SliderBack.value = 0.5f;
        //}
        //else if (int.Parse(RewardSplit[0]) != 0)
        //{
        //    SliderBack.value = 0.25f;
        //}
        //else 
        //{
        //    SliderBack.value = 0;
        //}
    }
}

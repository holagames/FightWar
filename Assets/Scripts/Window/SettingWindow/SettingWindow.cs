using UnityEngine;
using System.Collections;

public class SettingWindow : MonoBehaviour
{

    public UISlider ElectractySlider;
    public UISlider EffectSlider;
    public UISlider BGmusicSlider;
    public UISlider EffectMusicSlider;

    /// <summary>
    /// 省电模式
    /// </summary>
    public GameObject PowerSavingMode;
    /// <summary>
    /// 华丽模式
    /// </summary>
    public GameObject GorgeousModel;
    /// <summary>
    /// isPowerSaving  为0表示华丽模式  为1表示省电模式
    /// </summary>
    private int isPowerSaving = 0;

    public void InitPowerSaving()
    {
        if (isPowerSaving==0)//表示省电模式
        {            
            PowerSavingMode.transform.FindChild("Checkmark").gameObject.SetActive(true);
            GorgeousModel.transform.FindChild("Checkmark").gameObject.SetActive(false);
            PowerSavingMode.transform.FindChild("Label").GetComponent<UILabel>().text = "[ffffffc2]省电模式";
            GorgeousModel.transform.FindChild("Label").GetComponent<UILabel>().text = "[ffffff78]华丽模式";            
        }
        else//表示华丽模式
        {            
            PowerSavingMode.transform.FindChild("Checkmark").gameObject.SetActive(false);
            GorgeousModel.transform.FindChild("Checkmark").gameObject.SetActive(true);
            PowerSavingMode.transform.FindChild("Label").GetComponent<UILabel>().text = "[ffffff78]省电模式";
            GorgeousModel.transform.FindChild("Label").GetComponent<UILabel>().text = "[ffffffc2]华丽模式";            
        }
    }
    // Use this for initialization
    void Start()
    {
        isPowerSaving = int.Parse(PlayerPrefs.GetFloat("ElectractySlider", 1).ToString());
        InitPowerSaving();
        if (UIEventListener.Get(PowerSavingMode).onClick == null)
        {
            UIEventListener.Get(PowerSavingMode).onClick += delegate(GameObject go)
            {
                if (isPowerSaving==1)
                {
                    isPowerSaving = 0;
                }
                else
                {
                    isPowerSaving = 1;
                }
                InitPowerSaving();
            };
        }
        if (UIEventListener.Get(GorgeousModel).onClick == null)
        {
            UIEventListener.Get(GorgeousModel).onClick += delegate(GameObject go)
            {
                if (isPowerSaving == 1)
                {
                    isPowerSaving = 0;
                }
                else
                {
                    isPowerSaving = 1;
                }
                InitPowerSaving();
            };
        }



        if (UIEventListener.Get(GameObject.Find("SettingCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SettingCloseButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }

        //if (UIEventListener.Get(GameObject.Find("SettingMask")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("SettingMask")).onClick += delegate(GameObject go)
        //    {
        //        UIManager.instance.BackUI();
        //    };
        //}

        if (UIEventListener.Get(GameObject.Find("SureButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SureButton")).onClick += delegate(GameObject go)
            {
                PlayerPrefs.SetFloat("ElectractySlider", isPowerSaving);
                if (PlayerPrefs.GetFloat("ElectractySlider")==1)
                {
                    AudioEditer.instance.PlayOneShot("ui_button");
                    GameObject.Find("MainWindow").GetComponent<MainWindow>().ChangeWhenElectractySlider();
                    AudioEditer.instance.PlayLoop("");
                    AudioEditer.instance.audioSource.volume = 1;
                }
                else
                {
                    AudioEditer.instance.PlayOneShot("ui_button");
                    GameObject.Find("MainWindow").GetComponent<MainWindow>().ChangeWhenElectractySlider();
                    AudioEditer.instance.StopPlay();
                    AudioEditer.instance.audioSource.volume = 0;
                }
                UIManager.instance.BackUI();
            };
        }

        //if (PlayerPrefs.GetFloat("ElectractySlider") == 0)
        //{
        //    PlayerPrefs.SetFloat("ElectractySlider", 1);
        //}
        //if (PlayerPrefs.GetFloat("EffectSlider") == 0)
        //{
        //    PlayerPrefs.SetFloat("EffectSlider", 1);
        //}
        //if (PlayerPrefs.GetFloat("BGmusicSlider") == 0)
        //{
        //    PlayerPrefs.SetFloat("BGmusicSlider", 1);
        //}
        //if (PlayerPrefs.GetFloat("EffectMusicSlider") == 0)
        //{
        //    PlayerPrefs.SetFloat("EffectMusicSlider", 1);
        //}

        ElectractySlider.value = PlayerPrefs.GetFloat("ElectractySlider", 1);
        EffectSlider.value = PlayerPrefs.GetFloat("EffectSlider", 1);
        BGmusicSlider.value = PlayerPrefs.GetFloat("BGmusicSlider");
        EffectMusicSlider.value = PlayerPrefs.GetFloat("EffectMusicSlider");

        ElectractySlider.onDragFinished = delegate()
        {
            if (ElectractySlider.value > PlayerPrefs.GetFloat("ElectractySlider"))
            {
                ElectractySlider.value = 1;
                PlayerPrefs.SetFloat("ElectractySlider", 1);
                AudioEditer.instance.PlayOneShot("ui_button");
                GameObject.Find("MainWindow").GetComponent<MainWindow>().ChangeWhenElectractySlider();
            }
            else if (ElectractySlider.value < PlayerPrefs.GetFloat("ElectractySlider"))
            {
                ElectractySlider.value = 0;
                PlayerPrefs.SetFloat("ElectractySlider", 0);
                AudioEditer.instance.PlayOneShot("ui_button");
                GameObject.Find("MainWindow").GetComponent<MainWindow>().ChangeWhenElectractySlider();
            }
        };

        EffectSlider.onDragFinished = delegate()
        {
            PlayerPrefs.SetFloat("EffectSlider", EffectSlider.value);
            AudioEditer.instance.PlayOneShot("ui_button");

            if (EffectSlider.value > PlayerPrefs.GetFloat("EffectSlider"))
            {
                EffectSlider.value = 1;
                PlayerPrefs.SetFloat("EffectSlider", 1);
                AudioEditer.instance.PlayOneShot("ui_button");
            }
            else if (EffectSlider.value < PlayerPrefs.GetFloat("EffectSlider"))
            {
                EffectSlider.value = 0;
                PlayerPrefs.SetFloat("EffectSlider", 0);
                AudioEditer.instance.PlayOneShot("ui_button");
            }
        };

        BGmusicSlider.onDragFinished = delegate()
        {
            if (BGmusicSlider.value > PlayerPrefs.GetFloat("BGmusicSlider"))
            {
                BGmusicSlider.value = 1;
                AudioEditer.instance.playBGM = true;
                AudioEditer.instance.PlayLoop("");
                PlayerPrefs.SetFloat("BGmusicSlider", 1);
                PlayerPrefs.SetInt("musicIsOpen", 1);
            }
            else if (BGmusicSlider.value < PlayerPrefs.GetFloat("BGmusicSlider"))
            {
                BGmusicSlider.value = 0;
                AudioEditer.instance.playBGM = false;
                AudioEditer.instance.StopPlay();
                PlayerPrefs.SetFloat("BGmusicSlider", 0);
                PlayerPrefs.SetInt("musicIsOpen", 0);
            }
        };

        EffectMusicSlider.onDragFinished = delegate()
        {
            if (EffectMusicSlider.value > PlayerPrefs.GetFloat("EffectMusicSlider"))
            {
                EffectMusicSlider.value = 1;
                AudioEditer.instance.playSound = true;
                NGUITools.soundVolume = 1;
                AudioEditer.instance.PlayOneShot("ui_button");
                PlayerPrefs.SetFloat("EffectMusicSlider", 1);
            }
            else if (EffectMusicSlider.value < PlayerPrefs.GetFloat("EffectMusicSlider"))
            {
                EffectMusicSlider.value = 0;
                AudioEditer.instance.playSound = false;
                NGUITools.soundVolume = 0;
                PlayerPrefs.SetFloat("EffectMusicSlider", 0);
            }
        };

    }


}

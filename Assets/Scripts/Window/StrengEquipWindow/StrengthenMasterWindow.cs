using UnityEngine;
using System.Collections.Generic;

public class StrengthenMasterWindow : MonoBehaviour 
{
    public List<GameObject> masterTypeList = new List<GameObject>();
    
    public UILabel currentLv;
    public UILabel currentHP;
    public UILabel currentAtt;
    public UILabel currentDef;

    public UILabel nextLv;
    public UILabel nextHP;
    public UILabel nextAtt;
    public UILabel nextDef;

    public GameObject closeBtn;
    public GameObject arrow;
    
    private int _currentType = 1;

    public UIGrid itemGrid;

    public List<GameObject> itemList = new List<GameObject>();

    private List<int> mastLevel = new List<int>();

    private Hero mHero;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Effect");
        for (var i = 0; i < masterTypeList.Count;i++ )
        {
            if (UIEventListener.Get(masterTypeList[i]).onClick == null)
            {
                UIEventListener.Get(masterTypeList[i]).onClick += delegate(GameObject go)
                {
                    MasterTypeOnClick(go);
                };
            }
        }

        UIEventListener.Get(closeBtn).onClick += delegate(GameObject go)
        {
            DestroyImmediate(this.gameObject);
        };
    }

    private void MasterTypeOnClick(GameObject go)
    {
        UILabel name = gameObject.transform.FindChild("All/Right/BgSprite/NameLabel").GetComponent<UILabel>();
        _currentType = int.Parse(go.name.Substring(4,1));
        switch (_currentType)
        {
            case 1:
                name.text = "装备强化";
                break;
            case 2:
                name.text = "装备精炼";
                break;
            case 3:
                name.text = "饰品强化";
                break;
            case 4:
                name.text = "饰品精炼";
                break;
            default:
                break;
        }
        SetMasterInfo();
    }

    private void SetMasterInfo()
    {
        StrengthenMaster _currentMaster = TextTranslator.instance.GetStrengthenMasterByTypeAndLevel(_currentType, mastLevel[_currentType - 1]);
        StrengthenMaster _NextMaster = TextTranslator.instance.GetStrengthenMasterByTypeAndLevel(_currentType, mastLevel[_currentType - 1]+1);

        
        if (_currentMaster != null)
        {
            currentLv.text = "lv." + (mastLevel[_currentType - 1]).ToString();
            currentHP.text = _currentMaster.hp.ToString();
            currentDef.text = _currentMaster.defense.ToString();
            currentAtt.text = _currentMaster.attack.ToString();
        }
        else
        {
            currentLv.text = "lv." + "0";
            currentHP.text = "0";
            currentDef.text = "0";
            currentAtt.text = "0";
        }

        if (_NextMaster != null)
        {
            nextLv.gameObject.SetActive(true);
            arrow.SetActive(false);
            nextLv.text = "lv." + (mastLevel[_currentType - 1] + 1).ToString();
            nextHP.text = _NextMaster.hp.ToString();
            nextDef.text = _NextMaster.defense.ToString();
            nextAtt.text = _NextMaster.attack.ToString();
        }
        else
        {
            nextLv.gameObject.SetActive(false);
            arrow.SetActive(true);
            currentLv.text = "";
            nextLv.text = "";
        }

        for (var i = 0; i < itemList.Count;i++ )
        {

            if (_currentType <= 2)
            {
                if (i < 4)
                {
                    itemList[i].SetActive(true);
                }
                else
                {
                    itemList[i].SetActive(false);
                }
                itemGrid.cellWidth = 130f;
                itemGrid.Reposition();
            }
            else
            {
                if (i < 4)
                {
                    itemList[i].SetActive(false);
                }
                else
                {
                    itemList[i].SetActive(true);
                }

                itemGrid.cellWidth = 200f;
                itemGrid.Reposition();
            }

            if (mHero.equipList[i] != null)
            {
                TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(mHero.equipList[i].equipCode);
                itemList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = mHero.equipList[i].equipCode.ToString();                
                itemList[i].GetComponent<UISprite>().spriteName = string.Format("Grade{0}", mItemInfo.itemGrade);
                                

                int _currentLevel = 0;
                if (_currentType == 1 || _currentType == 3)
                {
                    itemList[i].transform.FindChild("Label").GetComponent<UILabel>().text =mHero.equipList[i].equipLevel.ToString();
                    _currentLevel = mHero.equipList[i].equipLevel;
                }
                else
                {
                    itemList[i].transform.FindChild("Label").GetComponent<UILabel>().text =  mHero.equipList[i].equipClass.ToString();
                    _currentLevel = mHero.equipList[i].equipClass;
                }

                if (_NextMaster != null)
                {
                    itemList[i].transform.FindChild("ProgressBar").GetComponent<UISlider>().value = (float)_currentLevel / (float)_NextMaster.needLevel;
                    itemList[i].transform.FindChild("lv").GetComponent<UILabel>().text = string.Format("{0}/{1}", _currentLevel.ToString(), _NextMaster.needLevel.ToString());
                }
                else
                {
                    itemList[i].transform.FindChild("ProgressBar").GetComponent<UISlider>().value = (float)_currentLevel / (float)_currentMaster.needLevel;
                    itemList[i].transform.FindChild("lv").GetComponent<UILabel>().text = string.Format("{0}/{1}", _currentLevel.ToString(), _currentMaster.needLevel.ToString());
                }

            }
        }


    }


    public void SerHeroInfo(string RecvString)
    {
        mastLevel.Clear();

        string[] dataSplit = RecvString.Split(';');
        mHero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(dataSplit[0]));
        
        mastLevel.Add(int.Parse(dataSplit[1]));
        mastLevel.Add(int.Parse(dataSplit[2]));
        mastLevel.Add(int.Parse(dataSplit[3]));
        mastLevel.Add(int.Parse(dataSplit[4]));

        for (var i = 0; i < masterTypeList.Count;i++ )
        {
            masterTypeList[i].transform.FindChild("Label").GetComponent<UILabel>().text = mastLevel[i].ToString();
        }
       
        SetMasterInfo();

    }
   
	
}

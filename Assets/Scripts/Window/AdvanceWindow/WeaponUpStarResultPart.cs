using UnityEngine;
using System.Collections;

public class WeaponUpStarResultPart : MonoBehaviour
{

    public GameObject StarBeforeIcon;
    public GameObject StarAfterIcon;
    public GameObject StarBeforeStar;
    public GameObject StarAfterStar;
    public GameObject JianTouEffect;
    public GameObject KuangEffect;
    public GameObject LineEffect;
    public GameObject SucessEffect;
    public GameObject ClassBeforeIcon;
    public GameObject ClassAfterIcon;
    public GameObject SysSuccessIcon;
    public GameObject SysSuccessLabel;
    public GameObject FightLabel;
    public GameObject AttributeLabel;

    public WeaponMaterial ItemMaterial;
    public WeaponUpClass ItemUpClass;
    public WeaponUpStar ItemUpStar;
    public TextTranslator.ItemInfo WeaponItem;
    // Use this for initialization
    void Start()
    {

    }

    public void SetInfo(int WeaponID, int WeaponClass, int WeaponStar, string BeforeFight, string AfterFight)
    {
        WeaponDic(WeaponID, WeaponClass, WeaponStar);
        if (WeaponClass == 1 && WeaponStar == 0)
        {
           StartCoroutine(SysEffect(WeaponID, WeaponClass, WeaponStar, BeforeFight, AfterFight));
        }
        else if ((WeaponStar != 1 && WeaponStar == 0)|| (WeaponClass == 5 && WeaponStar == 5))
        {
            StartCoroutine(ClassEffect(WeaponID, WeaponClass, WeaponStar, BeforeFight, AfterFight));
        }
        else
        {
            StartCoroutine(StarEffect(WeaponID, WeaponClass, WeaponStar, BeforeFight, AfterFight));       
        }
    }
    void WeaponDic(int WeaponID, int WeaponClass, int WeaponStar)
    {
        ItemMaterial = TextTranslator.instance.GetWeaponMaterialByID(WeaponID);
        if (WeaponStar == 0)
        {
            if (WeaponClass == 0)
            {
                ItemUpClass = TextTranslator.instance.GetWeaponUpClassByID(WeaponID, 2);
                ItemUpStar = TextTranslator.instance.GetWeaponUpStarByID(WeaponID, 1, 1);
            }
            else
            {
                ItemUpClass = TextTranslator.instance.GetWeaponUpClassByID(WeaponID, WeaponClass + 1);
                ItemUpStar = TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, 1);
            }

        }
        else
        {
            ItemUpClass = TextTranslator.instance.GetWeaponUpClassByID(WeaponID, WeaponClass + 1);
            ItemUpStar = TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, WeaponStar);
        }
        WeaponItem = TextTranslator.instance.GetItemByItemCode(WeaponID);
        //LeftInfo(WeaponID, WeaponClass);
    }

    IEnumerator SysEffect(int _WeaponID, int _WeaponClass, int _WeaponStar, string BeforeFight, string AfterFight)
    {
        SucessEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        KuangEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        SysSuccessIcon.SetActive(true);
        SysSuccessIcon.transform.Find("ZhuangBei_icon/Icon1").GetComponent<UISprite>().spriteName = ItemMaterial.WeaponID.ToString();
        JianTouEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SysSuccessLabel.SetActive(true);
        SysSuccessLabel.transform.Find("FightCurLabel1").GetComponent<UILabel>().text = "HP:" + "+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, _WeaponClass).Hp;
        SysSuccessLabel.transform.Find("FightCurLabel2").GetComponent<UILabel>().text = "攻击:" + "+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, _WeaponClass).Atk;
        SysSuccessLabel.transform.Find("FightCurLabel3").GetComponent<UILabel>().text = "防御:" + "+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, _WeaponClass).Def;
        yield return new WaitForSeconds(0.1f);
        LineEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SetFightInfo(BeforeFight, AfterFight);
    }

    IEnumerator StarEffect(int _WeaponID, int _WeaponClass, int _WeaponStar,string BeforeFight, string AfterFight)
    {
        SucessEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        KuangEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        StarBeforeIcon.SetActive(true);

        StarBeforeIcon.transform.Find("ZhuangBei_icon/Icon1").GetComponent<UISprite>().spriteName = ItemMaterial.WeaponID.ToString();
        JianTouEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        StarAfterIcon.SetActive(true);
       
        StarAfterIcon.transform.Find("ZhuangBei_icon/Icon2").GetComponent<UISprite>().spriteName = ItemMaterial.WeaponID.ToString();
        yield return new WaitForSeconds(0.1f);
        StarBeforeStar.SetActive(true);
        if (_WeaponStar != 0)
        {
            for (int i = 1; i < _WeaponStar; i++)
            {
                StarBeforeStar.transform.Find("ZhuangBei_icon/Grid/Icon" + i).gameObject.SetActive(true);
            }
        }
        yield return new WaitForSeconds(0.1f);
        StarAfterStar.SetActive(true);
        for (int i = 1; i < _WeaponStar +1; i++)
        {
            StarAfterStar.transform.Find("ZhuangBei_icon/Grid/Icon" + i).gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);
        LineEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SetFightInfo(BeforeFight, AfterFight);
    }

    IEnumerator ClassEffect(int _WeaponID, int _WeaponClass, int _WeaponStar, string BeforeFight, string AfterFight)
    {
        SucessEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        KuangEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        ClassBeforeIcon.SetActive(true);
        //ClassBeforeIcon.transform.Find("ZhuangBei_icon/Frame1").GetComponent<UISprite>().spriteName = "Grade" + ItemUpStar.Color.ToString();
        //ClassBeforeIcon.transform.Find("ZhuangBei_icon/Frame1/Icon1").GetComponent<UISprite>().spriteName = ItemMaterial.WeaponID.ToString();
        ClassBeforeIcon.transform.Find("ZhuangBei_icon/Frame1").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetWeaponUpStarByID(_WeaponID, _WeaponClass - 1, _WeaponStar).Color+1).ToString();
        ClassBeforeIcon.transform.Find("ZhuangBei_icon/Frame1/Icon1").GetComponent<UISprite>().spriteName = ItemMaterial.WeaponID.ToString();
        JianTouEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        ClassAfterIcon.SetActive(true);
        ClassAfterIcon.transform.Find("ZhuangBei_icon/Frame2").GetComponent<UISprite>().spriteName = "Grade" + (ItemUpStar.Color+1).ToString();
        ClassAfterIcon.transform.Find("ZhuangBei_icon/Frame2/Icon2").GetComponent<UISprite>().spriteName = ItemMaterial.WeaponID.ToString();
        yield return new WaitForSeconds(0.1f);
        AttributeLabel.SetActive(true);
        AttributeLabel.transform.Find("Attribute").GetComponent<UILabel>().text = "攻击：[-]+" + TextTranslator.instance.GetWeaponUpStarByID(_WeaponID, _WeaponClass, 0).Atk * 100 + "%" 
                                                                                  + "  " + "防御：[-]+" + TextTranslator.instance.GetWeaponUpStarByID(_WeaponID, _WeaponClass, 0).Def * 100 + "%" + "  " + "生命：[-]+"
                                                                                  + TextTranslator.instance.GetWeaponUpStarByID(_WeaponID, _WeaponClass, 0).Hp * 100 + "%";
        yield return new WaitForSeconds(0.1f);
        LineEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SetFightInfo(BeforeFight,AfterFight);
    }

    public void SetFightInfo(string BeforeFight, string AfterFight)
    {
        FightLabel.SetActive(true);
        FightLabel.transform.Find("FightBeforeLabel").GetComponent<UILabel>().text = BeforeFight;
        FightLabel.transform.Find("FightCurLabel").GetComponent<UILabel>().text = AfterFight;
    }
}

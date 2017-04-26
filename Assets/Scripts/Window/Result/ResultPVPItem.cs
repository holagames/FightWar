using UnityEngine;
using System.Collections;

public class ResultPVPItem : MonoBehaviour
{

    public GameObject SpriteAvatar;
    public GameObject LabelRoleLevel;

    public void Init(int _CharacterRoleID, int _LevelUpCount)
    {
        HeroInfo hero = TextTranslator.instance.GetHeroInfoByHeroID(_CharacterRoleID);
        gameObject.transform.GetComponent<UISprite>().spriteName = "yxdi" + (hero.heroRarity).ToString();
        //SpriteAvatar.GetComponent<UISprite>().spriteName = "60002";
        LabelRoleLevel.GetComponent<UILabel>().text = "Lv." + _LevelUpCount.ToString();
        if (_CharacterRoleID.ToString()[0] == '6')
        {
            SpriteAvatar.GetComponent<UISprite>().spriteName = (_CharacterRoleID).ToString();
        }
        if (_CharacterRoleID.ToString()[0] == '7')
        {
            SpriteAvatar.GetComponent<UISprite>().spriteName = (_CharacterRoleID - 10000).ToString();
        }
        //HeroInfo mheroInfo = TextTranslator.instance.GetHeroInfoByHeroID(_CharacterRoleID);
        //if (_CharacterRoleID > 0)
        //{
        //    Debug.Log(_CharacterRoleID);
        //    Hero h = CharacterRecorder.instance.GetHeroByCharacterRoleID(_CharacterRoleID);
        //    h.exp += _Exp;
        //    if (h.exp > h.maxExp)
        //    {
        //        Debug.Log("LevelUp");
        //        NetworkHandler.instance.SendProcess("1005#" + _CharacterRoleID.ToString() + ";");
        //    }
        //    ProgressBar.GetComponent<UISlider>().value = _ExpBefore;
        //    StartCoroutine(AddExp());
        //    LabelRoleUp.GetComponent<UILabel>().text = "Lv."+mLevelUpCount.ToString();
        //    SpriteAvatar.GetComponent<UISprite>().spriteName = h.cardID.ToString();
        //    LabelRoleExp.GetComponent<UILabel>().text = "+" + _Exp.ToString();

        //    if (_ExpBefore > _ExpAfter)
        //    {
        //        IsLevelUp = true;
        //    }
        //}
    }
    //IEnumerator AddExp()
    //{
    //    if (mLevelUpCount > 0)
    //    {
    //       // Debug.LogError(111111);
    //        while (mLevelUpCount > 0)
    //        {
    //           // Debug.LogError(222222);
    //            while (ProgressBar.GetComponent<UISlider>().value < 1)
    //            {
    //                ProgressBar.GetComponent<UISlider>().value += 0.01f;
    //                yield return new WaitForSeconds(0.05f);
    //            }
    //           // Debug.LogError(3333333);
    //            ProgressBar.GetComponent<UISlider>().value = 0;
    //            mLevelUpCount -= 1;
    //        }
    //       // Debug.LogError(4444444);
    //        while (ProgressBar.GetComponent<UISlider>().value < mExpAfter)
    //        {
    //            ProgressBar.GetComponent<UISlider>().value += 0.01f;
    //            yield return new WaitForSeconds(0.05f);
    //        }
    //       // Debug.LogError(5555555);
    //        ProgressBar.GetComponent<UISlider>().value = mExpAfter;
    //       // Debug.LogError(ProgressBar.GetComponent<UISlider>().value+"  (1)(1)");
    //    }
    //    else if (mLevelUpCount <= 0)
    //    {
    //        while (ProgressBar.GetComponent<UISlider>().value <= mExpAfter)
    //        {
    //            ProgressBar.GetComponent<UISlider>().value += 0.01f;
    //            yield return new WaitForSeconds(0.05f);
    //        }
    //        ProgressBar.GetComponent<UISlider>().value = mExpAfter;
    //      //  Debug.LogError(ProgressBar.GetComponent<UISlider>().value+"    (2)(2)");
    //    }
    //}
}
using UnityEngine;
using System.Collections;

public class ResultItem : MonoBehaviour
{

    public GameObject SpriteAvatar;
    public GameObject ProgressBar;
    public GameObject LabelRoleExp;
    public GameObject LabelRoleUp;

    float mExpAfter = 0;
    int mLevelUpCount = 0;
    bool IsGo = false;
    bool IsLevelUp = false;

    public GameObject ADD1;
    public GameObject ADD2;
    public GameObject ADD3;
    //public GameObject RankIcon;

    public GameObject gride;
    public UISprite pinJieSprite;

    public void Init(int _CharacterRoleID, int _LevelUpCount, float _ExpBefore, float _ExpAfter, int _Exp)
    {
        IsGo = true;
        mExpAfter = _ExpAfter;
        mLevelUpCount = _LevelUpCount;
        //HeroInfo mheroInfo = TextTranslator.instance.GetHeroInfoByHeroID(_CharacterRoleID);
        if (_CharacterRoleID > 0)
        {
            Hero h = CharacterRecorder.instance.GetHeroByCharacterRoleID(_CharacterRoleID);
            h.exp += _Exp;
            //Debug.Log("rare" + h.rare);
            //GradeColor(h.classNumber);

            gride = this.gameObject.transform.FindChild("Grid").gameObject;
            pinJieSprite = this.gameObject.transform.FindChild("SpritePinJie").GetComponent<UISprite>();
            DestroyGride();
            int addNum = TextTranslator.instance.SetHeroNameColor(this.gameObject.transform.Find("ItemGrade").GetComponent<UISprite>(), pinJieSprite, h.classNumber);//this.gameObject.GetComponent<UISprite>()
            for (int i = 0; i < addNum; i++)
            {
                GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
                obj.SetActive(true);
            }

            UIGrid _UIGrid = gride.GetComponent<UIGrid>();
            _UIGrid.sorting = UIGrid.Sorting.Horizontal;
            _UIGrid.pivot = UIWidget.Pivot.Center;
            _UIGrid.animateSmoothly = true;


            SetRankIcon(h.rank);

            if (_LevelUpCount > 0)
            {
                Debug.Log("LevelUp");
                NetworkHandler.instance.SendProcess("1005#" + _CharacterRoleID.ToString() + ";");
            }
            float _BeforeExp = GetfloatNum(_ExpBefore);
            ProgressBar.GetComponent<UISlider>().value = _BeforeExp;
            LabelRoleUp.GetComponent<UILabel>().text = "Lv." + h.level.ToString();
            StartCoroutine(AddExp());
            SpriteAvatar.GetComponent<UISprite>().spriteName = h.cardID.ToString();
            LabelRoleExp.GetComponent<UILabel>().text = "+" + _Exp.ToString();

            if (_ExpBefore > _ExpAfter)
            {
                IsLevelUp = true;
            }
        }
    }
    void DestroyGride()
    {
        for (int i = gride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gride.transform.GetChild(i).gameObject);
        }
    }
    float GetfloatNum(float Num) 
    {
        while(Num>1)
        {
            Num--;
        }
        return Num;
    }
    IEnumerator AddExp()
    {
        float _AfterExp = GetfloatNum(mExpAfter);
        if (mLevelUpCount > 0)//升级次数
        {
            while (mLevelUpCount > 0)
            {
                while (ProgressBar.GetComponent<UISlider>().value < 1)
                {
                    ProgressBar.GetComponent<UISlider>().value += 0.01f;
                    yield return new WaitForSeconds(0.01f);
                }
                ProgressBar.GetComponent<UISlider>().value = 0;
                mLevelUpCount -= 1;
                int level = int.Parse(LabelRoleUp.GetComponent<UILabel>().text.Replace("Lv.", "")) + 1;
                LabelRoleUp.GetComponent<UILabel>().text = "Lv." + level.ToString();
            }
            while (ProgressBar.GetComponent<UISlider>().value < _AfterExp)
            {
                ProgressBar.GetComponent<UISlider>().value += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            ProgressBar.GetComponent<UISlider>().value = _AfterExp;
        }
        else if (mLevelUpCount <= 0)
        {
            while (ProgressBar.GetComponent<UISlider>().value <= _AfterExp)
            {
                ProgressBar.GetComponent<UISlider>().value += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            ProgressBar.GetComponent<UISlider>().value = _AfterExp;
        }
    }

    void GradeColor(int Num)
    {
        switch (Num)
        {
            case 1:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi0";
                break;
            case 2:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi1";
                break;
            case 3:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi1";
                ADD1.SetActive(true);
                ADD1.GetComponent<UISprite>().spriteName = "zbkuang2";
                break;
            case 4:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi2";
                break;
            case 5:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi2";
                ADD1.SetActive(true);
                ADD1.GetComponent<UISprite>().spriteName = "zbkuang3";
                break;
            case 6:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi2";
                ADD2.SetActive(true);
                ADD2.GetComponent<UISprite>().spriteName = "zbkuang3";
                ADD2.transform.Find("ADDBg").GetComponent<UISprite>().spriteName = "zbkuang3";
                break;
            case 7:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
                break;
            case 8:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
                ADD1.SetActive(true);
                ADD1.GetComponent<UISprite>().spriteName = "zbkuang4";
                break;
            case 9:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
                ADD2.SetActive(true);
                ADD2.GetComponent<UISprite>().spriteName = "zbkuang4";
                ADD2.transform.Find("ADDBg").GetComponent<UISprite>().spriteName = "zbkuang4";
                break;
            case 10:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
                ADD3.SetActive(true);
                ADD3.GetComponent<UISprite>().spriteName = "zbkuang4";
                ADD3.transform.Find("ADDBg1").GetComponent<UISprite>().spriteName = "zbkuang4";
                ADD3.transform.Find("ADDBg2").GetComponent<UISprite>().spriteName = "zbkuang4";
                break;
            case 11:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi4";
                break;
            case 12:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi4";
                ADD1.SetActive(true);
                ADD1.GetComponent<UISprite>().spriteName = "zbkuang5";
                break;
            default:
                break;

            /*
        case 1:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi0";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang1";
            break;
        case 2:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi1";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang2";
            break;
        case 3:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi1";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang2";
            break;
        case 4:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi2";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang3";
            break;
        case 5:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi2";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang3";
            break;
        case 6:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi2";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang3";
            break;
        case 7:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang4";
            break;
        case 8:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang4";
            break;
        case 9:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang4";
            break;
        case 10:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang4";
            break;
        case 11:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi4";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang5";
            break;
        case 12:
            this.gameObject.GetComponent<UISprite>().spriteName = "yxdi4";
            GameObject.Find("ADD1").GetComponent<UISprite>().spriteName = "zbkuang5";
            break;
        default:
            break;*/
        }
    }
    //设置军衔
    void SetRankIcon(int Num)
    {
        this.transform.Find("RankIcon").transform.GetChild(Num).gameObject.SetActive(true);
        /*
        switch (Num)
        {
            case 1:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank01";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "下士";
                break;
            case 2:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank02";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "中士";
                break;
            case 3:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank03";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "上士";
                break;
            case 4:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank04";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "少尉";
                break;
            case 5:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank05";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "中尉";
                break;
            case 6:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank06";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "上尉";
                break;
            case 7:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank07";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "少校";
                break;
            case 8:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank08";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "中校";
                break;
            case 9:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank09";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "上校";
                break;
            case 10:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank10";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "少将";
                break;
            case 11:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank11";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "中将";
                break;
            case 12:
                this.transform.Find("RankIcon").GetComponent<UISprite>().spriteName = "rank12";
                this.transform.Find("RankIcon/Label").GetComponent<UILabel>().text = "上将";
                break;
            default:
                break;
        }
         */
    }
}

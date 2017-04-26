using UnityEngine;
using System.Collections;
using System;

public class HeroSkillUpResultItem: MonoBehaviour 
{
    [SerializeField]
    private UILabel desLabel;
    [SerializeField]
    private UILabel leftLabel;
    [SerializeField]
    private UILabel rightLabel;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetHeroSkillUpResultItem(string desStr, int leftNum, int rightNum)
    {
        desLabel.text = desStr;
      //  StartCoroutine(AddFightNum(leftLabel,leftNum));
        leftLabel.text = leftNum.ToString();
        StartCoroutine(AddFightNum(rightLabel,rightNum));
    }
    public void SetHeroSkillUpResultItem(string desStr, float leftNum, float rightNum)
    {
        desLabel.text = desStr;
     //   StartCoroutine(AddFightNum(leftLabel, leftNum));
        leftLabel.text = string.Format("{0}%", (float)Math.Round(leftNum * 100, 1));
        StartCoroutine(AddFightNum(rightLabel, rightNum));
    }
    IEnumerator AddFightNum(UILabel label, int MaxFightNum)
    {
        label.text = "";
        yield return new WaitForSeconds(0.5f);
        if (MaxFightNum <= 50)
        {
            int count = 0;
            if (count == MaxFightNum)
            {
                label.text = string.Format("{0}", count);
            }
            while (count < MaxFightNum)
            {
                count += 1;
                yield return new WaitForSeconds(0.01f);
                if (count >= MaxFightNum)
                {
                    count = MaxFightNum;
                }
                label.text = string.Format("{0}", count);
            }
        }
        else
        {
            int count = MaxFightNum - 50;
            while (count < MaxFightNum)
            {
                count += 1;
                yield return new WaitForSeconds(0.01f);
                if (count >= MaxFightNum)
                {
                    count = MaxFightNum;
                }
                label.text = string.Format("{0}", count);
            }
        }
    }
    IEnumerator AddFightNum(UILabel label, float MaxFightNum)
    {
        label.text = "";

        yield return new WaitForSeconds(0.5f);

        float _MaxFightNumInt = (float)Math.Round(MaxFightNum * 100, 1); ;// MaxFightNum * 100;
        if (_MaxFightNumInt <= 50)
        {
            float count = 0;
            if (count == MaxFightNum)
            {
                label.text = string.Format("{0}%", count);
            }
            while (count < _MaxFightNumInt)
            {
                count += 1;
                yield return new WaitForSeconds(0.01f);
                if (count >= _MaxFightNumInt)
                {
                    count = _MaxFightNumInt;
                }
                label.text = string.Format("{0}%", count);
            }
        }
        else
        {
            float count = _MaxFightNumInt - 50;
            while (count < _MaxFightNumInt)
            {
                count += 1;
                yield return new WaitForSeconds(0.01f);
                if (count >= _MaxFightNumInt)
                {
                    count = _MaxFightNumInt;
                }
                label.text = string.Format("{0}%", count);
            }
        }
    }
}

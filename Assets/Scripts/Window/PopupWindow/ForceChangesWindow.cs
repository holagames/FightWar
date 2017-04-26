using UnityEngine;
using System.Collections;

public class ForceChangesWindow : MonoBehaviour {

    public GameObject ZhanLi_up;
    public GameObject SpriteEffect;
    public UILabel LabelForce;

    public void LookForceChange(int Forcebefore,int ForceNow) 
    {
        StartCoroutine(AddNumber(Forcebefore, ForceNow));
    }

    IEnumerator AddNumber(int Forcebefore, int ForceNow)
    {

        ZhanLi_up.SetActive(true);
        SpriteEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LabelForce.gameObject.SetActive(true);
        LabelForce.text = Forcebefore.ToString();
        yield return new WaitForSeconds(0.2f);
        //CharacterRecorder.instance.Fight = ForceNow;
        int count = CharacterRecorder.instance.FightOld;//Forcebefore;

        if ((CharacterRecorder.instance.Fight - CharacterRecorder.instance.FightOld) > 500)
        {
            count = CharacterRecorder.instance.Fight - 500;
        }

        while (count < CharacterRecorder.instance.Fight)
        {
            count += 10;//5
            yield return new WaitForSeconds(0.01f);
            if (count >= CharacterRecorder.instance.Fight)
            {
                count = CharacterRecorder.instance.Fight;
            }
            LabelForce.text = count.ToString();
        }
        yield return new WaitForSeconds(1f);
        SpriteEffect.SetActive(false);
        LabelForce.transform.GetComponent<TweenScale>().from = new Vector3(1f, 1, 1);
        LabelForce.transform.GetComponent<TweenScale>().to = new Vector3(0f, 0f, 0);
        LabelForce.transform.GetComponent<TweenScale>().ResetToBeginning();
        LabelForce.transform.GetComponent<TweenScale>().PlayForward();
        CharacterRecorder.instance.IsOpenFight = true;
        yield return new WaitForSeconds(0.6f);

        Destroy(this.gameObject);
    }
}

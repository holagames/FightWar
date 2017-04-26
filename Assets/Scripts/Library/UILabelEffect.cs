using UnityEngine;
using System.Collections;

public class UILabelEffect : MonoBehaviour {

    [SerializeField]
    private GameObject spriteBg;

    [SerializeField]
    UILabel thisLabel;

    [SerializeField]
    TweenPosition tweenPosition;

    [SerializeField]
    TweenAlpha tweenAlpha;

	// Use this for initialization
	void Start () {
        //GetComponent<UIPanel>().depth = UIManager.instance.GetNextDepth() + 4;
        if (GameObject.Find("LoginWindow") == null)
        {
            this.gameObject.layer = 11;
        }
        else
        {
            this.gameObject.layer = 5;
        }
	}
	

    public void SetLabelNormal(string content, Vector3 from, Vector3 to)
    {
        thisLabel.text = content;

        //tweenPosition.from = from;
        //tweenPosition.to = to;


        tweenPosition.from = new Vector3(0, 80f, 0);
        tweenPosition.to = new Vector3(0, 80f, 0);
        tweenPosition.ResetToBeginning();
        tweenAlpha.ResetToBeginning();
        tweenPosition.PlayForward();
        tweenAlpha.PlayForward();
        
    }

    public void SetLabelInLoginWindow(string content, Vector3 from, Vector3 to)
    {
        thisLabel.text = content;

        //tweenPosition.from = from;
        //tweenPosition.to = to;


        tweenPosition.from = from;
        tweenPosition.to = to;
        tweenPosition.ResetToBeginning();
        tweenAlpha.ResetToBeginning();
        tweenPosition.PlayForward();
        tweenAlpha.PlayForward();

    }

    public void SetLabelNormal(string content,bool isNeedUp, Vector3 from, Vector3 to)
    {
        thisLabel.text = content;
        if (isNeedUp)
        {
            spriteBg.gameObject.SetActive(false);
            tweenPosition.from = from;
            tweenPosition.to = to;
            tweenPosition.duration = 0.5f;
            tweenPosition.delay = 0.2f;

            tweenAlpha.duration = 0.6f;
            tweenAlpha.delay = 0.3f;
        }
        else
        {
            tweenPosition.from = new Vector3(0, 80f, 0);
            tweenPosition.to = new Vector3(0, 80f, 0);
        }
        tweenPosition.ResetToBeginning();
        tweenAlpha.ResetToBeginning();
        tweenPosition.PlayForward();
        tweenAlpha.PlayForward();
    }
    public void OnTweenFinish()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class TeamBossMoveTime : MonoBehaviour {

    public GameObject BossGab;
    private Vector3 targetPosition;
    private float Timer = 0;
    float time1 = 0;

    float MoveDistance = 0;

	void Start () {

	}


    void FixedUpdate()
    {

        if(BossGab!=null)
        {
            //Timer += Time.deltaTime;
            //if (Timer > 0.01f)
            //{
            //    Timer = 0;
            //    //if (BossGab.transform.localPosition.x > targetPosition.x)
            //    //{
            //    //    BossGab.transform.localPosition = new Vector3(BossGab.transform.localPosition.x - 37.5f, BossGab.transform.localPosition.y, BossGab.transform.localPosition.z);
            //    //}
            //    //else 
            //    //{
            //    //    BossGab.transform.localPosition = targetPosition;
            //    //}

            //    MoveDistance = Vector3.Distance(BossGab.transform.localPosition, targetPosition);

            //    if (MoveDistance >= Vector3.Distance(Vector3.zero, targetPosition * Time.deltaTime))
            //    {
            //        BossGab.transform.localPosition += targetPosition * Time.deltaTime;
            //    }

            //}
            //MoveDistance = Vector3.Distance(BossGab.transform.localPosition, targetPosition);

            //if (MoveDistance >= Vector3.Distance(Vector3.zero, targetPosition * Time.deltaTime))
            //{
            //    BossGab.transform.localPosition -= targetPosition * Time.deltaTime;
            //}

            if (BossGab.transform.localPosition.x > targetPosition.x)
            {
                BossGab.transform.localPosition = new Vector3(BossGab.transform.localPosition.x - 37.5f, BossGab.transform.localPosition.y, BossGab.transform.localPosition.z);
            }
            else
            {
                BossGab.transform.localPosition = targetPosition;
            }
        }       
	}


    public void SetBossMoveTime(GameObject _BossGab, Vector3 _targetPosition) 
    {
        this.BossGab = _BossGab;
        this.targetPosition = _targetPosition;
    }
}

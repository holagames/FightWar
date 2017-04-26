using UnityEngine;
using System.Collections;

public class PlayAniOrRotation : MonoBehaviour
{
    [SerializeField]
    private GameObject collisionButton;
    public GameObject targetRole;
    private Animator _Animator;

    public Transform modelTrans;

    private bool TouchDownflg = false;
    private float startX = 0f;
    private float angleStartX = 0f;
    private bool moveflg = false;
    private int RoleIndex = 0;
    private Hero mHero;
    public static bool IsNeedAutoPlayAni = false;
    // Use this for initialization
    void Start()
    {
        //ResetTargetRole(collisionButton, targetRole);
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionButton == null || targetRole == null)
        {
            return;
        }
        if (TouchDownflg)
        {
            float endX = Input.mousePosition.x;
            if (Mathf.Abs(startX - endX) > 10f)
            {
                moveflg = true;
                float gotoY = angleStartX + (startX - endX) * 0.2f;
                modelTrans.localEulerAngles = new Vector3(0f, gotoY, 0f);
            }
            else
            {
                moveflg = false;
            }
        }
    }
    public void ResetTargetRole(GameObject _targetRole, Hero _hero)
    {
        collisionButton = GameObject.Find("CollisionButton");
        if (_targetRole == null || collisionButton == null)
        {
            return;
        }
        targetRole = _targetRole;
        mHero = _hero;
        //targetRole = GameObject.FindGameObjectWithTag("Role");
        _Animator = targetRole.GetComponent<Animator>();
        modelTrans = targetRole.transform;
        StatInit();
    }

    void StatInit()
    {
        if (collisionButton.GetComponent<UIEventListener>() != null)
        {
            collisionButton.GetComponent<UIEventListener>().onPress = CollisionButtonOnPress;
        }
        else
        {
            UIEventListener listener = UIEventListener.Get(collisionButton);
            listener.onPress = CollisionButtonOnPress;
        }
        if (collisionButton.GetComponent<UIEventListener>() != null)
        {
            collisionButton.GetComponent<UIEventListener>().onClick = CollisionButtonOnClick;
        }
        else
        {
            UIEventListener listener = UIEventListener.Get(collisionButton);
            listener.onClick = CollisionButtonOnClick;
        }
        //旧的默认idle
        //   _Animator.Play("idle");
        if (IsNeedAutoPlayAni)
        {
            Invoke("PlaySkillAnimator", 0.5f);
            IsNeedAutoPlayAni = false;
        }

    }
    void PlaySkillAnimator()
    {
        _Animator.Play("skill");
        FightMotion fm2 = TextTranslator.instance.fightMotionDic[PictureCreater.instance.ListRolePicture[RoleIndex].RoleID * 10 + 2];
        PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, RoleIndex, fm2);
    }

    void CollisionButtonOnPress(GameObject go, bool flg)
    {
        if (!flg)
        {
            // Debug.Log("????????????????" + flg);
            if (moveflg == true)
            {
                TouchDownflg = false;
                return;
            }
            TouchDownflg = false;
        }
        else
        {
            Vector3 curAngles = modelTrans.localEulerAngles;
            angleStartX = curAngles.y;
            startX = Input.mousePosition.x;
            TouchDownflg = true;
        }
    }
    void CollisionButtonOnClick(GameObject go)
    {
        if (go != null)
        {
            //Debug.Log("----collisionButtonOnClick------");
            int _randomInt = Random.Range(0, 4);
            /*       switch(_randomInt)
                   {
                       case 0: _Animator.Play("attack"); Invoke("BackToIdle", 2f); break;
                       case 1: _Animator.Play("skill"); Invoke("BackToIdle", 2f); break;
                       case 2: _Animator.Play("run"); Invoke("BackToIdle", 4f); break;
                       default: _Animator.Play("idle");break;
                   }*/
            //Debug.LogError("random..." + _randomInt);
            StartCoroutine(ShowAnimation(_randomInt, RoleIndex));
            TouchDownflg = false;
        }
    }
    private void BackToIdle()
    {
        //this.GetComponentInChildren<Animator>().Play("idle");
        if (_Animator != null)
        {
            _Animator.Play("idle");
        }
    }
    IEnumerator ShowAnimation(int randomInt, int SetRoleIndex)
    {
        yield return new WaitForSeconds(0.01f);

        AnimatorStateInfo animatorState = _Animator.GetCurrentAnimatorStateInfo(0);
        if (animatorState.IsName("Base Layer.idle"))
        {
            switch (randomInt)
            {
                case 0:
                    _Animator.Play("attack");
                    FightMotion fm = TextTranslator.instance.fightMotionDic[PictureCreater.instance.ListRolePicture[SetRoleIndex].RoleID * 10 + 1];
                    PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, SetRoleIndex, fm);
                    break;
                case 1: _Animator.Play("skill");
                    FightMotion fm2 = TextTranslator.instance.fightMotionDic[PictureCreater.instance.ListRolePicture[SetRoleIndex].RoleID * 10 + 2];
                    PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, SetRoleIndex, fm2);
                    break;
                case 2:
                    _Animator.Play("run"); break;
                case 3:
                    _Animator.Play("hurt"); break;
            }
            PictureCreater.instance.PlayRoleSound(mHero.cardID);//播放口头禅
        }
    }
}

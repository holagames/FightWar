using UnityEngine;
using System.Collections;

public class LegionWarCamera : MonoBehaviour {

    public Transform Obj;
    private float SwidthHalf = 0f;
    private float SheightHalf = 0f;

    float widthLegth = 0;
    float heightLegth = 0;

    float x = 0;
    float y = 0;


    private Vector2 NewForward = new Vector2();

    private bool IsMove = false;

    public float Speed = 2f;
    public float AddNum = 30f;//10
    Vector3 Newvec=new Vector3();


    void Start() 
    {
        float height = GameObject.FindObjectOfType<UIRoot>().activeHeight;
        float width = height * (float)Screen.width / ((float)Screen.height);

        //Debug.LogError(height);
        //Debug.LogError(width);
        x = (width - 1200) / 2f;
        //y = (height - 800) / 2f;
        y = (height - 800 * UIRootExtend.instance.isUiRootRatio) / 2f;

    }

    void OnDrag(Vector2 move) //拖动用  410x,170y
    {
        IsMove = false;
        Vector2 vec = new Vector2(Obj.transform.localPosition.x, Obj.transform.localPosition.y)+move;
        NewForward = move;
        if (vec.x <= -500f + x)
        {
            vec.x = -500f + x;
        }
        else if (vec.x >= 500f - x)
        {
            vec.x = 500f - x;
        }

        if (vec.y <= -250f + y)
        {
            vec.y = -250f + y;
        }
        else if (vec.y >= 250f - y)
        {
            vec.y = 250f - y;
        }
        Obj.transform.localPosition = new Vector3(vec.x, vec.y, 0);
    }

    void OnDragEnd() 
    {
        IsMove = true;
        Newvec = Obj.transform.localPosition + new Vector3(NewForward.x * AddNum, NewForward.y * AddNum, 0);
    }

    void Update() //滑动用
    {
        if (IsMove) 
        {
            if (Newvec.x <= -500f + x)
            {
                Newvec.x = -500f + x;
            }
            else if (Newvec.x >= 500f - x)
            {
                Newvec.x = 500f - x;
            }

            if (Newvec.y <= -250f + y)
            {
                Newvec.y = -250f + y;
            }
            else if (Newvec.y >= 250f - y)
            {
                Newvec.y = 250f - y;
            }
            float step = Speed * Time.deltaTime;
            Obj.transform.localPosition = new Vector3(Mathf.Lerp(Obj.transform.localPosition.x, Newvec.x, step), Mathf.Lerp(Obj.transform.localPosition.y, Newvec.y, step), Mathf.Lerp(Obj.transform.localPosition.z, Newvec.z, step));
        }
    }
    public void GotoCityNum(Vector3 vec) //定位用
    {
        IsMove = false;
        if (vec.x <= -500f + x)
        {
            vec.x = -500f + x;
        }
        else if (vec.x >= 500f - x)
        {
            vec.x = 500f - x;
        }

        if (vec.y <= -250f + y)
        {
            vec.y = -250f + y;
        }
        else if (vec.y >= 250f - y)
        {
            vec.y = 250f - y;
        }
        Obj.transform.localPosition = new Vector3(vec.x, vec.y, 0);
    }


    public void ControlOnDrag(Vector2 move) //ui城市点拖动用
    {
        IsMove = false;
        Vector2 vec = new Vector2(Obj.transform.localPosition.x, Obj.transform.localPosition.y) + move;
        NewForward = move;
        if (vec.x <= -500f + x)
        {
            vec.x = -500f + x;
        }
        else if (vec.x >= 500f - x)
        {
            vec.x = 500f - x;
        }

        if (vec.y <= -250f + y)
        {
            vec.y = -250f + y;
        }
        else if (vec.y >= 250f - y)
        {
            vec.y = 250f - y;
        }
        Obj.transform.localPosition = new Vector3(vec.x, vec.y, 0);
    }

    public void ControlOnDragEnd()//ui城市点拖动用
    {
        IsMove = true;
        Newvec = Obj.transform.localPosition + new Vector3(NewForward.x * AddNum, NewForward.y * AddNum, 0);
    }
}

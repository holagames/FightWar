using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatGrid : UIWidgetContainer
{
    public delegate void OnReposition();

    public enum Arrangement
    {
        Horizontal,
        Vertical,
    }

    public Arrangement arrangement = Arrangement.Horizontal;

    public int maxPerLine = 0;

    public bool animateSmoothly = false;

    public bool sorted = false;

    public bool hideInactive = true;

    public OnReposition onReposition;

    public bool repositionNow { set { if (value) { mReposition = true; enabled = true; } } }

    bool mStarted = false;

    bool mReposition = false;

    UIPanel mPanel;

    UIScrollView mDrag;

    bool mInitDone = false;

    void Init()
    {
        mInitDone = true;
        mPanel = NGUITools.FindInParents<UIPanel>(gameObject);
        mDrag = NGUITools.FindInParents<UIScrollView>(gameObject);
    }

    void Start()
    {
        if (!mInitDone) Init();
        mStarted = true;
        bool smooth = animateSmoothly;
        animateSmoothly = false;
        Reposition();
        animateSmoothly = smooth;
        enabled = false;
    }

    void Update()
    {
        if (mReposition) Reposition();
        enabled = false;
    }

    static public int SortByName(Transform a, Transform b) { return string.Compare(a.name, b.name); }

    [ContextMenu("Execute")]
    public void Reposition()
    {
        if (Application.isPlaying && !mStarted)
        {
            mReposition = true;
            return;
        }

        if (!mInitDone) Init();

        mReposition = false;
        Transform myTrans = transform;

        int x = 0;
        int y = 0;
        int height = 0;

        for (int i = 0; i < myTrans.childCount; ++i)
        {
            Transform t = myTrans.GetChild(i);

            UISprite cell = t.FindChild("TextBoard").gameObject.GetComponent<UISprite>();

            if (!NGUITools.GetActive(t.gameObject) && hideInactive) continue;

            float depth = t.localPosition.z;

            Vector3 pos = new Vector3(0, -height, depth);
            //旧的
            //height += cell.height;
            if (!t.transform.FindChild("ApplayButton").gameObject.activeSelf)
            {
                height += cell.height + 60;
            }
            else
            {
                height += cell.height + 90;
            }

            if (animateSmoothly && Application.isPlaying)
            {
                SpringPosition.Begin(t.gameObject, pos, 15f);
            }
            else t.localPosition = pos;

            if (++x >= maxPerLine && maxPerLine > 0)
            {
                x = 0;
                ++y;
            }
        }

        if (mDrag != null)
        {
            mDrag.UpdateScrollbars(true);
            mDrag.RestrictWithinBounds(true);
        }
        else if (mPanel != null)
        {
            mPanel.ConstrainTargetToBounds(myTrans, true);
        }

        if (onReposition != null)
            onReposition();
    }
}

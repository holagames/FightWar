using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RebirthSuccess : MonoBehaviour
{

    public GameObject successGrid;
    public GameObject rebirthItemPrefab;

    private List<GameObject> presentGridObj = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        #region   successRebirth点击事件
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            //UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            //{
            //    GameObject rw = GameObject.Find("RebirthWindow");
            //    rw.transform.FindChild("MaskSprite").gameObject.SetActive(false);
            //    Destroy(this.gameObject);
            //};
        }
        #endregion
    }
    /// <summary>
    /// 设置英雄重生成功面板的信息
    /// </summary>
    /// <param name="roleId">英雄的ID号</param>
    public void SetSuccessRebirthPanel(int roleId)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
        UILabel roleNameLabel = this.gameObject.transform.Find("RoleNameLabel").GetComponent<UILabel>();
        roleNameLabel.text = string.Format("[fdf1bf]英雄 {0} [fdf1bf]已焕然一新，返还可用于强化其他英雄.", TextTranslator.instance.SetHeroNameColor(hero.name, hero.classNumber));
        InitSuccessRebirthPanel();
    }
    /// <summary>
    /// 初始化成功按钮事件的点击事件
    /// </summary>
    public void InitSuccessRebirthPanel()
    {
        GameObject sureBtn = this.gameObject.transform.Find("SureBtn").gameObject;
        if (UIEventListener.Get(sureBtn).onClick == null)
        {
            UIEventListener.Get(sureBtn).onClick += delegate(GameObject go)
            {
                GameObject rw = GameObject.Find("RebirthWindow");
                rw.transform.FindChild("MaskSprite").gameObject.SetActive(false);
                Destroy(this.gameObject);
            };
        }
    }
    /// <summary>
    /// 启动重生成功的协程
    /// </summary>
    /// <param name="isComplete">是否播放完动画</param>
    /// <param name="agreement">协议</param>
    /// <param name="roleId">当前角色的ID</param>
    public void StartSuccessCoroutine(bool isComplete, List<string> agreement, int roleId, int rebirthIndex, int diamond)
    {
        StartCoroutine(SuccessRebirthBack(isComplete, agreement, roleId, rebirthIndex, diamond));
    }

    /// <summary>
    /// 成功重生时候调用的协程
    /// </summary>
    /// <param name="isComplete">动画是否播放完毕</param>
    /// <param name="agreement">重生完成返回的道具列表集合</param>
    /// <returns></returns>
    IEnumerator SuccessRebirthBack(bool isComplete, List<string> agreement, int roleId, int rebirthIndex, int diamond)
    {
        yield return new WaitForSeconds(1.2f);
        
        isComplete = true;
        if (isComplete)//动画播放完成且返回数值正确
        {
            DestroyGridAllChild();
            successGrid.transform.parent.localPosition = Vector3.zero;
            successGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
            for (int i = 0; i < agreement.Count; i++)
            {
                string[] dataSplit = agreement[i].Split('$');
                GameObject go = NGUITools.AddChild(successGrid, rebirthItemPrefab);
                presentGridObj.Add(go);
                go.name = dataSplit[0];
                go.GetComponent<RebirthItem>().SetRebirthItemInfo(int.Parse(dataSplit[0]), int.Parse(dataSplit[1]), "RebirthSuccess");
            }
            successGrid.GetComponent<UIGrid>().Reposition();
            AudioEditer.instance.PlayOneShot("ui_levelup");

            GameObject top = GameObject.Find("TopContent");
            if (top!=null)
            {
                CharacterRecorder.instance.lunaGem = diamond;
                top.GetComponent<TopContent>().Reset();
            }

            Hero hero = CharacterRecorder.instance.GetHeroByRoleID(roleId);
            NetworkHandler.instance.SendProcess(string.Format("1005#{0};", hero.characterRoleID));
            NetworkHandler.instance.SendProcess(string.Format("3001#{0};", hero.characterRoleID));
            NetworkHandler.instance.SendProcess(string.Format("3101#{0};", hero.characterRoleID));
            SetSuccessRebirthPanel(roleId);
            // NetworkHandler.instance.SendProcess(string.Format("3201#{0};", hero.characterRoleID));
        }
    }
    /// <summary>
    /// 销毁当前的物品集合内的GameObject
    /// </summary>
    public void DestroyGridAllChild()
    {
        for (int i = 0; i < presentGridObj.Count; i++)
        {
            DestroyImmediate(presentGridObj[i]);
        }
        presentGridObj.Clear();
        successGrid.GetComponent<UIGrid>().Reposition();
    }
}

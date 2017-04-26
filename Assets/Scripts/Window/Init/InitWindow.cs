using UnityEngine;
using System.Collections;

public class InitWindow : MonoBehaviour {

   
    public GameObject Logo;

    void Start()
    {
        UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_100);
        NetworkHandler.instance.SendProcess("9801#");
    } 
}

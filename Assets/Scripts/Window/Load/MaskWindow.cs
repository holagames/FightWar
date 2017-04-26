using UnityEngine;
using System.Collections;

public class MaskWindow : MonoBehaviour {

    float IdleTimer;
   
    void OnEnable()
    {
        IdleTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        IdleTimer += Time.deltaTime;
        if (IdleTimer > 2f)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;

public class NuclearwarItem : MonoBehaviour
{
    public UISprite Item;
    public UILabel num;
  public void Init(int name,int namber)
    {
        Item.spriteName = name.ToString();
        num.text = namber.ToString();
        TextTranslator.instance.ItemDescription(this.gameObject, name, 0);
    }
}
        

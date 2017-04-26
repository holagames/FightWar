using UnityEngine;
using System.Collections;

public class NewGuide
{
    public int NewGuideID { get; private set; }      //流水号Id
    public int Level { get; private set; }           //等级
    public string Name { get; private set; }           //
    public string Des { get; private set; }          //
    public string MainIcon { get; private set; }          //
    public string LevelUpTipIcon { get; private set; }       //
    public string MapDes { get; private set; }       //
    public NewGuide(int NewGuideID, int Level, string Name, string Des, string MainIcon, string LevelUpTipIcon, string MapDes)
    {
        this.NewGuideID = NewGuideID;
        this.Level = Level;
        this.Name = Name;
        this.Des = Des;
        this.MainIcon = MainIcon;
        this.LevelUpTipIcon = LevelUpTipIcon;
        this.MapDes = MapDes;
    }
}

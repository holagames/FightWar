using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Question
{
    public int QuestionID { get; set; }
    public int Type { get; set; }
    public int Option { get; set; }
    public string Name { get; set; }

    public BetterList<string> SelectionList = new BetterList<string>();
    public Question(int QuestionID, int Type, int Option, string Name, BetterList<string> SelectionList)
    {
        this.QuestionID = QuestionID;
        this.Type = Type;
        this.Option = Option;
        this.Name = Name;
        this.SelectionList = SelectionList;
    }
    
}

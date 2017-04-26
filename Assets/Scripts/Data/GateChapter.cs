using UnityEngine;
using System.Collections;

public class GateChapter{

    public int chapterID { get; private set; }

    public string name { get; private set; }

    public int level { get; private set; }

    public GateChapter(int newChapterID, string newName, int newlevel)
    {
        this.chapterID = newChapterID;
        this.name = newName;
        this.level = newlevel;
    }
}

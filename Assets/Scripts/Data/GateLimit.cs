using UnityEngine;
using System.Collections;

public class GateLimit
{

    public int GateLimitID { get; private set; }
    public int LimitTerm { get; private set; }

    public int LimitParam1 { get; private set; }
    public int LimitParam2 { get; private set; }
    public int WinTerm { get; private set; }
    public int WinParam1 { get; private set; }
    public int WinParam2 { get; private set; }

    public int Star2Term { get; private set; }
    public int Star2Param1 { get; private set; }
    public int Star2Param2 { get; private set; }
    public int Star3Term { get; private set; }

    public int Star3Param1 { get; private set; }
    public int Star3Param2 { get; private set; }

    public int Highlight { get; private set; }

    public GateLimit(int GateLimitID, int LimitTerm, int LimitParam1, int LimitParam2, int WinTerm, int WinParam1, int WinParam2, int Star2Term,
        int Star2Param1, int Star2Param2, int Star3Term, int Star3Param1, int Star3Param2, int Highlight)
    {
        this.GateLimitID = GateLimitID;
        this.LimitTerm = LimitTerm;
        this.LimitParam1 = LimitParam1;
        this.LimitParam2 = LimitParam2;
        this.WinTerm = WinTerm;
        this.WinParam1 = WinParam1;
        this.WinParam2 = WinParam2;
        this.Star2Term = Star2Term;
        this.Star2Param1 = Star2Param1;
        this.Star2Param2 = Star2Param2;
        this.Star3Term = Star3Term;
        this.Star3Param1 = Star3Param1;
        this.Star3Param2 = Star3Param2;
        this.Highlight = Highlight;
    }


}

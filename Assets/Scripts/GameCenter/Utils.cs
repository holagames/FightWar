
using System;
using UnityEngine;
public class Utils {

    public static DateTime GetTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }

    public static long GetNowTimeUTC()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds);
        
    }

    public static Color GetColorByGrade(int grade)
    {
        switch (grade)
        {
            case 1:
                return new Color((float)Convert.ToInt32("db", 16), (float)Convert.ToInt32("db", 16), (float)Convert.ToInt32("db", 16));          
            case 2:
                return new Color((float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("ff", 16), (float)Convert.ToInt32("3c", 16));
            case 3:
                return new Color((float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("ff", 16), (float)Convert.ToInt32("3c", 16));
            case 4:
                return new Color((float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("ff", 16), (float)Convert.ToInt32("3c", 16));
            case 5:
                return new Color((float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("9c", 16), (float)Convert.ToInt32("ff", 16));
            case 6:
                return new Color((float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("9c", 16), (float)Convert.ToInt32("ff", 16));               
            case 7:
                return new Color((float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("9c", 16), (float)Convert.ToInt32("ff", 16));
            case 8:
                return new Color((float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("9c", 16), (float)Convert.ToInt32("ff", 16));
            case 9:
                return new Color((float)Convert.ToInt32("b5", 16), (float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("ff", 16));                
            case 10:
                return new Color((float)Convert.ToInt32("b5", 16), (float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("ff", 16));
            case 11:
                return new Color((float)Convert.ToInt32("b5", 16), (float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("ff", 16));
            case 12:
                return new Color((float)Convert.ToInt32("b5", 16), (float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("ff", 16));               
            case 13:
                return new Color((float)Convert.ToInt32("b5", 16), (float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("ff", 16));
            case 14:
                return new Color((float)Convert.ToInt32("ff", 16), (float)Convert.ToInt32("6c", 16), (float)Convert.ToInt32("00", 16));
            case 15:
                return new Color((float)Convert.ToInt32("ff", 16), (float)Convert.ToInt32("6c", 16), (float)Convert.ToInt32("00", 16));
            case 16:
                return new Color((float)Convert.ToInt32("ff", 16), (float)Convert.ToInt32("6c", 16), (float)Convert.ToInt32("00", 16));
            case 17:
                return new Color((float)Convert.ToInt32("ff", 16), (float)Convert.ToInt32("6c", 16), (float)Convert.ToInt32("00", 16));
            case 18:
                return new Color((float)Convert.ToInt32("ff", 16), (float)Convert.ToInt32("6c", 16), (float)Convert.ToInt32("00", 16));
            case 19:
                return new Color((float)Convert.ToInt32("ff", 16), (float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("00", 16));
            case 20:
                return new Color((float)Convert.ToInt32("ff", 16), (float)Convert.ToInt32("00", 16), (float)Convert.ToInt32("00", 16));
            default:
                return   Color.white;
        }
    }
}

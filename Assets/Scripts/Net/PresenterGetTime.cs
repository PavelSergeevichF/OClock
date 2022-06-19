using System;
using System.Threading;
using UnityEngine;

public class PresenterGetTime: MonoBehaviour
{
    [SerializeField] private SOSources sOSources;
    [SerializeField]  private SOModel sOModel;

    void Start()
    {
        GetTime();
    }


    public void GetTime()
    {
        sOModel.timeModel[0] = GetTimeToSource(sOSources.Sources1);
        sOModel.timeModel[1] = GetTimeToSource(sOSources.Sources2);
    }
    private TimeModel GetTimeToSource(string Source)
    {
        int space = 0;
        string timeStr = "";
        TimeModel timeModeltemp;
        timeModeltemp.Second = 0; timeModeltemp.Minute = 0; timeModeltemp.Hour = 0;
        var www = new WWW(Source);
        while (!www.isDone && www.error == null)
            Thread.Sleep(1);
        var str = www.responseHeaders["Date"];
        foreach(char ch in str)
        {
            if(ch==' ')
            {
                space++;
            }
            if(space>=4&&space<5)
            {
                timeStr += ch;
            }
        }
        str = "";
        int pos = 0;
        int[] time = new int[3];
        foreach (char ch in timeStr)
        { 
            if(ch==':')
            {
                int.TryParse(str, out time[pos]);
                pos++;
                str = "";
            }
            else 
            {
                str += ch;
            }
        }
        int.TryParse(str, out time[pos]);
        timeModeltemp.Second = time[2];
        timeModeltemp.Minute = time[1];
        timeModeltemp.Hour = time[0]+3;
        if(timeModeltemp.Hour>23)
        {
            timeModeltemp.Hour -= 24;
        }
        return timeModeltemp;
    }
}

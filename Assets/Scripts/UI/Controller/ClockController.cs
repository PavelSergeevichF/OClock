using UnityEngine;
using DG.Tweening;

public class ClockController
{
    private ViewClock viewClock;
    private SOModel sOModel;
    private TimeModel timeModel;
    private TimeModel alarmTime;
    private MathTime mathTime;
    private Vector3 rotateHour;
    private Vector3 rotateMinute;
    private Vector3 rotateSecond;
    private bool panelActiv = false;
    private bool alarm = false;
    private bool alarmActiv = false;
    private bool checkError = false;
    private int frame = 0;
    private int checkHouer;
    private const int maxFrame = 59;
    public ClockController(ViewClock _viewClock, SOModel _sOModel)
    {
        viewClock = _viewClock;
        sOModel = _sOModel;
        mathTime = new MathTime();
    }
    public void ControllerStart()
    {
        setTime();
        setDigitalTime();
        viewClock.PanelAlarm.SetActive(panelActiv);
        viewClock.ImageAlarm.SetActive(alarm);
        checkHouer = timeModel.Hour;
    }
    public void ControllerUpdate()
    {
        moveArrow();
    }
    public void ControllerFixedUpdate()
    {
        if (frame >= maxFrame)
        {
            mathTime.MathTimeCount(ref timeModel);
            setDigitalTime();
            if(timeModel.Hour!= checkHouer)
            {
                checkHouer = timeModel.Hour;
                setTime();
            }
            frame = 0;
            checkAlarm();
        }
        else
        {
            frame++;
        }
    }

    private void checkAlarm()
    {
        if (alarm)
        {
            if (alarmTime.Hour == timeModel.Hour && alarmTime.Minute == timeModel.Minute)
            {
                alarmActiv = true;
                viewClock.AlarmSound.Play();
            }
        }
    }
    public void SetAlarm()
    {
        panelActiv = panelActiv ? false : true;
        viewClock.PanelAlarm.SetActive(panelActiv);
    }
    private void setTime()
    {
        viewClock.presenterGetTime.GetTime();
        timeModel = sOModel.timeModel[0];
    }
    private void setDigitalTime()
    {
        string Second = interpreterTime(timeModel.Second);
        string Minute = interpreterTime(timeModel.Minute);
        string Hour   = interpreterTime(timeModel.Hour);
        
        string timeTemp= $"{Hour}:{Minute}:{Second}";
        viewClock.Text.text = timeTemp;
    }
    public void ButtomOK()
    {
        checkError = false;
        alarmTime.Second = 0;
        int houerTemp = 0;
        int minuteTemp = 0;
        bool tempcheck = int.TryParse(viewClock.InputHour.text, out houerTemp);
        checkError = tempcheck ? false : true;
        if (!checkError)
        {
            tempcheck = int.TryParse(viewClock.InputMinute.text, out minuteTemp);
            checkError = tempcheck ? false : true;
        }
        if (houerTemp > 23 || houerTemp < 0)
        {
            checkError = true;
        }
        if (minuteTemp > 59 || minuteTemp < 0)
        {
            checkError = true;
        }
        Debug.Log($"checkError={checkError}, houerTemp={houerTemp}: minuteTemp={minuteTemp}");
        if(!checkError)
        {
            alarmTime.Hour = houerTemp;
            alarmTime.Minute = minuteTemp;
            panelActiv = false;
            viewClock.PanelAlarm.SetActive(panelActiv);
            alarm = true;
            viewClock.ImageAlarm.SetActive(alarm);
        }
    }
    public void OffAlarm()
    {
        if (alarm && alarmActiv)
        {
            viewClock.AlarmSound.Stop();
        }
        alarm = false;
        alarmActiv = false;
        viewClock.ImageAlarm.SetActive(alarm);
    }
    private void moveArrow()
    {
        rotateSecond.z = timeModel.Second * -6;
        rotateMinute.z = timeModel.Minute * -6;
        rotateHour.z = timeModel.Hour * -30 + -timeModel.Minute / 2;
        viewClock.SecondHand.transform.rotation = Quaternion.Euler(rotateSecond);
        viewClock.MinuteHand.transform.rotation = Quaternion.Euler(rotateMinute);
        viewClock.HourHand.transform.rotation   = Quaternion.Euler(rotateHour);
    }
    private string interpreterTime(int time)
    {
        string data;
        if (time < 10)
        {
            data = "0" + (time).ToString();
        }
        else
        {
            data = (time).ToString();
        }
        return data;
    }
    
}

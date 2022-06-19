using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewClock : MonoBehaviour
{
    [SerializeField] private ClockController clockController;
    [SerializeField] public PresenterGetTime presenterGetTime;
    [SerializeField] private SOModel sOModel;
    [SerializeField] private SOAlarm SOAlarm;
    public AudioSource AlarmSound;
    public InputField InputHour;
    public InputField InputMinute;
    public GameObject HourHand;
    public GameObject MinuteHand;
    public GameObject SecondHand;
    public GameObject AlarmHourHand;
    public GameObject AlarmMinuteHand;
    public GameObject PanelAlarm;
    public GameObject ImageAlarm;
    public Text Text;

    void Start()
    {
        clockController = new ClockController(this, sOModel);
        clockController.ControllerStart();
    }


    void Update() => clockController.ControllerUpdate();
    private void FixedUpdate() => clockController.ControllerFixedUpdate();
    public void SetAlarm() => clockController.SetAlarm();
    public void ButtomOK() => clockController.ButtomOK();
    public void OffAlarm() => clockController.OffAlarm();
}
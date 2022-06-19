using UnityEngine;

[CreateAssetMenu(fileName = nameof(SOAlarm), menuName = "SO/" + nameof(SOAlarm), order = 0)]
public class SOAlarm : ScriptableObject
{
    public TimeModel TimeModel;
}
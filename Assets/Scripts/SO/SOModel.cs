using UnityEngine;

[CreateAssetMenu(fileName = nameof(SOModel), menuName = "SO/" + nameof(SOModel), order = 0)]
public class SOModel : ScriptableObject
{
    public TimeModel[] timeModel = new TimeModel[2];
}
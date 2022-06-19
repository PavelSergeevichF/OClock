public class MathTime
{
    public MathTime()
    {
    }
    public void MathTimeCount(ref TimeModel timeModel)
    {
        if (timeModel.Second < 59)
        {
            timeModel.Second++;
        }
        else
        {
            timeModel.Second = 0;
            if (timeModel.Minute < 59)
            {
                timeModel.Minute++;
            }
            else
            {
                timeModel.Minute = 0;
                if (timeModel.Hour < 23)
                {
                    timeModel.Hour++;
                }
                else
                {
                    timeModel.Hour = 0;
                }
            }
        }
    }
}
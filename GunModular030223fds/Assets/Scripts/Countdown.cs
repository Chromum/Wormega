using UnityEngine;

[System.Serializable]
public struct Countdown
{
    public bool Finished;
    public float value;
    public float Count;
    public void CountdownUpdate()
    {
        if (!Finished)
            value -= Time.deltaTime;
        if (value <= 0)
            Finished = true;

    }

    public bool HasFinished() { return Finished; }
    
    public void StartCountdown()
    {
        value = Count;
        Finished = false;
    }


}
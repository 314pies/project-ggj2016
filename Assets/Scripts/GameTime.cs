using UnityEngine;

public class GameTime
{
    private float timer = 0f;
    
    public void Tick()
    {
        timer += Time.deltaTime;
    }

    public float GetTime()
    {
        return timer;
    }

    public void Reset()
    {
        timer = 0f;
    }
}

using UnityEngine;

[System.Serializable]
public class AIControlSetting
{
    public float controlChangingTime = 0.1f;
}

public class AIControl
{
    private AIControlSetting aiControlSetting = null;
    private Character character = null;
    private float timer = 0f;
    private Vector2 controlDirection = Vector2.zero;

    public AIControl( AIControlSetting aiControlSetting, Character character )
    {
        this.aiControlSetting = aiControlSetting;
        this.character = character;
    }

    public void Tick()
    {
        timer += Time.deltaTime;
        if ( timer >= aiControlSetting.controlChangingTime )
        {
            controlDirection = new Vector2( Random.Range( -1f, 1f ), Random.Range( -1f, 1f ) );
            timer = 0f;
        }

        character.Move( controlDirection.x, controlDirection.y );
    }
}
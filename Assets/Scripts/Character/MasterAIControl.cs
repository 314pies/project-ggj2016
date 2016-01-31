using UnityEngine;

public class MasterAIControl
{
    [SerializeField]
    private float controlChangingTime = 2f;

    private Master character = null;
    private float timer = 0f;
    private Vector2 controlDirection = Vector2.zero;

    public MasterAIControl( Master character )
    {
        this.character = character;
    }

    public void Tick()
    {
        //character.CircleMove();
        UpdateMoveDecision();
    }

    private void UpdateMoveDecision()
    {
        timer += Time.deltaTime;
        if ( timer >= controlChangingTime )
        {
            controlDirection = new Vector2( Random.Range( -1f, 1f ), Random.Range( -1f, 1f ) );
            timer = 0f;
        }
        if (MpLobby.IsServer)
            character.Move( controlDirection.x, controlDirection.y );
    }
}
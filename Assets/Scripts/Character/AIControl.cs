using UnityEngine;

[System.Serializable]
public class AIControlSetting
{
    public float controlChangingTime = 0.1f;
    public float actionDelayRelease = 0.15f;
}

public class AIControl
{
    private AIControlSetting aiControlSetting = null;
    private Character character = null;
    private Master master = null;

    private float timer = 0f;
    private float actionTimer = 0f;
    private Vector2 controlDirection = Vector2.zero;

    /*For Networking*/
    public int NetworkId;
    public NetworkControllerInGame InGameNetManager = null;

    /**/
    public AIControl(AIControlSetting aiControlSetting, Character character, Master master)
    {
        this.aiControlSetting = aiControlSetting;
        this.character = character;
        this.master = master;
    }

    public void Tick()
    {
        character.Tick();
        UpdateMoveDecision();
        UpdateActionDecision();
    }

    private void UpdateMoveDecision()
    {
        timer += Time.deltaTime;
        if (timer >= aiControlSetting.controlChangingTime)
        {
            Vector2 controlDirectionTemp = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timer = 0f;
            if (MpLobby.IsServer)
            {
                InGameNetManager.SendMoveDir(controlDirectionTemp.x, controlDirectionTemp.y, NetworkId);
                //
            }
        }
        character.Move(controlDirection.x, controlDirection.y);
    }

    public void UpdateMoveDecision_Remote(float Dirx, float DirY)
    {
        controlDirection.x = Dirx;
        controlDirection.y = DirY;

        //  character.Move(Dirx, DirY);
    }

    private void UpdateActionDecision()
    {
        if (master.IsInLightRange(character.GetPosition()) == true)
        {
            actionTimer = 0f;
            character.DoAction(true);
        }
        else
        {
            if (actionTimer < aiControlSetting.actionDelayRelease)
            {
                actionTimer += Time.deltaTime;
            }

            if (actionTimer >= aiControlSetting.actionDelayRelease)
            {
                character.DoAction(false);
            }
        }
    }
}
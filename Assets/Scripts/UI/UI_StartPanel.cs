using UnityEngine;

public class UI_StartPanel : SimpleUI
{
    public override void Initialize()
    {
        EventManager.StartListening("OnEnterGameState", Hide);
    }
}

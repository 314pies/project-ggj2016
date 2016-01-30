using UnityEngine;

public class UI_FailedPanel : SimpleUI
{
    public override void Initialize()
    {
        EventManager.StartListening("OnEnterFailState", Show);
        EventManager.StartListening("OnEnterGameState", Hide);
    }
}
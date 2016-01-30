using UnityEngine;

public class UI_SuccessPanel : SimpleUI
{
    public override void Initialize()
    {
        EventManager.StartListening("OnEnterSuccessState", Show);
        EventManager.StartListening("OnEnterGameState", Hide);
    }
}
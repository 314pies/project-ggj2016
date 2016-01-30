public class UI_FailedPanel : SimpleUI
{
    public override void Initialize()
    {
        EventManager.StartListening( EventDictionary.ON_ENTER_FAILED_STATE, Show );
        EventManager.StartListening( EventDictionary.ON_ENTER_GAME_STATE, Hide );
    }
}
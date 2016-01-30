public class UI_StartPanel : SimpleUI
{
    public override void Initialize()
    {
        EventManager.StartListening( EventDictionary.ON_ENTER_GAME_STATE, Hide );
    }
}
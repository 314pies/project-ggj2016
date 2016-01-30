using UnityEngine;
using UnityEngine.UI;

public class UI_SuccessPanel : SimpleUI
{
    [SerializeField]
    public Text timeText = null;

    private GameTime gameTime = null;

    public override void Initialize()
    {
        EventManager.StartListening( EventDictionary.ON_ENTER_SUCCESS_STATE, UpdateTimeText );
        EventManager.StartListening( EventDictionary.ON_ENTER_SUCCESS_STATE, Show );
        EventManager.StartListening( EventDictionary.ON_ENTER_GAME_STATE, Hide );
    }

    public void SetGameTime( GameTime gameTime )
    {
        this.gameTime = gameTime;
    }

    private void UpdateTimeText()
    {
        timeText.text = gameTime.GetTime().ToString();
    }
}
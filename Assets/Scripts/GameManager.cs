using UnityEngine;
using System.Collections.Generic;

public class GameManager
{
    private GameTime gameTime = null;
    private Character player = null;
    private List<Character> aiCharacterList = null;
    private Master master = null;
    private ControllerManager controllerManager = null;

    enum GameState { Start, InGame, Success, Fail }
    GameState gameState = GameState.Start;
    private int remainAINumber = 0;

    public GameManager( GameTime gameTime, ControllerManager controllerManager, Character player, List<Character> aiCharacterList, Master master )
    {
        this.gameTime = gameTime;
        this.controllerManager = controllerManager;
        this.player = player;
        this.aiCharacterList = aiCharacterList;
        this.master = master;

        remainAINumber = aiCharacterList.Count;
    }

    public void Tick()
    {
        if ( gameState == GameState.InGame )
        {
            gameTime.Tick();
            controllerManager.Tick();
            for ( int i = 0; i < aiCharacterList.Count; ++i )
            {
                if ( aiCharacterList[ i ].IsAlive() == true && master.IsInLightRange( aiCharacterList[ i ].GetPosition() ) == true )
                {
                    if ( aiCharacterList[ i ].IsDoingAction() == false )
                    {
                        aiCharacterList[ i ].Kill();
                        --remainAINumber;
                    }
                }
            }

            if ( master.IsInLightRange( player.GetPosition() ) == true )
            {
                if ( player.IsDoingAction() == false )
                {
                    player.Kill();
                }
            }

            if ( Input.GetKeyDown( KeyCode.Alpha7 ) )
            {
                master.ChangeToCircleLight();
            }
            else if ( Input.GetKeyDown( KeyCode.Alpha8 ) )
            {
                master.ChangeToSpotLight();
            }
            else if ( Input.GetKeyDown( KeyCode.Alpha9 ) )
            {
                master.ChangeToAllLightMode();
            }

            if ( player.IsAlive() == false )
            {
                gameState = GameState.Fail;
                EventManager.TriggerEvent( EventDictionary.ON_ENTER_FAILED_STATE );
            }
            else if ( remainAINumber == 0 )
            {
                gameState = GameState.Success;
                EventManager.TriggerEvent( EventDictionary.ON_ENTER_SUCCESS_STATE );
            }
        }
        else if ( Input.GetKeyDown( KeyCode.Return ) && gameState == GameState.Start )
        {
            StartTheGame();
        }
        else if ( gameState == GameState.Fail )
        {
            if ( Input.GetKeyDown( KeyCode.Return ) )
            {
                ResetGame();
                gameState = GameState.InGame;
                EventManager.TriggerEvent( EventDictionary.ON_ENTER_GAME_STATE );
            }
        }
        else if ( gameState == GameState.Success )
        {
            if ( Input.GetKeyDown( KeyCode.Return ) )
            {
                ResetGame();
                gameState = GameState.InGame;
                EventManager.TriggerEvent( EventDictionary.ON_ENTER_GAME_STATE );
            }
        }
    }

    void StartTheGame()
    {
        gameState = GameState.InGame;
        EventManager.TriggerEvent( EventDictionary.ON_ENTER_GAME_STATE );
    }

    private void ResetGame()
    {
        for ( int i = 0; i < aiCharacterList.Count; ++i )
        {
            aiCharacterList[ i ].Reset();
        }
        player.Reset();

        remainAINumber = aiCharacterList.Count;
        gameTime.Reset();
    }
}
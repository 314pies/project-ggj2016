using UnityEngine;
using System.Collections.Generic;

public class GameManager
{
	private GameTime gameTime = null;
	private Character player = null;
	private List<Character> aiCharacterList = null;
	private Master master = null;
	private ControllerManager controllerManager = null;
	private NetworkControllerInGame NetworkController = null;
	
	enum GameState { Start, InGame, Success, Fail }
	GameState gameState = GameState.Start;
	private int remainAINumber = 0;
	
	public bool IsServerRunning=false;
    public static bool DisableLocalControl=false;

	public GameManager( GameTime gameTime, ControllerManager controllerManager, Character player, List<Character> aiCharacterList, Master master,NetworkControllerInGame NetworkController)
	{
		this.gameTime = gameTime;
		this.controllerManager = controllerManager;
		this.player = player;
		this.aiCharacterList = aiCharacterList;
		this.master = master;
		this.NetworkController = NetworkController;
		remainAINumber = aiCharacterList.Count;
	}

    public void Tick()
    {
        if ( gameState == GameState.InGame || IsServerRunning )
        {
            gameTime.Tick();
            if(!player.IsAlive())
                DisableLocalControl = true;
            else
                DisableLocalControl = false;


            controllerManager.Tick();

			if ( gameTime.GetTime() > 3f && MpLobby.IsServer)
			{
				for ( int i = 0; i < aiCharacterList.Count; ++i )
				{
					if ( aiCharacterList[ i ].IsAlive() == true && master.IsInLightRange( aiCharacterList[ i ].GetPosition() ) == true )
					{
						if ( aiCharacterList[ i ].IsDoingAction() == false )
						{
							if (!aiCharacterList[i].IsRemotePlayer)
							{
								aiCharacterList[i].Kill();
								int Index = aiCharacterList[i].NetWorkingId;
								NetworkController.KillAnAI(Index);
								//  Debug.Log("Player Left"+remainAINumber);
								--remainAINumber;
							}
							else
							{
								aiCharacterList[i].Kill();
								int Index = aiCharacterList[i].NetWorkingId;
								NetworkController.KillAnPlayer(Index);
								Debug.Log("Player Left" + remainAINumber);
								--remainAINumber;
							}
						}
					}
				}

                if (master.IsInLightRange(player.GetPosition()) == true)
                {
                    if (player.IsDoingAction() == false)
                    {
                        player.Kill();
                        NetworkController.KillAnPlayer(MpLobby.MyIndex);
                    }
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


            /*Count how many player dead, bad performance, good enough*/
            int HowManyDeadRemotePlayer = 0;
            for(int i = 0; i < MpLobby.PlayerCount; i++)
            {
                if (i!=MpLobby.MyIndex && !NetworkController.OtherPlayersCha[i].IsAlive())
                {
                    HowManyDeadRemotePlayer++;
                }
            }
            if(HowManyDeadRemotePlayer == MpLobby.PlayerCount-1 && !NetworkController.LocalPlayerCha.IsAlive())
            {
                gameState = GameState.Fail;
                EventManager.TriggerEvent(EventDictionary.ON_ENTER_FAILED_STATE);
            }
            /*Count how many player dead, bad performance, good enough*/
            /**/
            int HowManyDeadChar = 0;
            for (int i = 0; i < aiCharacterList.Count; i++)
            {
                if (!aiCharacterList[i].IsAlive())
                {
                    HowManyDeadChar++;
                }
            }
            if(HowManyDeadChar == aiCharacterList.Count && player.IsAlive())
            {
                gameState = GameState.Success;
                EventManager.TriggerEvent(EventDictionary.ON_ENTER_SUCCESS_STATE);
            }

            
            /**/
            //if ( player.IsAlive() == false )
            //{
            //	gameState = GameState.Fail;
            //	if (remainAINumber==0)//Only on server?
            //	{
            //		//Round End
            //		IsServerRunning = false;//Every oneDie
            //	}
            //	EventManager.TriggerEvent( EventDictionary.ON_ENTER_FAILED_STATE );
            //}
            //else if ( remainAINumber == 0 )
            //{
            //	gameState = GameState.Success;
            //	IsServerRunning = false;
            //	EventManager.TriggerEvent( EventDictionary.ON_ENTER_SUCCESS_STATE );
            //}
        }

        if (gameState == GameState.Success || gameState == GameState.Fail)
        {
            if (MpLobby.IsServer)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //RemoteRestted();
                    NetworkController.Resetted();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) && gameState == GameState.Start && MpLobby.IsServer)
        {
            // StartTheGame();
            NetworkController.AllStartTheGame();
            IsServerRunning = true;
        }
        //else if ( gameState == GameState.Fail )
        //{
        //	if ( Input.GetKeyDown( KeyCode.Return ) && !IsServerRunning)
        //	{
        //		ResetGame();
        //		gameState = GameState.InGame;
        //		EventManager.TriggerEvent( EventDictionary.ON_ENTER_GAME_STATE );
        //	}
        //}
        //else if ( gameState == GameState.Success && !IsServerRunning)
        //{
        //	if ( Input.GetKeyDown( KeyCode.Return ) )
        //	{
        //		ResetGame();
        //		gameState = GameState.InGame;
        //		EventManager.TriggerEvent( EventDictionary.ON_ENTER_GAME_STATE );
        //	}
        //}
    }

    public void RemoteRestted()
    {
        ResetGame();
        gameState = GameState.InGame;
        EventManager.TriggerEvent(EventDictionary.ON_ENTER_GAME_STATE);


    }

    public void StartTheGame()
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
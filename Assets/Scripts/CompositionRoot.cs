using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameSetting
{
    public AIControlSetting AIControlSetting = null;
    public CharacterSetting characterSetting = null;
    public MasterSetting masterSetting = null;
}

public class CompositionRoot : MonoBehaviour
{
	[SerializeField]
	private GameSetting gameSetting = null;
	[SerializeField]
	private MasterView masterView = null;
	[SerializeField]
	private CharacterView characterPrefab = null;
	[SerializeField]
	private int AINumber = 30;
	
	[SerializeField]
	private UI_SuccessPanel successPanel = null;
	
	private GameManager gameManager = null;
	private ControllerManager controllerManager = null;
	public NetworkControllerInGame NetObj;
    private void Awake()
    {
		/// Master
		MasterView viewMaster = (MasterView)Instantiate( masterView );
		NetObj.Master = viewMaster.gameObject;
		viewMaster.Initialize( gameSetting.masterSetting );
		Master master = new Master( viewMaster );
		MasterAIControl masterControl = new MasterAIControl( master );
		//NetObj.Master.SetActive(false);//tagged
		
		// AI
		List<AIControl> AIControlList = new List<AIControl>();
		List<Character> AICharacter = new List<Character>();
		for ( int i = 0; i < AINumber; ++i )
		{
			CharacterView view = (CharacterView)Instantiate( characterPrefab );
			NetObj.AllAI[i]=view.gameObject;
			NetObj.AllAIChaV[i]=view;
			
			
			view.Initialize( gameSetting.characterSetting );           
			Character character = new Character( view );
			
			character.NetWorkingId=i;
			character.InGameNetManager=NetObj;
			NetObj.AllAICha[i]=character;
			
			AIControl aiControl = new AIControl( gameSetting.AIControlSetting, character, master );
            aiControl.InGameNetManager = NetObj;
            aiControl.NetworkId = i;
            NetObj.AllAICon[i] = aiControl;
            AIControlList.Add( aiControl );
			AICharacter.Add( character );
			//NetObj.AllAI[i].SetActive(false);//tagged
		}
		
		// Player
		CharacterView playerView = (CharacterView)Instantiate( characterPrefab );
		NetObj.LocalPlayer = playerView.gameObject;
		NetObj.LocalPlayerChaV = playerView;
		
		playerView.Initialize( gameSetting.characterSetting );
		
		Character player = new Character (playerView);
		NetObj.LocalPlayerCha = player;
		
		PlayerControl playerControl = new PlayerControl( player, AICharacter );
		
		
		playerControl.NetworkController = NetObj;
		
		//Create Others remote
		for(int i = 0; i < MpLobby.PlayerCount; i++)
		{
			if (i != MpLobby.MyIndex)
			{
				CharacterView view = (CharacterView)Instantiate(characterPrefab);
				print("Assigned");
				NetObj.OtherPlayersRemote[i] = view.gameObject;
				NetObj.OtherPlayersAIChaV[i] = view;
				
				
				view.Initialize(gameSetting.characterSetting);
				Character character = new Character(view);
				
				character.NetWorkingId = i;
				character.IsRemotePlayer = true;
				character.InGameNetManager = NetObj;
				NetObj.OtherPlayersCha[i] = character;
				
				//AIControl aiControl = new AIControl(gameSetting.AIControlSetting, character, master);
				//AIControlList.Add(aiControl);
				AICharacter.Add(character);
			}
		}

        GameTime gameTime = new GameTime();
        controllerManager = new ControllerManager(playerControl, AIControlList, masterControl);
		gameManager = new GameManager( gameTime, controllerManager, player, AICharacter, master,NetObj );
		NetObj.gameManager = gameManager;
        successPanel.SetGameTime( gameTime );
    }

    private void Update()
    {
        //controllerManager.Tick();
        gameManager.Tick();
    }
}
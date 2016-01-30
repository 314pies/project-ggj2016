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

    private GameManager gameManager = null;
    private ControllerManager controllerManager = null;

    private void Awake()
    {
        // Master
        MasterView viewMaster = Instantiate( masterView );
        viewMaster.Initialize( gameSetting.masterSetting );
        Master master = new Master( viewMaster );
        MasterAIControl masterControl = new MasterAIControl( master );

        // AI
        List<AIControl> AIControlList = new List<AIControl>();
        List<Character> AICharacter = new List<Character>();
        for ( int i = 0; i < AINumber; ++i )
        {
            CharacterView view = Instantiate( characterPrefab );
            view.Initialize( gameSetting.characterSetting );
            Character character = new Character( view );
            AIControl aiControl = new AIControl( gameSetting.AIControlSetting, character, master );
            AIControlList.Add( aiControl );
            AICharacter.Add( character );
        }

        // Player
        CharacterView playerView = Instantiate( characterPrefab );
        playerView.Initialize( gameSetting.characterSetting );
        Character player = new Character( playerView );
        PlayerControl playerControl = new PlayerControl( player, AICharacter );

        controllerManager = new ControllerManager(playerControl, AIControlList, masterControl);
        gameManager = new GameManager(controllerManager, player, AICharacter, master );
        
    }

    private void Update()
    {
        //controllerManager.Tick();
        gameManager.Tick();
    }
}
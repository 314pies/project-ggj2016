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
    private CharacterView characterView = null;
    [SerializeField]
    private int AINumber = 30;

    private GameManager gameManager = null;
    private ControllerManager controllerManager = null;

    private void Awake()
    {
        // Player
        CharacterView playerView = Instantiate( characterView );
        playerView.Initialize( gameSetting.characterSetting );
        Character player = new Character( playerView );
        PlayerControl playerControl = new PlayerControl( player );

        // AI
        List<AIControl> AIControlList = new List<AIControl>();
        List<Character> AICharacter = new List<Character>();
        for ( int i = 0; i < AINumber; ++i )
        {
            CharacterView view = Instantiate( characterView );
            view.Initialize( gameSetting.characterSetting );
            Character character = new Character( view );
            AIControl aiControl = new AIControl( gameSetting.AIControlSetting, character );
            AIControlList.Add( aiControl );
            AICharacter.Add( character );
        }

        // Master
        MasterView viewMaster = Instantiate( masterView );
        viewMaster.Initialize( gameSetting.masterSetting );
        Master master = new Master( viewMaster );
        MasterAIControl masterControl = new MasterAIControl( master );

        gameManager = new GameManager( player, AICharacter, master );
        controllerManager = new ControllerManager( playerControl, AIControlList, masterControl );
    }

    private void Update()
    {
        gameManager.Tick();
        controllerManager.Tick();
    }
}
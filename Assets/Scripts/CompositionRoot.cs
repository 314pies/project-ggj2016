using UnityEngine;
using System.Collections.Generic;

public class CompositionRoot : MonoBehaviour {
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
		CharacterView playerView = Instantiate (characterView);
		Character player = new Character (playerView);
		PlayerControl playerControl = new PlayerControl (player);

		// AI
		List<AIControl> AIControlList = new List<AIControl> ();
		List<Character> AICharacter = new List<Character> ();
		for (int i = 0; i < AINumber; ++i) {
			CharacterView view = Instantiate (characterView);
			Character character = new Character (view);
			AIControl aiControl = new AIControl(character);
			AIControlList.Add(aiControl);
			AICharacter.Add (character);
		}

		// Master
		MasterView viewMaster = Instantiate (masterView);
		MasterCharacter master = new MasterCharacter (viewMaster);
		MasterAIControl masterControl = new MasterAIControl (master);

		gameManager = new GameManager (player, AICharacter,master);
		controllerManager =new ControllerManager (playerControl, AIControlList, masterControl);
	}

	private void Update()
	{
		gameManager.Tick ();
		controllerManager.Tick ();
	}
}
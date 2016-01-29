using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerManager
{
	private List<AIControl> AICharacterList = null;
	private PlayerControl playerControl = null;
	private MasterAIControl masterControl = null;
	
	public ControllerManager(PlayerControl playerControl, List<AIControl> AICharacterList, MasterAIControl masterControl)
	{
		this.AICharacterList = AICharacterList;
		this.playerControl = playerControl;
		this.masterControl = masterControl;
	}
	
	public void Tick()
	{
		//update characters
		
		playerControl.Tick ();
		
		for (int i = 0; i < AICharacterList.Count; ++i) {
				AICharacterList[i].Tick();
		}
		
		masterControl.Tick ();
	}

}
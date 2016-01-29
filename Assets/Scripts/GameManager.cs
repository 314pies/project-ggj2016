using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager
{
	private Character player = null;
	private List<Character> aiCharacterList = null;
	private MasterCharacter master = null;

	public GameManager(Character player, List<Character> aiCharacterList, MasterCharacter master)
	{
		this.player = player;
		this.aiCharacterList = aiCharacterList;
		this.master = master;
	}

	public void Tick()
	{
		for (int i = 0; i < aiCharacterList.Count; ++i) {
			if (master.IsInCircleRange(aiCharacterList[i].GetPosition()) == true)
			{
				aiCharacterList[i].DoAction(true);
			}
			else
			{
				aiCharacterList[i].DoAction(false);
			}
		}
		//Vector2 masterPos = master.GetPosition ();
	}
}
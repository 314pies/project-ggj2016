using UnityEngine;
using System.Collections;

public class MasterAIControl
{
	private MasterCharacter character = null;

	public MasterAIControl(MasterCharacter character)
	{
		this.character = character;
	}
	
	public void Tick()
	{
		character.CircleMove ();
	}
}

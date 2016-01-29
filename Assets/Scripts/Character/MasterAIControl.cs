using UnityEngine;
using System.Collections;

public class MasterAIControl
{
	private Master character = null;

	public MasterAIControl(Master character)
	{
		this.character = character;
	}
	
	public void Tick()
	{
		character.CircleMove ();
	}
}

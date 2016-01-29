using UnityEngine;
using System.Collections;

public class PlayerControl
{
	private Character character = null;

	public PlayerControl(Character character)
	{
		this.character = character;
	}

	public void Tick()
	{
		character.Move (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		character.DoAction (Input.GetKey(KeyCode.Space));
	}
}

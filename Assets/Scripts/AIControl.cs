using UnityEngine;
using System.Collections;

public class AIControl
{
	private Character character = null;
	private float CONTROL_CHANGING_TIME = 0.1f;
	private float timer = 0f;
	private Vector2 controlDirection = Vector2.zero;

	public AIControl(Character character)
	{
		this.character = character;
	}

	public void Tick()
	{
		timer += Time.deltaTime;
		if (timer >= CONTROL_CHANGING_TIME) {
			controlDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
			timer = 0f;
		}

		character.Move (controlDirection.x, controlDirection.y);
	}
}
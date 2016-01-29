using UnityEngine;
using System.Collections;

public class MasterCharacter
{
	private MasterView masterView;


	public MasterCharacter(MasterView masterView)
	{
		this.masterView = masterView;
	}

	private Vector2 GetPosition()
	{
		return masterView.transform.localPosition;
	}

	private float GetCircleRange()
	{
		return masterView.GetCircleRange ()  * 0.08f;
	}

	public bool IsInCircleRange(Vector2 otherPos)
	{
		if (Vector2.Distance (GetPosition (), otherPos) <= GetCircleRange ())
			return true;
		else
			return false;
	}
	
	public void CircleMove()
	{
		masterView.CircleMove ();
	}


	public void RandomMove()
	{

	}
}

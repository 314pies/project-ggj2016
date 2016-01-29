using UnityEngine;
using System.Collections;

public class CharacterView : MonoBehaviour {
	[SerializeField]
	private float speed = 0.5f;
	[SerializeField]
	private Vector2 posLimit = Vector2.zero;

	private SpriteRenderer m_spriteRenderer;
	private Transform m_transform;


	void Awake()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer> ();
		m_transform = transform;
	}


	public void Translate( float x, float y)
	{
		Vector3 speedDirection = new Vector3 (x, y, 0);
		speedDirection = speedDirection.normalized;
		speedDirection *= speed;

		m_transform.Translate (speedDirection * Time.deltaTime);

		m_transform.localPosition = new Vector2 (Mathf.Clamp (transform.localPosition.x, -posLimit.x, posLimit.x), Mathf.Clamp (transform.localPosition.y, -posLimit.y, posLimit.y));
	}


	public void DoAction(bool active)
	{
		m_spriteRenderer.color = active ? Color.black : Color.white;
	}


}
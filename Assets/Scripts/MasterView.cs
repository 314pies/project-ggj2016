using UnityEngine;
using System.Collections;

public class MasterView : MonoBehaviour {
	[SerializeField]
	private float speed = 0.5f;
	[SerializeField]
	private Vector2 posLimit = Vector2.zero;
	[SerializeField]
	private float circleRange = 10;
	[SerializeField]
	private Transform circleLight = null;
	
	
	private SpriteRenderer m_spriteRenderer;
	private Transform m_transform;
	private float m_curAngle = 0;
	
	
	void Awake()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer> ();
		m_transform = transform;
	}

	private void Update()
	{
		circleLight.localScale = new Vector3 (circleRange, circleRange, 1);
	}
	
	
	public void CircleMove()
	{
		m_curAngle += Time.deltaTime * speed;
		
		Vector3 newPosition = Quaternion.Euler (0, 0, m_curAngle) * Vector3.right;
		
		m_transform.localPosition = newPosition;
		m_transform.localEulerAngles = Vector3.forward * m_curAngle;
	}
	
	public float GetCircleRange()
	{
		return circleRange;
	}
	public void Translate( float x, float y)
	{
		Vector3 speedDirection = new Vector3 (x, y, 0);
		speedDirection = speedDirection.normalized;
		speedDirection *= speed;
		
		m_transform.Translate (speedDirection * Time.deltaTime);
		
		m_transform.localPosition = new Vector2 (Mathf.Clamp (transform.localPosition.x, -posLimit.x, posLimit.x), Mathf.Clamp (transform.localPosition.y, -posLimit.y, posLimit.y));
	}
	
	
}
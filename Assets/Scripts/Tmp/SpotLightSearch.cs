using UnityEngine;
using System.Collections;

public class SpotLightSearch : MonoBehaviour
{
	public GameObject target;
	public float searchAngle;
	public float searchDistance;
	
	private Transform _myTransform;
	private float originArea;

	void Awake()
	{	
		_myTransform = transform;
	}
	
	void Update()
	{
		if(IsInCircleRange(target.transform.position))
			Debug.Log ("Find Target");

	}


	public bool IsInCircleRange(Vector3 other)
	{
		Vector3 forward = _myTransform.forward * searchDistance;
		Vector3 right = Quaternion.AngleAxis( searchAngle, _myTransform.up ) * forward;
		Vector3 left = Quaternion.AngleAxis( -searchAngle, _myTransform.up ) * forward;
		
		Vector3 pointF = _myTransform.position + forward;
		Vector3 pointR = _myTransform.position + right;
		Vector3 pointL = _myTransform.position + left;

		bool inLeftTriangle = Calculate( other, _myTransform.position, pointL, pointF );
		bool inRightTriangle = Calculate( other, _myTransform.position, pointR, pointF );
		
		if (inLeftTriangle || inRightTriangle)
			return true;

		return false;
	}

	
	public bool Calculate( Vector3 point, Vector3 vertex1, Vector3 vertex2, Vector3 vertex3 ) {
		
		Vector2 vectorAB = new Vector2( vertex2.x - vertex1.x, vertex2.z - vertex1.z );
		Vector2 vectorAC = new Vector2( vertex3.x - vertex1.x, vertex3.z - vertex1.z );
		
		originArea = Mathf.Abs( ( vectorAB.x * vectorAC.y - vectorAB.y * vectorAC.x ) ) / 2;
		
		Vector2 vectorPA = new Vector2( vertex1.x - point.x, vertex1.z - point.z );
		Vector2 vectorPB = new Vector2( vertex2.x - point.x, vertex2.z - point.z );
		Vector2 vectorPC = new Vector2( vertex3.x - point.x, vertex3.z - point.z );
		
		float areaPAB = Mathf.Abs( ( vectorPA.x * vectorPB.y - vectorPA.y * vectorPB.x ) ) / 2;
		float areaPAC = Mathf.Abs( ( vectorPA.x * vectorPC.y - vectorPA.y * vectorPC.x ) ) / 2;
		float areaPBC = Mathf.Abs( ( vectorPB.x * vectorPC.y - vectorPB.y * vectorPC.x ) ) / 2;
		
		if( areaPAB + areaPAC + areaPBC > originArea )
			return false;
		else
			return true;
	}


	void OnDrawGizmos()
	{
		Vector3 forward = transform.forward * searchDistance;
		Vector3 right = Quaternion.AngleAxis( searchAngle, transform.up ) * forward;
		Vector3 left = Quaternion.AngleAxis( -searchAngle, transform.up ) * forward;
		
		Vector3 pointF = transform.position + forward;
		Vector3 pointR = transform.position + right;
		Vector3 pointL = transform.position + left;

		Debug.DrawLine (transform.position, pointF);
		Debug.DrawLine (transform.position, pointR);
		Debug.DrawLine (transform.position, pointL);
		Debug.DrawLine (pointR, pointF);
		Debug.DrawLine (pointL, pointF);
	}
	
}

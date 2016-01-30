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
		if( IsInSpotLightRange( target.transform.position))
			Debug.Log ("Find Target");

	}


	public bool IsInSpotLightRange(Vector3 other)
	{
		Vector3 up = _myTransform.up * searchDistance;
		Vector3 right = Quaternion.AngleAxis( searchAngle, -_myTransform.forward ) * up;
		Vector3 left = Quaternion.AngleAxis( -searchAngle, -_myTransform.forward ) * up;
		
		Vector3 pointF = _myTransform.position + up;
		Vector3 pointR = _myTransform.position + right;
		Vector3 pointL = _myTransform.position + left;

        //bool inLeftTriangle = Calculate( other, _myTransform.position, pointL, pointF );
        //bool inRightTriangle = Calculate( other, _myTransform.position, pointR, pointF );

        //if (inLeftTriangle || inRightTriangle)
        //	return true;

        if ( IsInTriangle( _myTransform.position, pointL, pointF, other )
            || IsInTriangle( _myTransform.position, pointR, pointF, other ) )
            return true;
        else
            return false;
	}

	
	//public bool Calculate( Vector3 point, Vector3 vertex1, Vector3 vertex2, Vector3 vertex3 ) {
		
	//	Vector2 vectorAB = new Vector2( vertex2.x - vertex1.x, vertex2.z - vertex1.z );
	//	Vector2 vectorAC = new Vector2( vertex3.x - vertex1.x, vertex3.z - vertex1.z );
		
	//	originArea = Mathf.Abs( ( vectorAB.x * vectorAC.y - vectorAB.y * vectorAC.x ) ) / 2;
		
	//	Vector2 vectorPA = new Vector2( vertex1.x - point.x, vertex1.z - point.z );
	//	Vector2 vectorPB = new Vector2( vertex2.x - point.x, vertex2.z - point.z );
	//	Vector2 vectorPC = new Vector2( vertex3.x - point.x, vertex3.z - point.z );
		
	//	float areaPAB = Mathf.Abs( ( vectorPA.x * vectorPB.y - vectorPA.y * vectorPB.x ) ) / 2;
	//	float areaPAC = Mathf.Abs( ( vectorPA.x * vectorPC.y - vectorPA.y * vectorPC.x ) ) / 2;
	//	float areaPBC = Mathf.Abs( ( vectorPB.x * vectorPC.y - vectorPB.y * vectorPC.x ) ) / 2;
		
	//	if( areaPAB + areaPAC + areaPBC > originArea )
	//		return false;
	//	else
	//		return true;
	//}


	void OnDrawGizmos()
	{
		Vector3 up = transform.up * searchDistance;
		Vector3 right = Quaternion.AngleAxis( searchAngle, -transform.forward ) * up;
		Vector3 left = Quaternion.AngleAxis( -searchAngle, -transform.forward ) * up;
		
		Vector3 pointF = transform.position + up;
		Vector3 pointR = transform.position + right;
		Vector3 pointL = transform.position + left;

		Debug.DrawLine (transform.position, pointF);
		Debug.DrawLine (transform.position, pointR);
		Debug.DrawLine (transform.position, pointL);
		Debug.DrawLine (pointR, pointF);
		Debug.DrawLine (pointL, pointF);
	}

    public bool IsInTriangle( Vector3 A, Vector3 B, Vector3 C, Vector3 P )
    {
        return AreAtSameSide( A, B, C, P ) &&
            AreAtSameSide( B, C, A, P ) &&
            AreAtSameSide( C, A, B, P );
    }

    private bool AreAtSameSide( Vector3 A, Vector3 B, Vector3 C, Vector3 P )
    {
        Vector3 AB = B - A;
        Vector3 AC = C - A;
        Vector3 AP = P - A;

        Vector3 v1 = Vector3.Cross( AB,AC );
        Vector3 v2 = Vector3.Cross( AB,AP );

        return Vector3.Dot( v1, v2 ) >= 0;
    }
}

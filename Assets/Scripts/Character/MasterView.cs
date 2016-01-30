using UnityEngine;

public class MasterView : MonoBehaviour
{
    [SerializeField]
    private Transform circleLight = null;
    [SerializeField]
    private Transform spotlight = null;

    public float searchAngle = 15f;
    public float searchDistance = 2f;

    private MasterSetting masterSetting = null;

    private SpriteRenderer m_spriteRenderer;
    private Transform m_transform;
    private float m_curAngle = 0;

    public void Initialize( MasterSetting masterSetting )
    {
        this.masterSetting = masterSetting;
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_transform = transform;
    }

    private void Update()
    {
        circleLight.localScale = new Vector3( masterSetting.circleRangeInTextureSize, masterSetting.circleRangeInTextureSize, 1 );
    }

    public void CircleMove()
    {
        m_curAngle += Time.deltaTime * masterSetting.rotateSpeed;

        Vector3 newPosition = Quaternion.Euler( 0, 0, m_curAngle ) * Vector3.right;

        m_transform.localPosition = newPosition;
        m_transform.localEulerAngles = Vector3.forward * m_curAngle;
    }

    public float GetCircleRange()
    {
        return masterSetting.circleRangeInTextureSize * masterSetting.rangeTextureSizeToUnitValue;
    }

    public void Translate( float x, float y )
    {
        Vector3 speedDirection = new Vector3( x, y, 0 );
        speedDirection = speedDirection.normalized;
        speedDirection *= masterSetting.moveSpeed;

        m_transform.Translate( speedDirection * Time.deltaTime );

        m_transform.localPosition = new Vector2(
            Mathf.Clamp( transform.localPosition.x, -masterSetting.border.x, masterSetting.border.x ),
            Mathf.Clamp( transform.localPosition.y, -masterSetting.border.y, masterSetting.border.y ) );
    }


    public bool IsInSpotLightRange( Vector3 other )
    {
        Vector3 up = m_transform.up * masterSetting.searchDistance;
        Vector3 right = Quaternion.AngleAxis( masterSetting.searchAngle, -m_transform.forward ) * up;
        Vector3 left = Quaternion.AngleAxis( -masterSetting.searchAngle, -m_transform.forward ) * up;

        Vector3 pointF = m_transform.position + up;
        Vector3 pointR = m_transform.position + right;
        Vector3 pointL = m_transform.position + left;

        if ( IsInTriangle( m_transform.position, pointL, pointF, other )
            || IsInTriangle( m_transform.position, pointR, pointF, other ) )
            return true;
        else
            return false;
    }

    private bool IsInTriangle( Vector3 A, Vector3 B, Vector3 C, Vector3 P )
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

        Vector3 v1 = Vector3.Cross( AB, AC );
        Vector3 v2 = Vector3.Cross( AB, AP );

        return Vector3.Dot( v1, v2 ) >= 0;
    }

    private void OnDrawGizmos()
    {
        Vector3 up = transform.up * searchDistance;
        Vector3 right = Quaternion.AngleAxis( searchAngle, -transform.forward ) * up;
        Vector3 left = Quaternion.AngleAxis( -searchAngle, -transform.forward ) * up;

        Vector3 pointF = transform.position + up;
        Vector3 pointR = transform.position + right;
        Vector3 pointL = transform.position + left;

        Debug.DrawLine( transform.position, pointF );
        Debug.DrawLine( transform.position, pointR );
        Debug.DrawLine( transform.position, pointL );
        Debug.DrawLine( pointR, pointF );
        Debug.DrawLine( pointL, pointF );
    }

    public void ChangeToCircleLight()
    {
        CloseAllLight();
        circleLight.gameObject.SetActive( true );
    }

    public void ChangeToSpotLight()
    {
        CloseAllLight();
        spotlight.gameObject.SetActive( true );
    }

    public void ChangeToAllLightMode()
    {

    }

    private void CloseAllLight()
    {
        circleLight.gameObject.SetActive( false );
        spotlight.gameObject.SetActive( false );
    }
}
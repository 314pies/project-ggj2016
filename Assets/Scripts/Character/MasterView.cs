using UnityEngine;

public class MasterView : MonoBehaviour
{
    [SerializeField]
    private float circleRange = 10;
    [SerializeField]
    private Transform circleLight = null;

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
}
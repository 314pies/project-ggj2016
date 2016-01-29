using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private CharacterSetting characterSetting = null;
    private SpriteRenderer m_spriteRenderer;
    private Transform m_transform;

    public void Initialize( CharacterSetting characterSetting )
    {
        this.characterSetting = characterSetting;
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_transform = transform;
    }

    public void Translate( float x, float y )
    {
        Vector3 speedDirection = new Vector3( x, y, 0 );
        speedDirection = speedDirection.normalized;
        speedDirection *= characterSetting.moveSpeed;

        m_transform.Translate( speedDirection * Time.deltaTime );

        m_transform.localPosition = new Vector2(
            Mathf.Clamp( transform.localPosition.x, -characterSetting.border.x, characterSetting.border.x ),
            Mathf.Clamp( transform.localPosition.y, -characterSetting.border.y, characterSetting.border.y ) );
    }

    public void DoAction( bool active )
    {
        m_spriteRenderer.color = active ? Color.black : Color.white;
    }
}
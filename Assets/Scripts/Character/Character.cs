using UnityEngine;

[System.Serializable]
public class CharacterSetting
{
    public float moveSpeed = 2f;
    public Vector2 border = new Vector2( 3f, 2.5f );
}

public class Character
{
    public Vector2 Position = Vector2.zero;
    private CharacterView characterView = null;
    private bool isDoingAction = false;
    private bool isAlive = true;

    public Character( CharacterView characterView )
    {
        this.characterView = characterView;
    }

    public void Reset()
    {
        isDoingAction = false;
        isAlive = true;
        characterView.Reset();
    }

    public Vector2 GetPosition()
    {
        return characterView.transform.localPosition;
    }

    public void Move( float x, float y )
    {
        if ( isAlive )
        {
            if ( isDoingAction == false )
                characterView.Translate( x, y );
        }
    }

    public void DoAction( bool active )
    {
        if ( isAlive )
        {
            isDoingAction = active;
            characterView.DoAction( active );
        }
    }

    public bool IsDoingAction()
    {
        return isDoingAction;
    }

    public void BePush()
    {
        if ( isAlive )
        {

        }
    }

    public void Kill()
    {
        isAlive = false;
        characterView.PlayDieAnim();
    }
}
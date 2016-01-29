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

    public Character( CharacterView characterView )
    {
        this.characterView = characterView;
    }

    public Vector2 GetPosition()
    {
        return characterView.transform.localPosition;
    }

    public void Move( float x, float y )
    {
        if ( isDoingAction == false )
            characterView.Translate( x, y );
    }

    public void DoAction( bool active )
    {
        isDoingAction = active;
        characterView.DoAction( active );
    }

    public void BePush()
    {

    }

    public void Kill()
    {

    }
}
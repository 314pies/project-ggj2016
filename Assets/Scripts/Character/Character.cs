using UnityEngine;

[System.Serializable]
public class CharacterSetting
{
    public float moveSpeed = 2f;
    public Vector2 borderMax = new Vector2( 9f, 2f );
    public Vector2 borderMin = new Vector2( -9f, -4.3f );
    public float pushRange = 0.5f;
    public float pushTime = 1f;
    public float fallTime = 2f;
    public float fallSpeed = 1f;
}

public class Character
{
	/*Need for Network*/
	public NetworkControllerInGame InGameNetManager;
	public int NetWorkingId=-1;
	public bool IsRemotePlayer=false;//Not AI, is Player
	/*Need for Network*/

    public Vector2 Position = Vector2.zero;
    private CharacterView characterView = null;
    private bool isDoingAction = false;
    private bool isAlive = true;
    private bool isPushing = false;
    private bool isFalling = false;
    private float pushTimer = 0f;
    private float fallTimer = 0f;
    private Vector2 forceDir = Vector2.zero;

    public Character( CharacterView characterView )
    {
        this.characterView = characterView;
    }

    public void Tick()
    {
        if ( isPushing )
        {
            pushTimer += Time.deltaTime;
            if ( pushTimer >= characterView.GetPushTime() )
            {
                isPushing = false;
                characterView.ResetAnim();
            }
        }

        if ( isFalling )
        {
            characterView.FallingTranslate( forceDir.x, forceDir.y );
            fallTimer += Time.deltaTime;
            if ( fallTimer >= characterView.GetFallTime() )
            {
                isFalling = false;
                characterView.ResetAnim();
            }
        }
    }

    public void Reset()
    {
        isDoingAction = false;
        isPushing = false;
        isFalling = false;
        isAlive = true;
        characterView.ResetPosition();
        characterView.ResetAnim();
    }

    public Vector2 GetPosition()
    {
        return characterView.transform.localPosition;
    }

    public void Move( float x, float y )
    {
        if ( isAlive )
        {
            if ( isDoingAction == false && isPushing == false && isFalling == false )
                characterView.Translate( x, y );
            else
                characterView.Translate( 0f, 0f );
        }
    }

    public void DoAction( bool active )
    {
        if ( isAlive && isPushing == false && isFalling == false && active != isDoingAction )
        {
            isDoingAction = active;
            characterView.DoAction( active );
        }
    }

    public bool IsDoingAction()
    {
        return isDoingAction;
    }

    public bool IsInPushRange( Vector2 otherPos )
    {
        if ( Vector2.Distance( GetPosition(), otherPos ) <= characterView.GetPushRange() )
            return true;
        else
            return false;
    }

    public void Push()
    {
        if ( isAlive == true && isPushing == false && isFalling == false )
        {
            pushTimer = 0f;
            isPushing = true;
            characterView.PlayPushAnim();
        }
    }

    public bool IsAvailableToPushOthers()
    {
        return isAlive == true && isPushing == false && isFalling == false;
    }

    public void Fall( Vector2 forceDir )
    {
        if ( isAlive == true && isFalling == false )
        {
            this.forceDir = forceDir;
            fallTimer = 0f;
            isDoingAction = false;
            isFalling = true;
            characterView.PlayFallAnim();
        }
    }

    public void Kill()
    {
        isAlive = false;
        isDoingAction = false;
        isPushing = false;
        isFalling = false;
        characterView.PlayDieAnim();
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
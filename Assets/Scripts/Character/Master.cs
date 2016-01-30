using UnityEngine;

[System.Serializable]
public class MasterSetting : CharacterSetting
{
    public float rotateSpeed = 100f;
    public float circleRangeInTextureSize = 10f;
    public float rangeTextureSizeToUnitValue = 0.08f;

    public float searchAngle = 15f;
    public float searchDistance = 2f;
}

public class Master
{
    enum LightState { CIRCLE, SPOTLIGHT, ALL }
    private LightState lightState = LightState.CIRCLE;
    private MasterView masterView;

    public Master( MasterView masterView )
    {
        this.masterView = masterView;
    }

    private Vector2 GetPosition()
    {
        return masterView.transform.localPosition;
    }

    public bool IsInLightRange( Vector2 otherPos )
    {
        if ( lightState == LightState.CIRCLE )
            return IsInCircleRange( otherPos );
        else if ( lightState == LightState.SPOTLIGHT )
            return IsInSpotLightRange( otherPos );
        else
            return true;
    }

    private bool IsInCircleRange( Vector2 otherPos )
    {
        if ( Vector2.Distance( GetPosition(), otherPos ) <= masterView.GetCircleRange() )
            return true;
        else
            return false;
    }

    private bool IsInSpotLightRange( Vector2 otherPos )
    {
        return masterView.IsInSpotLightRange( otherPos );
    }

    public void CircleMove()
    {
        masterView.CircleMove();
    }

    public void Move( float x, float y )
    {
        masterView.Translate( x, y );
    }

    public void RandomMove()
    {

    }

    public void ChangeToCircleLight()
    {
        lightState = LightState.CIRCLE;
        masterView.ChangeToCircleLight();
    }

    public void ChangeToSpotLight()
    {
        lightState = LightState.SPOTLIGHT;
        masterView.ChangeToSpotLight();
    }

    public void ChangeToAllLightMode()
    {
        //lightState = LightState.ALL;
        //masterView.ChangeToAllLightMode();
    }
}
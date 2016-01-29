using UnityEngine;

[System.Serializable]
public class MasterSetting : CharacterSetting
{
    public float rotateSpeed = 100f;
    public float circleRangeInTextureSize = 10f;
    public float rangeTextureSizeToUnitValue = 0.08f;
}

public class Master
{
    private MasterView masterView;

    public Master( MasterView masterView )
    {
        this.masterView = masterView;
    }

    private Vector2 GetPosition()
    {
        return masterView.transform.localPosition;
    }

    private float GetCircleRange()
    {
        return masterView.GetCircleRange();
    }

    public bool IsInCircleRange( Vector2 otherPos )
    {
        if ( Vector2.Distance( GetPosition(), otherPos ) <= GetCircleRange() )
            return true;
        else
            return false;
    }

    public void CircleMove()
    {
        masterView.CircleMove();
    }

    public void RandomMove()
    {

    }
}
using System;
using System.Diagnostics;
using Godot;

public partial class FlyByWireCommandInterpret : FlightCommandInterpret
{
    [Export]
    private float rotationMax = 15;
    
    [Export]
    private float rotationMinThreshold = 0.000001f;

    
    [Export]
    private float velocityMax = 15;

    [Export]
    private float velocityMinThreshold = 0.000001f;

    public override float IdleBaseRotation(Vector2 currentVelocity, float currentRotation)
    {
        if (currentRotation == 0)
        {
            return 0;
        }
        float absoluteRotation = Math.Abs(currentRotation);
        float speedValue = Lerp(0, rotationMax, absoluteRotation);
        float direction = currentRotation < 0 ? -1f : 1f;
        float returnValue = speedValue * -direction;
        if (Math.Abs(returnValue) < rotationMinThreshold)
        {
            returnValue = returnValue > 0 ? rotationMinThreshold : -rotationMinThreshold;
        }
        return returnValue;
    }

    public override Vector2 IdleBaseVelocity(Vector2 currentVelocity, float currentRotation)
    {
        return Vector2.Zero;
        Vector2 shipBasedMovement = currentVelocity.Rotated(currentRotation);

        if (currentVelocity == Vector2.Zero)
        {
            return currentVelocity;
        }
        float returnXValue = Lerp(0, velocityMax, shipBasedMovement.X * -1f);
        float returnYValue = Lerp(0, velocityMax, shipBasedMovement.Y * -1f);
        if (Math.Abs(returnXValue) < rotationMinThreshold)
        {
            returnXValue = returnXValue > 0 ? velocityMax : -velocityMax;
        }
        if (Math.Abs(returnYValue) < rotationMinThreshold)
        {
            returnYValue = returnYValue > 0 ? velocityMax : -velocityMax;
        }
        return new Vector2(returnXValue, returnYValue);
    }

    float Lerp(float firstFloat, float secondFloat, float by)
    {
        return firstFloat * (1 - by) + secondFloat * by;
    }
}
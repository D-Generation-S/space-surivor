using System;
using Godot;

public partial class FlyByWireCommandInterpret : FlightCommandInterpret
{
    [Export(PropertyHint.Range,"0.01745, 0.01745")]
    private float rotationMax = 0.01745f;
    
    [Export]
    private float rotationMinThreshold = 0.000001f;
    
    [Export]
    private float velocityMax = 15;

    [Export]
    private float rotationDifference = 0.001f;

    private float targetShipRotation;

    public override void SetupInterpret(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        targetShipRotation = currentShipRotation;
    }

    public override float IdleBaseRotation(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        var diff = targetShipRotation - currentShipRotation;
        var absoluteDiff = Math.Abs(diff);
        if ((diff < rotationDifference && diff > -rotationDifference) || absoluteDiff < 0.1f)
        {
            return 0;
        }
        
        var clamped = Math.Clamp(absoluteDiff, 0, 1);
        float direction = diff < 0 ? -1f : 1f;
        if (diff > 1)
        {
            return direction;
        }
        float targetTurnRate = Lerp(0, rotationMax, clamped) * direction;
        float turnDifference = targetTurnRate - currentRotationVelocity;

        float percentage = Math.Clamp(Math.Abs(turnDifference / targetTurnRate), 0, 1);
        direction = turnDifference > 0 ? 1f : -1f;
        return Lerp(0, 1, percentage) * direction;
    }

    public override Vector2 IdleBaseVelocity(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        Vector2 direction = currentVelocity.Normalized().Rotated(currentShipRotation);
        Vector2 shipBasedMovement = currentVelocity.Rotated(currentRotationVelocity);

        if (currentVelocity == Vector2.Zero)
        {
            return currentVelocity;
        }
        return Vector2.Zero;
        /**
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
        */
    }

    public override float InterpretRotation(float commandedRotation, Vector2 currentVelocity, float rotationVelocity, float currentShipRotation)
    {
        if(commandedRotation == 0)
        {
            return 0;
        }
        targetShipRotation = currentShipRotation;
        return base.InterpretRotation(commandedRotation, currentVelocity, rotationVelocity, currentShipRotation);
    }

    float Lerp(float firstFloat, float secondFloat, float by)
    {
        return firstFloat * (1 - by) + secondFloat * by;
    }
}
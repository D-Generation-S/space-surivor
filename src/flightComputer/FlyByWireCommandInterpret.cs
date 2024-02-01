using System;
using Godot;

/// <summary>
/// Class used to provide a fly by wire interpret
/// This will try to keep the ship on course and prevent drifting if no input is given.
/// </summary>
public partial class FlyByWireCommandInterpret : FlightCommandInterpret
{
    /// <summary>
    /// The maximal rotation allowed to correct the rotation in case of no input
    /// </summary>
    [Export(PropertyHint.Range,"0.01745, 0.01745")]
    private float rotationMax = 0.01745f;

    /// <summary>
    /// The allowed difference between target and real rotation, this prevents some osculation
    /// </summary>
    [Export]
    private float rotationDifference = 0.001f;

    /// <summary>
    /// The 
    /// </summary>
    private float targetShipRotation;

    /// <summary>
    /// Constant for a half rotation in degree, this is identically to pi or 180 degree
    /// </summary>
    private readonly float halfRotation = (float)Math.PI;

    /// <inheritdoc/>
    public override void SetupInterpret(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        targetShipRotation = currentShipRotation;
    }

    /// <inheritdoc/>
    public override float IdleBaseRotation(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        var diff = targetShipRotation - currentShipRotation;
        var absoluteDiff = Math.Abs(diff);
        if (absoluteDiff > halfRotation)
        {
            absoluteDiff = halfRotation - diff * -1;
            diff *= -1;
        }
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

    /// <inheritdoc/>
    public override Vector2 IdleBaseVelocity(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        var normalized = currentVelocity.Normalized();
        float yComponent = currentVelocity.Y < normalized.Y ? currentVelocity.Y : normalized.Y;
        float xComponent = currentVelocity.X < normalized.X ? currentVelocity.X : normalized.X;
        return new Vector2(xComponent, yComponent) * -1;
    }

    /// <inheritdoc/>
    public override float InterpretRotation(float commandedRotation, Vector2 currentVelocity, float rotationVelocity, float currentShipRotation)
    {
        if(commandedRotation == 0)
        {
            return 0;
        }
        targetShipRotation = currentShipRotation;
        
        return base.InterpretRotation(commandedRotation, currentVelocity, rotationVelocity, currentShipRotation);
    }

    public override Vector2 InterpretBaseVelocity(Vector2 commandedVelocity, Vector2 currentVelocity, float rotationVelocity, float currentShipRotation)
    {
        if (commandedVelocity.X == 0)
        {
            float newX = currentVelocity.X * -1;
            commandedVelocity = new Vector2(newX, commandedVelocity.Y);
        }
        return base.InterpretBaseVelocity(commandedVelocity, currentVelocity, rotationVelocity, currentShipRotation);
    }

    /// <summary>
    /// Method to lerp between two values
    /// Note: This method should properly be moved to some math extension
    /// </summary>
    /// <param name="firstFloat">The first float value to lerp between</param>
    /// <param name="secondFloat">The second float value to lerp between</param>
    /// <param name="by">The value used for lerping, should be between 0 and 1</param>
    /// <returns>The lerped value</returns>
    float Lerp(float firstFloat, float secondFloat, float by)
    {
        return firstFloat * (1 - by) + secondFloat * by;
    }
}
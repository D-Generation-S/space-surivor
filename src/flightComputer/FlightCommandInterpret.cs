using Godot;

public partial class FlightCommandInterpret : Node
{
    public virtual Vector2 IdleBaseVelocity(Vector2 currentVelocity, float currentRotation)
    {
        return Vector2.Zero;
    }

    public virtual float IdleBaseRotation(Vector2 currentVelocity, float currentRotation)
    {
        return 0f;
    }

    public virtual Vector2 InterpretBaseVelocity(Vector2 commandedVelocity, Vector2 currentVelocity, float currentRotation)
    {
        return commandedVelocity;
    }

    public virtual float InterpretRotation(float commandedRotation, Vector2 currentVelocity, float currentRotation)
    {
        return commandedRotation;
    }
}
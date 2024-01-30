using Godot;

public partial class FlightCommandInterpret : Node
{
    [Export]
    private string displayName;

    public virtual void SetupInterpret(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        
    }

    public virtual Vector2 IdleBaseVelocity(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        return Vector2.Zero;
    }

    public virtual float IdleBaseRotation(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        return 0f;
    }

    public virtual Vector2 InterpretBaseVelocity(Vector2 commandedVelocity, Vector2 currentVelocity, float rotationVelocity, float currentShipRotation)
    {
        return commandedVelocity;
    }

    public virtual float InterpretRotation(float commandedRotation, Vector2 currentVelocity, float rotationVelocity, float currentShipRotation)
    {
        return commandedRotation;
    }

    public string GetDisplayName()
    {
        return displayName ?? Name.ToString();
    }
}
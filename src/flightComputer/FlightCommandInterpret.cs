using Godot;

/// <summary>
/// This class is used to interpret commands provided by the flight computer component
/// This class is a abstract base for the strategy pattern
/// </summary>
public partial class FlightCommandInterpret : Node
{
    /// <summary>
    /// The display name for the interpret of the players ui
    /// </summary>
    [Export]
    private string displayName;

    /// <summary>
    /// Method to setup the interpret if selected
    /// </summary>
    /// <param name="currentVelocity">The current velocity of the ship in global space</param>
    /// <param name="currentRotationVelocity">The current rotation velocity of the ship</param>
    /// <param name="currentShipRotation">The current rotation of the ship, this number is always positive</param>
    public virtual void SetupInterpret(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        
    }

    /// <summary>
    /// Method used if no base action like strafing, acceleration or deceleration is triggered
    /// </summary>
    /// <param name="currentVelocity">The current velocity of the ship relative to the ship. This vector is normalized</param>
    /// <param name="currentRotationVelocity">The current rotation velocity of the ship</param>
    /// <param name="currentShipRotation">The current rotation of the ship</param>
    /// <returns>The changed vector interpret by the algorithm implemented by the command interpret</returns>
    public virtual Vector2 IdleBaseVelocity(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        return Vector2.Zero;
    }

    /// <summary>
    /// Method used if no rotation is triggered
    /// </summary>
    /// <param name="currentVelocity">The current velocity of the ship relative to the ship. This vector is normalized</param>
    /// <param name="currentRotationVelocity">The current rotation velocity of the ship</param>
    /// <param name="currentShipRotation">The current rotation of the ship</param>
    /// <returns>The changed rotation vector interpret by the algorithm implemented by the command interpret</returns>
    public virtual float IdleBaseRotation(Vector2 currentVelocity, float currentRotationVelocity, float currentShipRotation)
    {
        return 0f;
    }

    /// <summary>
    /// Method to interpret the velocity change commanded by the flight computer
    /// </summary>
    /// <param name="commandedVelocity">The velocity change commanded by the flight computer</param>
    /// <param name="currentVelocity">The current velocity of the ship relative to the ship. This vector is normalized</param>
    /// <param name="currentRotationVelocity">The current rotation velocity of the ship</param>
    /// <param name="currentShipRotation">The current rotation of the ship</param>
    /// <returns>The changed and interpreted vector by the implementation</returns>
    public virtual Vector2 InterpretBaseVelocity(Vector2 commandedVelocity, Vector2 currentVelocity, float rotationVelocity, float currentShipRotation)
    {
        return commandedVelocity;
    }

    /// <summary>
    /// Method to interpret the rotation change commanded by the flight computer
    /// </summary>
    /// <param name="commandedRotation">The rotation commanded by the flight computer</param>
    /// <param name="currentVelocity">The current velocity of the ship relative to the ship. This vector is normalized</param>
    /// <param name="currentRotationVelocity">The current rotation velocity of the ship</param>
    /// <param name="currentShipRotation">The current rotation of the ship</param>
    /// <returns></returns>
    public virtual float InterpretRotation(float commandedRotation, Vector2 currentVelocity, float rotationVelocity, float currentShipRotation)
    {
        return commandedRotation;
    }

    /// <summary>
    /// Get the display name of this flight computer interpret
    /// </summary>
    /// <returns>The display name</returns>
    public string GetDisplayName()
    {
        return displayName ?? Name.ToString();
    }
}
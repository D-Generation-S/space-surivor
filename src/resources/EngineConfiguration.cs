using Godot;

/// <summary>
/// The configuration for an ship engine
/// </summary>
[GlobalClass]
public partial class EngineConfiguration : BaseComponent
{
    /// <summary>
    /// The acceleration of this engine.
    /// The y component does represent forward and backward,
    /// while x represents the strafing speed
    /// </summary>
    [Export]
    private Vector2 engineAcceleration;
    
    /// <summary>
    /// The rotation speed for this engine
    /// </summary>
    [Export]
    private float engineRotation;

    /// <summary>
    /// The consumption for this engine
    /// </summary>
    [Export]
    private ConsumerConfiguration consumerConfiguration;

    /// <summary>
    /// The heat configuration
    /// </summary>
    [Export]
    private HeatConfiguration heatConfiguration;

    /// <summary>
    /// Get the consumer configuration
    /// </summary>
    /// <returns>The consumption configuration for the engine</returns>
    public ConsumerConfiguration GetConsumerConfiguration()
    {
        return consumerConfiguration;
    }

    /// <summary>
    /// Get the heat configuration
    /// </summary>
    /// <returns>The heat configuration for this engine</returns>
    public HeatConfiguration GetHeatConfiguration()
    {
        return heatConfiguration;
    }

    /// <summary>
    /// Get the acceleration of this engine
    /// </summary>
    /// <returns></returns>
    public Vector2 GetEngineAcceleration()
    {
        return engineAcceleration;
    }

    /// <summary>
    /// Get the rotation speed of this engine
    /// </summary>
    /// <returns>The rotation speed</returns>
    public float GetEngineRotationSpeed()
    {
        return engineRotation;
    }
}

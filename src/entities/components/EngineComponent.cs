using Godot;

/// <summary>
/// The engine component of the ship
/// </summary>
public partial class EngineComponent : ConsumerComponent
{
    /// <summary>
    /// The engine configuration for this component
    /// </summary>
    [Export]
    private EngineConfiguration engineConfiguration;

    /// <summary>
    /// The current heat of the engine
    /// </summary>
    private int currentHeat;

    /// <summary>
    /// The last power consumption in the previous tick of this engine
    /// </summary>
    private int lastPowerConsumption;

    /// <summary>
    /// was the engine on at the last tick
    /// </summary>
    private bool wasOn;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentHeat = 0;
    }

    /// <inheritdoc/>
    public override void ConsumerTick(int tickNumber)
    {
        if (!wasOn)
        {
            lastPowerConsumption = 0;
            return;
        }
        currentHeat += engineConfiguration.GetHeatConfiguration().GetHeatPerTick();
        lastPowerConsumption = engineConfiguration.GetConsumerConfiguration().GetConsumption();
        wasOn = false;
    }

    /// <summary>
    /// Is the engine currently firing
    /// </summary>
    public void Firing()
    {
        wasOn = true;
    }

/// <inheritdoc/>
    public override int GetStoredHeat()
    {
        var returnHeat = currentHeat;
        currentHeat = 0;
        return returnHeat;
    }

/// <inheritdoc/>
    public override int GetConsumption()
    {
        return lastPowerConsumption;
    }

/// <inheritdoc/>
    public override int GetPriority()
    {
        return engineConfiguration.GetConsumerConfiguration().GetPriority();
    }

    /// <summary>
    /// The speed this engine can accelerate with
    /// the y component does represent forward and backward, 
    /// while x represents strafing
    /// </summary>
    /// <returns>The vector with the acceleration of the ship</returns>
    public Vector2 GetAccelerationSpeed()
    {
        return engineConfiguration.GetEngineAcceleration();
    }

    /// <summary>
    /// The speed this engine can rotate the ship with
    /// </summary>
    /// <returns>The rotation speed</returns>
    public float GetRotationSpeed()
    {
        return engineConfiguration.GetEngineRotationSpeed();
    }
}

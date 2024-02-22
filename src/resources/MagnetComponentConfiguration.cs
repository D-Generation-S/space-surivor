using Godot;

/// <summary>
/// Configuration class for a magnet component
/// </summary>
[GlobalClass]
public partial class MagnetComponentConfiguration : BaseComponent
{
    /// <summary>
    /// The consumption configuration for this component
    /// </summary>
    [Export]
    private ConsumerConfiguration consumption;

    /// <summary>
    /// The multiplier to apply to the radius of the collection area
    /// </summary>
    [Export]
    private float magnetMultiplier = 1f;

    /// <summary>
    /// Get the consumption per tick of this magnet
    /// </summary>
    /// <returns>The consumption per tick</returns>
    public int GetConsumption()
    {
        return consumption.GetConsumption();
    }

    /// <summary>
    /// Get the priority of this component for getting power
    /// </summary>
    /// <returns>The priority</returns>
    public int GetPriority()
    {
        return consumption.GetPriority();
    }

    /// <summary>
    /// Get the multiplier to apply to the radius of the collection area
    /// </summary>
    /// <returns>The multiplier value</returns>
    public float GetMultiplier()
    {
        return magnetMultiplier;
    }
}
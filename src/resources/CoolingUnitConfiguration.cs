using Godot;

/// <summary>
/// Configuration for a cooling unit
/// </summary>
[GlobalClass]
public partial class CoolingUnitConfiguration : BaseComponent
{
    /// <summary>
    /// The consumption configuration for this cooling unit
    /// </summary>
    [Export]
    private ConsumerConfiguration consumerConfiguration;

    /// <summary>
    /// The heat which can be absorbed per tick
    /// </summary>
    [Export]
    private int heatAbsorptionPerTick;

    /// <summary>
    /// The max heat which can be stored by this component
    /// </summary>
    [Export]
    private int heatCapacity;

    /// <summary>
    /// Get the consumer configuration of this cooling unit
    /// </summary>
    /// <returns>The consumer configuration</returns>
    public ConsumerConfiguration GetConsumerConfiguration()
    {
        return consumerConfiguration;
    }

    /// <summary>
    /// Get the heat absorption of this component
    /// </summary>
    /// <returns>The heat which is absorbed per tick</returns>
    public int GetHeatAbsorption()
    {
        return heatAbsorptionPerTick;
    }

    /// <summary>
    /// Get the maximal heat capacity which can be stored in this unit
    /// </summary>
    /// <returns>The max heat capacity</returns>
    public int GetHeatCapacity()
    {
        return heatCapacity;
    }
}

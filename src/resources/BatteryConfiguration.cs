using Godot;

/// <summary>
/// Configuration of a battery component
/// </summary>
[GlobalClass]
public partial class BatteryConfiguration : BaseComponent
{
    /// <summary>
    /// The max capacity of the battery
    /// </summary>
    [Export]
    private int maxBatteryCapacity;

    /// <summary>
    /// The heat configuration if battery is loading
    /// </summary>
    [Export]
    private HeatConfiguration heatConfigurationIfLoading;

    /// <summary>
    /// The maximal amount which can be loaded at one
    /// </summary>
    [Export]
    private int maxLoadAmount;


    /// <summary>
    /// The max amount which can be unloaded per tick
    /// </summary>
    [Export]
    private int maxUnloadAmount;

    /// <summary>
    /// The max capacity of the battery
    /// </summary>
    /// <returns>The max battery capacity</returns>
    public int GetCapacity()
    {
        return maxBatteryCapacity;
    }

    /// <summary>
    /// The max amount which can be loaded per tick
    /// </summary>
    /// <returns>The max load amount per tick</returns>
    public int GetMaxLoadAmount()
    {
        return maxLoadAmount;
    }

    /// <summary>
    /// The max unload amount per tick
    /// </summary>
    /// <returns>The max unload amount</returns>
    public int GetMaxUnloadAmount()
    {
        return maxUnloadAmount;
    }

    /// <summary>
    /// Get the heat generated if loading
    /// </summary>
    /// <returns>The heat generated if loading</returns>
    public int GetLoadHeat()
    {
        return heatConfigurationIfLoading.GetHeatPerTick();
    }
}
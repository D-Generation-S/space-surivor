using Godot;

/// <summary>
/// This component does receive and store electricity.
/// It will allow unloading it if required
/// </summary>
public partial class BatteryComponent : ConsumerComponent
{   
    /// <summary>
    /// The battery configuration to use
    /// </summary>
    [Export]
    private BatteryConfiguration batteryConfiguration;

    /// <summary>
    /// The current electricity stored
    /// </summary>
    private int currentLoad;

    /// <summary>
    /// The current heat of the battery
    /// </summary>
    private int storedHeat;

    /// <summary>
    /// Load electricity into the battery
    /// </summary>
    /// <param name="value">The amount to load</param>
    /// <returns>The amount which was added to the battery, this could be less than the amount which should be loaded</returns>
    public int Load(int value)
    {
        var capacity = GetMaxCapacity();
        if (value < 0 || currentLoad >= capacity)
        {
            return 0;
        }
        var maxLoad = batteryConfiguration.GetMaxLoadAmount();
        storedHeat = batteryConfiguration.GetLoadHeat();
        if (value > maxLoad)
        {
            value = maxLoad;
        }
        currentLoad += value;
        var overflow = currentLoad - capacity;
        overflow = overflow < 0 ? 0 : overflow;
        currentLoad -= overflow;
        return value - overflow;
    }

    /// <summary>
    /// Get a specific amount from the battery
    /// </summary>
    /// <param name="amount">The requested amount from the battery</param>
    /// <returns>The amount returned by the battery</returns>
    public int Unload(int amount)
    {
        if (currentLoad == 0)
        {
            return 0;
        }
        amount = amount > batteryConfiguration.GetMaxUnloadAmount() ?  batteryConfiguration.GetMaxUnloadAmount() : amount;
        if (amount > currentLoad)
        {
            currentLoad = 0;
            return currentLoad;
        }
        currentLoad -= amount;
        return amount;
    }

    /// <summary>
    /// The power consumption of the battery
    /// </summary>
    /// <returns>This is always zero for now</returns>
    public override int GetConsumption()
    {
        return 0;
    }

    /// <summary>
    /// Get the heat stored in the battery
    /// </summary>
    /// <returns>The heat amount stored in the battery</returns>
    public override int GetStoredHeat()
    {
        var returnHeat = storedHeat;
        storedHeat = 0;
        return returnHeat;
    }

    /// <summary>
    /// The maximal capacity which can be stored in this battery
    /// </summary>
    /// <returns></returns>
    public int GetMaxCapacity()
    {
        return batteryConfiguration.GetCapacity();
    }

    /// <summary>
    /// The current amount available in this battery
    /// </summary>
    /// <returns>The current battery load</returns>
    public int GetCurrentLoad()
    {
        return currentLoad;
    }

    /// <summary>
    /// The max load which can be added at once to the battery
    /// </summary>
    /// <returns>The max load of the battery</returns>
    public int GetMaxLoad()
    {
        return batteryConfiguration.GetMaxLoadAmount();
    }

    /// <summary>
    /// The max amount which can be unloaded from this battery
    /// </summary>
    /// <returns></returns>
    public int GetMaxUnload()
    {
        return batteryConfiguration.GetMaxUnloadAmount();
    }
}
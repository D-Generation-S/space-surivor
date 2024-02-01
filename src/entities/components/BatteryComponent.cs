using Godot;

public partial class BatteryComponent : ConsumerComponent
{   
    [Export]
    private BatteryConfiguration batteryConfiguration;

    private int currentLoad;

    private int storedHeat;

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

    public override int GetConsumption()
    {
        return 0;
    }

    public override int GetStoredHeat()
    {
        var returnHeat = storedHeat;
        storedHeat = 0;
        return returnHeat;
    }

    public int GetMaxCapacity()
    {
        return batteryConfiguration.GetCapacity();
    }

    public int GetCurrentLoad()
    {
        return currentLoad;
    }

    public int GetMaxLoad()
    {
        return batteryConfiguration.GetMaxLoadAmount();
    }

    public int GetMaxUnload()
    {
        return batteryConfiguration.GetMaxUnloadAmount();
    }
}
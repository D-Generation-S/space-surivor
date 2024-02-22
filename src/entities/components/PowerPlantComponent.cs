using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// The power plant of the ship
/// </summary>
public partial class PowerPlantComponent : ConsumerComponent
{
    /// <summary>
    /// Event if the power used from this plant did change
    /// </summary>
    /// <param name="usagePercentage">The percentage of the produced power which is used per tick</param>
    [Signal]
    public delegate void UsedPlantPowerChangedEventHandler(float usagePercentage);

    /// <summary>
    /// The maximal capacity of the batteries on this ship
    /// </summary>
    /// <param name="newBatteryCapacity">The new max battery capacity</param>
    [Signal]
    public delegate void MaxBatteryCapacityChangedEventHandler(int newBatteryCapacity);
    
    /// <summary>
    /// The current stored electricity in the batteries did change
    /// </summary>
    /// <param name="batteryCapacity">The new capacity of the battery</param>
    [Signal]
    public delegate void BatteryCapacityChangedEventHandler(int batteryCapacity);

    /// <summary>
    /// The configuration of this power plant
    /// </summary>
    [Export]
    private PowerPlantConfiguration plantConfiguration;

    /// <summary>
    /// The max battery capacity for this ship
    /// </summary>
    private int maxBatteryCapacity => shipBatteries.Sum(battery => battery.GetMaxCapacity());

    /// <summary>
    /// The current capacity of all batteries on this ship 
    /// </summary>
    private int currentBatteryCapacity => shipBatteries.Sum(battery => battery.GetCurrentLoad());

    /// <summary>
    /// The maximal amount of energy the batteries can unload per tick
    /// </summary>
    private int maxBatteryUnload => shipBatteries.Sum(battery => battery.GetMaxUnload());
    
    /// <summary>
    /// All the electric consumers of this ship
    /// </summary>
    private List<ConsumerComponent> consumers;

    /// <summary>
    /// All the batteries on this ship
    /// </summary>
    private List<BatteryComponent> shipBatteries;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        ComponentsChanged();
    }

    /// <inheritdoc/>
    public override void ConsumerTick(int tickNumber)
    {
        int powerPlantProduction = plantConfiguration.GetProduction();
        int availablePower = powerPlantProduction;
        int consumedEnergy = 0;
        foreach (var consumer in consumers)
        {
            var consumerConsumption = consumer.GetConsumption();
            if (availablePower - consumerConsumption < 0)
            {
                var powerPool = availablePower;
                var difference = consumerConsumption - powerPool;
                if (currentBatteryCapacity < difference || maxBatteryUnload < difference)
                {
                    consumer.Disable();
                    continue;
                }
                foreach (var battery in shipBatteries)
                {
                    var unloadedAmount = battery.Unload(difference);
                    difference -= unloadedAmount;
                    difference = difference < 0? 0 : difference;
                }
                consumedEnergy += availablePower;
                availablePower = 0;
                consumer.Enable();
                continue;
            }
            consumedEnergy += consumer.GetConsumption();
            consumer.Enable();
            availablePower -= consumerConsumption;
        }


        if (availablePower > 0)
        {
            foreach (var battery in shipBatteries)
            {
                var loadedEnergy = battery.Load(availablePower);
                availablePower -= loadedEnergy;
                consumedEnergy += loadedEnergy;
            }
        }

        
        consumedEnergy = consumedEnergy > powerPlantProduction ? powerPlantProduction : consumedEnergy;
        float usedPercentage = (float)(consumedEnergy / (float)powerPlantProduction);

        EmitSignal(SignalName.UsedPlantPowerChanged, usedPercentage);
        EmitSignal(SignalName.BatteryCapacityChanged, currentBatteryCapacity);
    }

    /// <inheritdoc/>
    public override int GetConsumption()
    {
        return 0;
    }

    /// <inheritdoc/>
    public override int GetStoredHeat()
    {
        return 0;
    }

    /// <summary>
    /// Event to tell the plant to rescan the components. 
    /// </summary>
    public void ComponentsChanged()
    {
        var allConsumers = GetParent().GetChildren()
                         .OfType<ConsumerComponent>()
                         .Where(consumer => consumer != this)
                         .OrderBy(component => component.GetPriority())
                         .ToList();
        consumers = allConsumers.Where(consumer => consumer.GetType() != typeof(BatteryComponent)).ToList();
        shipBatteries = allConsumers.OfType<BatteryComponent>().ToList();

        EmitSignal(SignalName.MaxBatteryCapacityChanged, GetMaxBatteryCapacity());
    }

    public int GetMaxBatteryCapacity()
    {
        return maxBatteryCapacity + shipBatteries.Sum(battery => battery.GetMaxCapacity());
    }

    public int GetCurrentBatteryCapacity()
    {
        return currentBatteryCapacity + shipBatteries.Sum(battery => battery.GetCurrentLoad());
    }
}

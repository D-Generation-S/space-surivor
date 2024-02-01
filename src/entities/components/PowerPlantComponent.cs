using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class PowerPlantComponent : ConsumerComponent
{
	[Signal]
    public delegate void UsedPlantPowerChangedEventHandler(float usagePercentage);

	[Signal]
    public delegate void MaxBatteryCapacityChangedEventHandler(int newBatteryCapacity);
    
    [Signal]
    public delegate void BatteryCapacityChangedEventHandler(int batteryCapacity);

	[Export]
	private PowerPlantConfiguration plantConfiguration;

	private int maxBatteryCapacity => shipBatteries.Sum(battery => battery.GetMaxCapacity());

	private int currentBatteryCapacity => shipBatteries.Sum(battery => battery.GetCurrentLoad());

	private int maxBatteryUnload => shipBatteries.Sum(battery => battery.GetMaxUnload());
	

	private List<ConsumerComponent> consumers;

	private List<BatteryComponent> shipBatteries;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ComponentsChanged();
	}

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
				consumedEnergy += availablePower;
				availablePower = 0;
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
				consumer.Enable();
				continue;
			}
			consumedEnergy += consumer.GetConsumption();
			consumer.Enable();
			availablePower -= consumerConsumption;
		}
		consumedEnergy = consumedEnergy > powerPlantProduction ? powerPlantProduction : consumedEnergy;
		float usedPercentage = (float)(consumedEnergy / (float)powerPlantProduction);


		if (availablePower > 0)
		{
			foreach (var battery in shipBatteries)
			{
				availablePower -= battery.Load(availablePower);
			}
		}

		EmitSignal(SignalName.UsedPlantPowerChanged, usedPercentage);
		EmitSignal(SignalName.BatteryCapacityChanged, currentBatteryCapacity);
    }

    public override int GetConsumption()
    {
        return 0;
    }

    public override int GetStoredHeat()
    {
        return 0;
    }

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

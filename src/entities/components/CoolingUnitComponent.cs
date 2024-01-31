using System;
using System.Linq;
using Godot;

public partial class CoolingUnitComponent : ConsumerComponent
{
    [Signal]
    public delegate void MaxHeatCapacityChangedEventHandler(int newHeatCapacity);
    
    [Signal]
    public delegate void StoredHeatChangedEventHandler(int newHeat);

	[Export]
	private CoolingUnitConfiguration coolingUnitConfiguration;

	[Export]
	private int maxHeatDamage = 5;

	private int storedHeat;

	private HealthComponent healthComponent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		storedHeat = 0;
		healthComponent = GetParent().GetChildren().OfType<HealthComponent>().FirstOrDefault();
		if (healthComponent is null)
		{
			GD.PushError("No health component found");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var consumers = GetParent().GetChildren().OfType<ConsumerComponent>().ToList();
		foreach (var consumer in consumers)
		{
			storedHeat += consumer.GetStoredHeat();
		}
		storedHeat -= coolingUnitConfiguration.GetHeatAbsorption();
		if (storedHeat < 0)
		{
			storedHeat = 0;
		}
		
		if (storedHeat > coolingUnitConfiguration.GetHeatCapacity())
		{
			var damage = storedHeat - coolingUnitConfiguration.GetHeatCapacity();
			healthComponent.Damage(Math.Min(damage, maxHeatDamage));
		}

		EmitSignal(SignalName.StoredHeatChanged, storedHeat);
	}

	public int GetMaxHeatCapacity()
	{
		return coolingUnitConfiguration.GetHeatCapacity();
	}

    public override int GetStoredHeat()
    {
        return storedHeat;
    }

    public override int GetConsumption()
    {
        return coolingUnitConfiguration.GetConsumerConfiguration().GetConsumption();
    }

	public override int GetPriority()
    {
        return coolingUnitConfiguration.GetConsumerConfiguration().GetPriority();
    }
}

using System;
using System.Linq;
using Godot;

/// <summary>
/// Component to cool down the ship,
/// this will fetch heat from the ship's component and cool it down.
/// If the stored head exceeds the capacity it will damage the ship hull
/// </summary>
public partial class CoolingUnitComponent : ConsumerComponent
{
    /// <summary>
    /// Event if the max heat capacity did change
    /// </summary>
    /// <param name="newHeatCapacity"></param>
    [Signal]
    public delegate void MaxHeatCapacityChangedEventHandler(int newHeatCapacity);
    
    /// <summary>
    /// Event if the stored heat capacity did change
    /// </summary>
    /// <param name="newHeat"></param>
    [Signal]
    public delegate void StoredHeatChangedEventHandler(int newHeat);

    /// <summary>
    /// The configuration of this cooling component
    /// </summary>
    [Export]
    private CoolingUnitConfiguration coolingUnitConfiguration;

    /// <summary>
    /// The max heat damage per tick done by this cooling component
    /// </summary>
    [Export]
    private int maxHeatDamage = 5;

    /// <summary>
    /// The currently stored heat amount
    /// </summary>
    private int storedHeat;

    /// <summary>
    /// The health component of the ship to damage
    /// </summary>
    private HealthComponent healthComponent;


    /// <inheritdoc/>
    public override void _Ready()
    {
        storedHeat = 0;
        healthComponent = GetParent().GetChildren().OfType<HealthComponent>().FirstOrDefault();
        if (healthComponent is null)
        {
            GD.PushError("No health component found");
        }
    }

    /// <inheritdoc/>
    public override void ConsumerTick(int tickNumber)
    {
        var consumers = GetParent().GetChildren().OfType<ConsumerComponent>().Where(consumer => consumer != this).ToList();
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

    /// <summary>
    /// Get the maximal heat capacity of this cooling component
    /// </summary>
    /// <returns>The max heat capacity</returns>
    public int GetMaxHeatCapacity()
    {
        return coolingUnitConfiguration.GetHeatCapacity();
    }

    /// <inheritdoc/>
    public override int GetStoredHeat()
    {
        return storedHeat;
    }

    /// <inheritdoc/>
    public override int GetConsumption()
    {
        return coolingUnitConfiguration.GetConsumerConfiguration().GetConsumption();
    }

    /// <inheritdoc/>
    public override int GetPriority()
    {
        return coolingUnitConfiguration.GetConsumerConfiguration().GetPriority();
    }
}

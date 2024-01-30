using System.Reflection.Metadata.Ecma335;
using Godot;

[GlobalClass]
public partial class CoolingUnitConfiguration : Resource
{
    /// <summary>
    /// The name of the engine
    /// </summary>
    [Export]
    private string name;

    [Export]
    private ConsumerConfiguration consumerConfiguration;

    [Export]
    private int heatAbsorptionPerTick;

    [Export]
    private int heatCapacity;

    public string GetName()
    {
        return name;
    }

    public ConsumerConfiguration GetConsumerConfiguration()
    {
        return consumerConfiguration;
    }

    public int GetHeatAbsorption()
    {
        return heatAbsorptionPerTick;
    }

    public int GetHeatCapacity()
    {
        return heatCapacity;
    }
}

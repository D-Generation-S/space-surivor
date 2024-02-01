using System.Reflection.Metadata.Ecma335;
using Godot;

[GlobalClass]
public partial class CoolingUnitConfiguration : BaseComponent
{
    [Export]
    private ConsumerConfiguration consumerConfiguration;

    [Export]
    private int heatAbsorptionPerTick;

    [Export]
    private int heatCapacity;

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

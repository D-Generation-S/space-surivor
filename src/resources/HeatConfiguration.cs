using Godot;

[GlobalClass]
public partial class HeatConfiguration : Resource
{
    /// <summary>
    /// The name of the input
    /// </summary>
    [Export]
    private int heatPerTick;

    public int GetHeatPerTick()
    {
        return heatPerTick;
    }
}

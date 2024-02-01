using Godot;

/// <summary>
/// Heat configuration for a given component
/// </summary>
[GlobalClass]
public partial class HeatConfiguration : Resource
{
    /// <summary>
    /// The heat which is generated per tick
    /// </summary>
    [Export]
    private int heatPerTick;

    /// <summary>
    /// Get the heat generated per tick
    /// </summary>
    /// <returns></returns>
    public int GetHeatPerTick()
    {
        return heatPerTick;
    }
}

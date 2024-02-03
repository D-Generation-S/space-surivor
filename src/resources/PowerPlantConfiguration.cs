using Godot;

/// <summary>
/// The power plant configuration for the ship
/// </summary>
[GlobalClass]
public partial class PowerPlantConfiguration : BaseComponent
{
    /// <summary>
    /// The power produced per tick
    /// </summary>
    [Export]
    private int production;

    /// <summary>
    /// Get the energy amount which is produced per tick
    /// </summary>
    /// <returns>The energy amount</returns>
    public int GetProduction()
    {
        return production;
    }
}

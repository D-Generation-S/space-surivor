using Godot;

/// <summary>
/// The configuration for a weapon
/// </summary>
[GlobalClass]
public partial class WeaponConfiguration : BaseComponent
{
    /// <summary>
    /// Weapon heat produced if active but nothing happens
    /// </summary>
    [ExportGroup("Heat")]
    [Export]
    private int idleHeat;

    /// <summary>
    /// Heat produced if fire is actually fired
    /// </summary>
    [ExportGroup("Heat")]
    [Export]
    private int heatByFire;


    /// <summary>
    /// Weapon consumption if active but nothing happens
    /// </summary>
    [ExportGroup("Consumption")]
    [Export]
    private int consumptionIdle;    

    /// <summary>
    /// Weapon consumption if actually fired
    /// </summary>
    [ExportGroup("Consumption")]
    [Export]
    private int consumptionFire;

    /// <summary>
    /// The sound effect to play when getting fired
    /// </summary>
    [ExportGroup("Effect")]
    [Export]
    private AudioStreamMP3 fireEffect;

    /// <summary>
    /// The rate the weapon can be fired in ticks
    /// </summary>
    [ExportGroup("Rate")]
    [Export]
    private int fireRateInTicks;

    /// <summary>
    /// Get the heat if the weapon is idle
    /// </summary>
    /// <returns>The idle heat</returns>
    public int GetIdleHeat()
    {
        return idleHeat;
    }

    /// <summary>
    /// Get the heat if the weapon is fired
    /// </summary>
    /// <returns>The heat if fired</returns>
    public int GetFiringHeat()
    {
        return heatByFire;
    }

    /// <summary>
    /// Get the consumption if the weapon is idle
    /// </summary>
    /// <returns>The idle consumption</returns>
    public int GetIdleConsumption()
    {
        return consumptionIdle;
    }

    /// <summary>
    /// Get the consumption if the weapon is getting fired
    /// </summary>
    /// <returns>Consumption if fired</returns>
    public int GetFiringConsumption()
    {
        return consumptionFire + GetIdleConsumption();
    }

    /// <summary>
    /// Get the sound effect if weapon is fired
    /// </summary>
    /// <returns>The fire sound effect</returns>
    public AudioStreamMP3 GetFireEffect()
    {
        return fireEffect;
    }

    /// <summary>
    /// Get the fire rate of this weapon
    /// </summary>
    /// <returns>The rate of fire in ticks</returns>
    public int GetFireRate()
    {
        return fireRateInTicks;
    }
}
using Godot;

[GlobalClass]
public partial class WeaponConfiguration : BaseComponent
{
    [ExportGroup("Heat")]
    [Export]
    private int idleHeat;

    [ExportGroup("Heat")]
    [Export]
    private int heatByFire;

    [ExportGroup("Consumption")]
    [Export]
    private int consumptionIdle;    

    [ExportGroup("Consumption")]
    [Export]
    private int consumptionFire;

    [ExportGroup("Effect")]
    [Export]
    private AudioStreamMP3 fireEffect;

    public int GetIdleHeat()
    {
        return idleHeat;
    }

    public int GetFiringHeat()
    {
        return heatByFire;
    }

    public int GetIdleConsumption()
    {
        return consumptionIdle;
    }

    public int GetFiringConsumption()
    {
        return consumptionFire + GetIdleConsumption();
    }

    public AudioStreamMP3 GetFireEffect()
    {
        return fireEffect;
    }
}
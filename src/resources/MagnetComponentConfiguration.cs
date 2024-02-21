using Godot;

[GlobalClass]
public partial class MagnetComponentConfiguration : BaseComponent
{
    [Export]
    private ConsumerConfiguration consumption;

    [Export]
    private float magnetMultiplier = 1f;

    public int GetConsumption()
    {
        return consumption.GetConsumption();
    }

    public int GetPriority()
    {
        return consumption.GetPriority();
    }

    public float GetMultiplier()
    {
        return magnetMultiplier;
    }
}
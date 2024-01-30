using Godot;

[GlobalClass]
public partial class ConsumerConfiguration : Resource
{
    /// <summary>
    /// The name of the input
    /// </summary>
    [Export]
    private int consumption;

    [Export]
    private int priority;

    public int GetConsumption()
    {
        return consumption;
    }

    public int GetPriority()
    {
        return priority;
    }
}

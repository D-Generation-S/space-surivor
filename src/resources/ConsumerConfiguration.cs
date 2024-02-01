using Godot;

[GlobalClass]
public partial class ConsumerConfiguration : Resource
{
    /// <summary>
    /// The name of the input
    /// </summary>
    [Export]
    private int consumption;

    /// <summary>
    /// The priority of this consumer
    /// </summary>
    [Export]
    private int priority;

    /// <summary>
    /// The energy consumption per tick
    /// </summary>
    /// <returns>Consumption per tick</returns>
    public int GetConsumption()
    {
        return consumption;
    }

    /// <summary>
    /// Get the priority of this consumer,
    /// A priority close to 0 means it will get more likely power.
    /// Important core components should be close to 0
    /// </summary>
    /// <returns>The energy priority of this component</returns>
    public int GetPriority()
    {
        return priority;
    }
}

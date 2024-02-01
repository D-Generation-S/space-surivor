using Godot;

/// <summary>
/// A component which will consume or produce some resources
/// Those components do have a stored heat and a electricity requirement
/// </summary>
public partial class ConsumerComponent : Node
{
    /// <summary>
    /// Is this component active right now
    /// </summary>
    private bool isActive = false;

    /// <summary>
    /// Number used to calculate the tick leftover
    /// </summary>
    private double deltaLeftOver = 0f;

    /// <summary>
    /// How many ticks per second should be simulated
    /// </summary>
    private int ticksPerSecond = 10;

    /// <summary>
    /// The tick mark to be used with the delta time
    /// </summary>
    private double tickMark => ticksPerSecond / 60;

    /// <summary>
    /// The current tick number
    /// </summary>
    private int tickNumber = 0;

	public override void _Process(double delta)
	{
        deltaLeftOver += delta;
        if (deltaLeftOver > tickMark)
        {
            deltaLeftOver -= tickMark;
            ConsumerTick(tickNumber);
            tickNumber++;
        }
	}

    /// <summary>
    /// Method used for ticks produced by the tick settings
    /// </summary>
    /// <param name="tickNumber">The number of the current tick</param>
    public virtual void ConsumerTick(int tickNumber)
    {

    }

    /// <summary>
    /// Get the power consumption of this component
    /// This should be the average between ticks since it will only be requested
    /// once per tick.
    /// </summary>
    /// <returns>The power consumption</returns>
    public virtual int GetConsumption()
    {
        return int.MaxValue;
    }

    /// <summary>
    /// The priority of this component to deliver power to it
    /// If the component is critically for running the ship the priority should be close to 0
    /// </summary>
    /// <returns></returns>
    public virtual int GetPriority()
    {
        return int.MaxValue;
    }

    /// <summary>
    /// Get the currently stored heat int this component
    /// This should be the average or sum between ticks.
    /// This will only be fetched once per tick.
    /// If the information is fetched, it should be set to 0 since the
    /// cooler did "remove" it.
    /// </summary>
    /// <returns></returns>
    public virtual int GetStoredHeat()
    {
        return 0;
    }

    /// <summary>
    /// Is this component active right now?
    /// </summary>
    /// <returns>True if active</returns>
    public bool Active()
    {
        return isActive;
    }

    /// <summary>
    /// Enable this component
    /// </summary>
    public void Enable()
    {
        isActive = true;
    }

    /// <summary>
    /// Disable this component
    /// </summary>
    public void Disable()
    {
        isActive = false;
    }
}

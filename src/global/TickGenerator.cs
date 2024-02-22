using Godot;

/// <summary>
/// A singelton class to generate ticks for the game
/// </summary>
public partial class TickGenerator : Node
{
    [Signal]
    public delegate void GameTickEventHandler(int tickNumber);

    /// <summary>
    /// Number used to calculate the tick leftover
    /// </summary>
    private double deltaLeftOver = 0f;

    /// <summary>
    /// How many ticks per second should be simulated
    /// </summary>
    private int ticksPerSecond = 60;

    /// <summary>
    /// The tick mark to be used with the delta time
    /// </summary>
    private double tickMark => (60f - ticksPerSecond) / 60f;

    /// <summary>
    /// The current tick number
    /// </summary>
    private int tickNumber = 0;

    public override void _Ready()
    {
        base._Ready();
        ProcessMode = ProcessModeEnum.Pausable;
    }

    public override void _Process(double delta)
    {
        deltaLeftOver += delta;
        if (deltaLeftOver > tickMark)
        {
            deltaLeftOver -= tickMark;
            EmitSignal(SignalName.GameTick, tickNumber);
            tickNumber++;
        }
    }

    /// <summary>
    /// Get the ticks per second
    /// </summary>
    /// <returns>The number of ticks in a second</returns>
    public int GetTicksPerSecond()
    {
        return ticksPerSecond;
    }
}
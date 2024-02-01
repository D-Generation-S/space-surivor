using Godot;

public partial class ConsumerComponent : Node
{
    private bool isActive = false;

    private double deltaLeftOver = 0f;

    private int ticksPerSecond = 10;

    private double tickMark => ticksPerSecond / 60;

    private int tickNumber = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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

    public virtual void ConsumerTick(int tickNumber)
    {

    }

    public virtual int GetConsumption()
    {
        return int.MaxValue;
    }

    public virtual int GetPriority()
    {
        return int.MaxValue;
    }

    public virtual int GetStoredHeat()
    {
        return 0;
    }

    public bool Active()
    {
        return isActive;
    }

    public void Enable()
    {
        isActive = true;
    }

    public void Disable()
    {
        isActive = false;
    }
}

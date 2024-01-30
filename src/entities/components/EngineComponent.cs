using Godot;

public partial class EngineComponent : ConsumerComponent
{
    [Export]
    private EngineConfiguration engineConfiguration;

    private int currentHeat;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        currentHeat = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void Firing()
    {
        currentHeat += engineConfiguration.GetHeatConfiguration().GetHeatPerTick();
    }

    public override int GetStoredHeat()
    {
        var returnHeat = currentHeat;
        currentHeat = 0;
        return returnHeat;
    }

    public override int GetConsumption()
    {
        return engineConfiguration.GetConsumerConfiguration().GetConsumption();
    }

    public override int GetPriority()
    {
        return engineConfiguration.GetConsumerConfiguration().GetPriority();
    }

    public Vector2 GetAccelerationSpeed()
    {
        return engineConfiguration.GetEngineAcceleration();
    }

    public float GetRotationSpeed()
    {
        return engineConfiguration.GetEngineRotationSpeed();
    }
}

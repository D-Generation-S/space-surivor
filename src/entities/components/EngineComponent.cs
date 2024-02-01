using Godot;

public partial class EngineComponent : ConsumerComponent
{
    [Export]
    private EngineConfiguration engineConfiguration;

    private int currentHeat;

    private int lastPowerConsumption;

    private bool wasOn;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        currentHeat = 0;
	}

    public override void ConsumerTick(int tickNumber)
    {
        if (!wasOn)
        {
            lastPowerConsumption = 0;
            return;
        }
        currentHeat += engineConfiguration.GetHeatConfiguration().GetHeatPerTick();
        lastPowerConsumption = engineConfiguration.GetConsumerConfiguration().GetConsumption();
        wasOn = false;
    }

    public void Firing()
    {
        wasOn = true;
    }

    public override int GetStoredHeat()
    {
        var returnHeat = currentHeat;
        currentHeat = 0;
        return returnHeat;
    }

    public override int GetConsumption()
    {
        return lastPowerConsumption;
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

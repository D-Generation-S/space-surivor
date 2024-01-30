using Godot;

[GlobalClass]
public partial class EngineConfiguration : Resource
{
    /// <summary>
    /// The name of the engine
    /// </summary>
    [Export]
    private string name;

    [Export]
    private Vector2 engineAcceleration;
    

    [Export]
    private float engineRotation;

    [Export]
    private int engineSize;

    [Export]
    private ConsumerConfiguration consumerConfiguration;

    [Export]
    private HeatConfiguration heatConfiguration;

    public string GetName()
    {
        return name;
    }

    public ConsumerConfiguration GetConsumerConfiguration()
    {
        return consumerConfiguration;
    }

    public HeatConfiguration GetHeatConfiguration()
    {
        return heatConfiguration;
    }

    public Vector2 GetEngineAcceleration()
    {
        return engineAcceleration;
    }

    public float GetEngineRotationSpeed()
    {
        return engineRotation;
    }

    public int GetEngineSize()
    {
        return engineSize;
    }
}

using System.Linq;
using Godot;

public partial class MagnetComponent : ConsumerComponent
{
    [Export]
    private MagnetComponentConfiguration configuration;

    [Export]
    private CollectionArea collectionShape;

    private Node2D controlledParent;

    public override void _Ready()
    {
        collectionShape.SetRadiusMultiplier(configuration.GetMultiplier());
        controlledParent = GetNode("%Components").GetParent<Node2D>();
        controlledParent.TreeExiting += () => {
            controlledParent = null;  
        };
    }

    public override void _PhysicsProcess(double delta)
    {
        if (controlledParent is null)
        {
            return;
        }
        foreach(var item in collectionShape.GetOverlappingBodies().Where(item => item.IsInGroup("experience")).OfType<ExperiencePoint>())
        {
            item.Attract(controlledParent);
        }
    }

    public override int GetConsumption()
    {
        return configuration.GetConsumption();
    }

    public override int GetStoredHeat()
    {
        return 0;
    }

    public override int GetPriority()
    {
        return configuration.GetPriority();
    }
}

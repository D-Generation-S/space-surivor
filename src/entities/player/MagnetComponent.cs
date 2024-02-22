using System.Linq;
using Godot;

/// <summary>
/// Ship component to allow the player to pull in experience points
/// </summary>
public partial class MagnetComponent : ConsumerComponent
{
    /// <summary>
    /// The configuration of the magnet, this tells the magnet how much area there is
    /// </summary>
    [Export]
    private MagnetComponentConfiguration configuration;

    /// <summary>
    /// The collection area of the magnet, everything touching this area will be pulled towards the player
    /// </summary>
    [Export]
    private CollectionArea collectionShape;

    /// <summary>
    /// The parent which is controlled by this component,
    /// this is required to tell the experience points where to go to
    /// </summary>
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

    /// <inheritdoc/>
    public override int GetConsumption()
    {
        return configuration.GetConsumption();
    }

    /// <inheritdoc/>
    public override int GetStoredHeat()
    {
        return 0;
    }

    /// <inheritdoc/>
    public override int GetPriority()
    {
        return configuration.GetPriority();
    }
}

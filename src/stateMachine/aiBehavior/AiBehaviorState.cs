using System.Linq;
using Godot;

public partial class AiBehaviorState : State
{
    [Export]
    private AiToFlightComputer aiToFlightComputer;

    private EntityMovement thisEntity;

    private EntityMovement player;

    public override void _Ready()
    {
        player = GetTree().Root.GetNodesInGroup<EntityMovement>("player", true).FirstOrDefault();
        thisEntity = aiToFlightComputer.GetControlledEntity();
        if (player is not null)
        {
            player.TreeExiting += () => 
            {
                player = null;
            };
        }
    }

    public AiToFlightComputer GetFlightComputerBridge()
    {
        return aiToFlightComputer;
    }

    public EntityMovement GetPlayer()
    {
        return player;
    }

    public EntityMovement GetParentMoveable()
    {
        return thisEntity;
    }
}
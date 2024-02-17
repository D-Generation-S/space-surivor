using System.Linq;
using Godot;

/// <summary>
/// Behavior for an enemy ai based on a FSM
/// </summary>
public partial class AiBehaviorState : State
{
    /// <summary>
    /// The flight ai to flight computer interface
    /// </summary>
    [Export]
    private AiToFlightComputer aiToFlightComputer;

    /// <summary>
    /// The entity which is controlled by this ai behavior
    /// </summary>
    private EntityMovement thisEntity;

    /// <summary>
    /// The player entity
    /// </summary>
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

    /// <summary>
    /// The the ai flight computer bridge
    /// </summary>
    /// <returns>The ai computer bridge</returns>
    public AiToFlightComputer GetFlightComputerBridge()
    {
        return aiToFlightComputer;
    }

    /// <summary>
    /// Get the player
    /// </summary>
    /// <returns>The player object</returns>
    public EntityMovement GetPlayer()
    {
        return player;
    }

    /// <summary>
    /// Get the controlled entity for this ai behavior
    /// </summary>
    /// <returns>The controlled entity</returns>
    public EntityMovement GetParentMoveable()
    {
        return thisEntity;
    }
}
using Godot;

/// <summary>
/// Class which will hover the controlled entity to the tracked item,
/// hover means it will go to the target without using the newtonian physic.
/// </summary>
public partial class HoverToItem : State
{
    /// <summary>
    /// The state used to search an target to hover to
    /// </summary>
    [ExportGroup("State settings")]
    [Export]
    private State searchState;

    /// <summary>
    /// The state used to stop all motion
    /// </summary>
    [Export]
    private State stopState;

    /// <summary>
    /// The speed this item does move
    /// </summary>
    [Export]
    private float movementSpeed = 50;

    /// <summary>
    /// The distance in which the item will be considered close enough
    /// </summary>
    [Export]
    private float closeInDistance = 5;

    /// <summary>
    /// The maximal distance this item will try to track the target before stopping
    /// </summary>
    [Export]
    private float maxFollowDistance = 500;

    /// <summary>
    /// The closest item key to move to
    /// </summary>
    [ExportGroup("Blackboard settings")]
    [Export]
    private string closestItem;
    
    /// <summary>
    /// The body which is controlled by this state
    /// </summary>
    [Export]
    private string parentItemKey;

    /// <inheritdoc />
    public override void Enter()
    {
        if (blackboard is null)
        {
            GD.PushError("Requires Blackboard");
            return;
        }
        var trackedItem = blackboard.GetData<Node2D>(closestItem);
        if (!TrackedItemIsValid(trackedItem) && searchState is not null)
        {
            EmitSignal(SignalName.Transitioned, this, searchState.GetType().Name);
        }
    }

    /// <inheritdoc />
    public override void Update(double delta)
    {
        var trackedItem = blackboard.GetData<Node2D>(closestItem);
        var controlledBody = blackboard.GetData<CharacterBody2D>(parentItemKey);
        if (stopState is not null && !TrackedItemIsValid(trackedItem))
        {
            EmitSignal(SignalName.Transitioned, this, stopState.GetType().Name);
            return;
        }
        if (stopState is not null && trackedItem.GlobalPosition.DistanceTo(controlledBody.GlobalPosition) < closeInDistance)
        {
            EmitSignal(SignalName.Transitioned, this, stopState.GetType().Name);
            return;
        }
        if (stopState is not null && trackedItem.GlobalPosition.DistanceTo(controlledBody.GlobalPosition) > maxFollowDistance)
        {
            EmitSignal(SignalName.Transitioned, this, stopState.GetType().Name);
            return;
        }
        Vector2 newVelocity = trackedItem.GlobalPosition - controlledBody.GlobalPosition;
        controlledBody.Velocity = newVelocity.Normalized() * movementSpeed;
    }

    /// <summary>
    /// Method to check if the tracked item is still valid
    /// </summary>
    /// <param name="trackedItem">The tracked item to check</param>
    /// <returns>True if the tracked item is still a valid one</returns>
    private bool TrackedItemIsValid(Node2D trackedItem)
    {
        return trackedItem is not null && IsInstanceValid(trackedItem);
    }
}

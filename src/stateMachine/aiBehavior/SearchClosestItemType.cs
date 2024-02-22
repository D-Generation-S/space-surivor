using Godot;

/// <summary>
/// Search the closest item of the given type on the whole game screen
/// </summary>
public partial class SearchClosestItemType : State
{
    /// <summary>
    /// The name of the key to store the item into
    /// </summary>
    [Export]
    private string blackboardKeyName;

    /// <summary>
    /// The name of the group the item is in
    /// </summary>
    [Export]
    private string groupName;
    
    /// <summary>
    /// The reference node to calculate the distance to the item
    /// </summary>
    [Export]
    private Node2D referenceNode;

    /// <summary>
    /// The next state if an item of that kind was found
    /// </summary>
    [Export]
    private State nextState;
    
    /// <summary>
    /// The number of attempts to find an item
    /// </summary>
    [Export]
    private int searchAttempts = 5;

    /// <summary>
    /// The state to use if all attempts failed
    /// </summary>
    [Export]
    private State pauseState;

    /// <summary>
    /// The current attempt this state is on
    /// </summary>
    private int currentAttempt;

    /// <inheritdoc />
    public override void Enter()
    {
        if (blackboard is null)
        {
            GD.PushError("Requires blackboard");
            return;
        }
        currentAttempt = 0;
    }

    /// <inheritdoc />
    public override void Update(double delta)
    {
        if (currentAttempt > searchAttempts && pauseState is not null)
        {
            EmitSignal(SignalName.Transitioned, this, pauseState.GetType().Name);
        }
        var entities = GetTree().Root.GetNodesInGroup<Node2D>(groupName, true);
        float distance = float.MaxValue;
        Node2D foundEntity = null;
        foreach(var entity in entities) 
        {
            float currentDistance = referenceNode.GlobalPosition.DistanceTo(entity.GlobalPosition);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                foundEntity = entity;
            }
        }
        if ( foundEntity is not null && nextState is not null)
        {
            blackboard.SetData(blackboardKeyName, foundEntity);
            EmitSignal(SignalName.Transitioned, this, nextState.GetType().Name);
        }

        currentAttempt++;
    }
}

using Godot;
using System.Linq;

/// <summary>
/// Search the closest item within a given collision area
/// </summary>
public partial class SearchCloseItemState : State
{
    /// <summary>
    /// The area to search the item in
    /// </summary>
    [ExportGroup("State settings")]
    [Export]
    private Area2D searchArea;

    /// <summary>
    /// The group to search for
    /// </summary>
    [Export]
    private string searchGroup;
    
    /// <summary>
    /// The next state to use if a item was found
    /// </summary>
    [Export]
    private State nextState;

    /// <summary>
    /// The key for the closest item found
    /// </summary>
    [ExportGroup("Blackboard keys")]
    [Export]
    private string closestItem;

    /// <summary>
    /// The key for the parent item used to calculate the distance
    /// </summary>
    [Export]
    private string parentItem;

    /// <inheritdoc />
    public override void _Ready()
    {
        searchArea.TreeExiting += () => searchArea = null;
    }

    /// <inheritdoc />
    public override void Enter()
    {
        if (blackboard is null)
        {
            GD.PushError("Requires blackboard");
            return;
        }
        blackboard.SetData(parentItem, GetParent().GetParent<Node2D>());
    }

    /// <inheritdoc />
    public override void Update(double delta)
    {
        if (blackboard is null)
        {
            GD.PushError("Requires blackboard");
            return;
        }
        var followItem = blackboard?.GetData<Node2D>(this.closestItem);
        if (followItem is not null && nextState is not null)
        {
            EmitSignal(SignalName.Transitioned, this, nextState.GetType().Name);
            return;
        }

        var experience = searchArea.GetOverlappingBodies()
                                   .Where(item => IsInstanceValid(item))
                                   .Where(item => item.IsInGroup(searchGroup))
                                   .OfType<Node2D>();
        Node2D closestItem = null;
        float closestDistance = float.MaxValue;
        var parent = blackboard.GetData<Node2D>(parentItem);
        foreach (var experienceNode in experience)
        {
            var distance = experienceNode.GlobalPosition.DistanceTo(parent.GlobalPosition);
            if (distance < closestDistance && distance > 0)
            {
                closestItem = experienceNode;
            }
        }
        if (closestItem is not null)
        {
            SetFollowItem(closestItem);
        }
    }

    /// <summary>
    /// Method to set the item which should be followed
    /// </summary>
    /// <param name="followItem">The item to follow</param>
    public void SetFollowItem(Node2D followItem)
    {
        blackboard?.SetData(closestItem, followItem);
    }
}

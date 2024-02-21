using Godot;
using System.Linq;

public partial class SearchCloseItemState : State
{
    [ExportGroup("State settings")]
    [Export]
    private Area2D searchArea;

    [Export]
    private string searchGroup;
    
    [Export]
    private State nextState;

    [ExportGroup("Blackboard keys")]
    [Export]
    private string closest_item;

    [Export]
    private string parent_item;

    


    public override void _Ready()
    {
        searchArea.TreeExiting += () => searchArea = null;
    }

    public override void Enter()
    {
        if (blackboard is null)
        {
            GD.PushError("Requires blackboard");
            return;
        }
        blackboard.SetData(parent_item, GetParent().GetParent<Node2D>());
    }

    public override void Update(double delta)
    {
        if (blackboard is null)
        {
            GD.PushError("Requires blackboard");
            return;
        }
        var followItem =  blackboard?.GetData<Node2D>(closest_item);
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
        var parent = blackboard.GetData<Node2D>(parent_item);
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

    public void SetFollowItem(Node2D followItem)
    {
        blackboard?.SetData(closest_item, followItem);
    }
}

using Godot;
using System;

public partial class HoverToItem : State
{
    [ExportGroup("State settings")]
    [Export]
    private State searchState;

    [Export]
    private State stopState;

    [Export]
    private CharacterBody2D controlledBody;

    [Export]
    private float movementSpeed = 50;

    [Export]
    private float closeInDistance = 5;

    [Export]
    private float maxFollowDistance = 500;

    [ExportGroup("Blackboard settings")]
    [Export]
    private string closestItem;

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

    public override void Update(double delta)
    {
        var trackedItem = blackboard.GetData<Node2D>(closestItem);
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

    private bool TrackedItemIsValid(Node2D trackedItem)
    {
        return trackedItem is not null && IsInstanceValid(trackedItem);
    }
}

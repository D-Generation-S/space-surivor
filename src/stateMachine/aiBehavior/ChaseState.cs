using Godot;


/// <summary>
/// Ai behavior to hunt down the player and try to collide with him
/// </summary>
public partial class ChaseState : State
{
    [Export]
    private string chasedObjectBlackboardKey;
    
    [Export]
    private string aiToFlightComputerKey;

    [Export]
    private float targetDistance = 0;

    [Export]
    private State nextState;

    
    [Export]
    private State errorState;

    /// <inheritdoc />
    public override void Enter()
    {
        if (blackboard is null)
        {
            GD.PushError("Requires blackboard");
            return;
        }
    }
    
    /// <inheritdoc />
    public override void Update(double delta)
    {
        var chasedObject = blackboard.GetData<PlayerEntity>(chasedObjectBlackboardKey);
        var aiBridge = blackboard.GetData<AiToFlightComputer>(aiToFlightComputerKey);

        if (!IsInstanceValid(chasedObject))
        {
            if (errorState is not null)
            {
                EmitSignal(SignalName.Transitioned, this, errorState);
            }

            return;
        }

        if (chasedObject is null || aiBridge is null)
        {
            if (errorState is not null)
            {
                EmitSignal(SignalName.Transitioned, this, errorState);
            }
            return;
        }
        var thisObject = aiBridge.GetControlledEntity();
        aiBridge.SetGlobalVelocity(chasedObject.GlobalPosition - thisObject.GlobalPosition);
        aiBridge.SetSpeed(1);

        if (thisObject.GlobalPosition.DistanceTo(chasedObject.GlobalPosition) < targetDistance && nextState is not null)
        {
            EmitSignal(SignalName.Transitioned, this, nextState);
        }
    }
}

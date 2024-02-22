using Godot;


/// <summary>
/// Ai behavior to hunt down the player and try to collide with him
/// </summary>
public partial class ChaseState : State
{
    /// <summary>
    /// The string of the chased object key on the blackboard
    /// </summary>
    [Export]
    private string chasedObjectBlackboardKey;
    
    /// <summary>
    /// The ai bridge to use to communicate with the flight computer
    /// </summary>
    [Export]
    private string aiToFlightComputerKey;

    /// <summary>
    /// The distance to get to the target before switching to the next state
    /// </summary>
    [Export]
    private float targetDistance = 0;

    /// <summary>
    /// The next state to use if within the given distance
    /// </summary>
    [Export]
    private State nextState;

    /// <summary>
    /// The state to switch to if something goes wrong, like the chased target got destroyed
    /// </summary>
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

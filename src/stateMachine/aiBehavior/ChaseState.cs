using Godot;

public partial class ChaseState : AiBehaviorState
{
    public override void Enter()
    {
 
    }

    public override void Update(double delta)
    {
        var player = GetPlayer();
        if (player is null)
        {
            EmitSignal(SignalName.Transitioned, this, nameof(IdleState));
        }
        var thisObject = GetParentMoveable();
        GetFlightComputerBridge().SetGlobalVelocity(player.GlobalPosition - thisObject.GlobalPosition);
        GetFlightComputerBridge().SetSpeed(1);
    }
}

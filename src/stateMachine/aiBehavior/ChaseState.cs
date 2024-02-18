/// <summary>
/// Ai behavior to hunt down the player and try to collide with him
/// </summary>
public partial class ChaseState : AiBehaviorState
{
    /// <inheritdoc />
    public override void Enter()
    {
 
    }
    
    /// <inheritdoc />
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

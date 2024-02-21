using Godot;

/// <summary>
/// Simple state to stop an entities movement
/// </summary>
public partial class StopState : State
{
    /// <summary>
    /// The character body to stop
    /// </summary>
    [Export]
    private CharacterBody2D controlledBody;

    /// <summary>
    /// The next state after stopping the movement
    /// </summary>
    [Export]
    private State nextState;

    /// <inheritdoc />
    public override void Enter()
    {
        if (controlledBody is null)
        {
            GD.PushError("Missing controlled body");
            return;
        }
        controlledBody.Velocity = Vector2.Zero;
        
        if (nextState is not null)
        {
            EmitSignal(SignalName.Transitioned, this, nextState.GetType().Name);
        }
    }

    /// <inheritdoc />
    public override void Update(double delta)
    {
        if (nextState is not null)
        {
            EmitSignal(SignalName.Transitioned, this, nextState.GetType().Name);
        }
    }
}

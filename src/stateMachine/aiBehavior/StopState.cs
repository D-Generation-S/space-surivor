using Godot;

public partial class StopState : State
{

    [Export]
    private CharacterBody2D controlledBody;

    [Export]
    private State nextState;

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

    public override void Update(double delta)
    {
        if (nextState is not null)
        {
            EmitSignal(SignalName.Transitioned, this, nextState.GetType().Name);
        }
    }
}

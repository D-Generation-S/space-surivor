using Godot;

public partial class State : Node
{
    [Signal]
    public delegate void TransitionedEventHandler(State caller, string newStateName);

    public virtual void Enter() {}

    public virtual void Exit() {}

    public virtual void Update(double delta) {}

    public virtual void PhysicUpdate(double delta) {}
}
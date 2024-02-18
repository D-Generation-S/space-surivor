using Godot;

/// <summary>
/// A simple state for a FSM
/// </summary>
public partial class State : Node
{
    /// <summary>
    /// The event handler to transit to a new state
    /// </summary>
    /// <param name="caller">The state which called the transition</param>
    /// <param name="newStateName">The name of the new state to change to</param>
    [Signal]
    public delegate void TransitionedEventHandler(State caller, string newStateName);

    /// <summary>
    /// Called if this state is entered 
    /// </summary>
    public virtual void Enter() {}

    /// <summary>
    /// Called if this state is leaving
    /// </summary>
    public virtual void Exit() {}

    /// <summary>
    /// Method called every update of the game
    /// </summary>
    /// <param name="delta">The delta time elapsed</param>
    public virtual void Update(double delta) {}

    /// <summary>
    /// Method called every physic update of the game
    /// </summary>
    /// <param name="delta">The delta time elapsed</param>
    public virtual void PhysicUpdate(double delta) {}
}
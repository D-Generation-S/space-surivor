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
    /// The blackboard to use
    /// </summary>
    protected Blackboard blackboard;

    /// <summary>
    /// The node was loaded for the first time
    /// <param name="blackboard">The blackboard if any featured</param>
    /// </summary>
    public virtual void Init(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }

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
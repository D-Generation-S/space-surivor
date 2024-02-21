using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;

/// <summary>
/// Class for a state machine, using the state object
/// </summary>
public partial class StateMachine : Node
{
    /// <summary>
    /// The initial state this machine is using
    /// </summary>
    [Export]
    private State initialState;

    /// <summary>
    /// Does this state machine does have a blackboard
    /// </summary>
    [Export]
    private bool featuresBlackboard = false;

    /// <summary>
    /// All states stored in this machine, where the key is the state name
    /// </summary>
    private Dictionary<string, State> states;

    /// <summary>
    /// The currently active state for this machine
    /// </summary>
    private State currentState;

    /// <summary>
    /// The blackboard to use
    /// </summary>
    private Blackboard blackboard;

    public override void _Ready()
    {
        states = new Dictionary<string, State>();
        if (featuresBlackboard)
        {
            blackboard = new Blackboard();
        }
        foreach(var state in  GetChildren().OfType<State>()
                                          .ToList())
        {
            states.Add(state.GetType().Name.ToString().ToLower(), state);
            state.Transitioned += OnStateTransitioned;
            state.Init(blackboard);
        }

        if (initialState is not null)
        {
            currentState = initialState;
            initialState.Enter();
        }

    }

    /// <summary>
    /// If a transition was made, this will change the state if needed
    /// </summary>
    /// <param name="caller">The state which called for the transition</param>
    /// <param name="newStateName">The new state to change to</param>
    private void OnStateTransitioned(State caller, string newStateName)
    {
        newStateName = newStateName.ToLower();
        if (caller != currentState || !states.ContainsKey(newStateName))
        {
            return;
        }
        var newState = states[newStateName];
        currentState?.Exit();
        newState.Enter();
        currentState = newState;
    }

    public override void _Process(double delta)
    {
        if (currentState is null)
        {
            return;
        }
        currentState.Update(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (currentState is null)
        {
            return;
        }
        currentState.PhysicUpdate(delta);
        Debug.WriteLine(currentState.Name);
    }
}
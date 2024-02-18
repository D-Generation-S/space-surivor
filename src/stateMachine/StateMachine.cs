using System.Collections.Generic;
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
    /// All states stored in this machine, where the key is the state name
    /// </summary>
    private Dictionary<string, State> states;

    /// <summary>
    /// The currently active state for this machine
    /// </summary>
    private State currentState;

    public override void _Ready()
    {
        states = new Dictionary<string, State>();
        foreach(var state in  GetChildren().OfType<State>()
                                          .ToList())
        {
            states.Add(state.GetType().Name.ToString().ToLower(), state);
            state.Transitioned += OnStateTransitioned;
        }

        if (initialState is not null)
        {
            initialState.Enter();
            currentState = initialState;
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
    }
}
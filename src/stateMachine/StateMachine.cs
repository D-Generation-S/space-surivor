using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class StateMachine : Node
{
    [Export]
    private State initialState;

    private Dictionary<string, State> states;

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
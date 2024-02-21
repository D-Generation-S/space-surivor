using System;
using Godot;

/// <summary>
/// Idle ai state, simple wandering around
/// </summary>
public partial class IdleState : State
{
    [Export]
    private string aiToFlightComputerKey;

    [Export]
    private State checkState;

    /// <summary>
    /// The wandering direction
    /// </summary>
    private Vector2 moveDirection;

    /// <summary>
    /// The time left until the wander direction is changed
    /// </summary>
    private float wanderTime;

    /// <summary>
    /// Randomize the wander information
    /// </summary>
    private void RandomizeWander()
    {
        moveDirection = new Vector2(Random.Shared.Next(-1, 1), Random.Shared.Next(-1, 1)).Normalized();
        wanderTime = Random.Shared.Next(5, 10);
        var aiToFlightComputer = blackboard.GetData<AiToFlightComputer>(aiToFlightComputerKey);
        aiToFlightComputer.SetGlobalVelocity(moveDirection);		
        aiToFlightComputer.SetSpeed(1);
    }

    /// <inheritdoc />
    public override void Enter()
    {
        RandomizeWander();
    }

    /// <inheritdoc />
    public override void Update(double delta)
    {
        if (wanderTime > 0)
        {
            wanderTime -= (float)delta;
            return;
        }

        RandomizeWander();
        if (checkState is not null)
        {
            EmitSignal(SignalName.Transitioned, this, checkState.GetType().Name);
        }
        
    }

}

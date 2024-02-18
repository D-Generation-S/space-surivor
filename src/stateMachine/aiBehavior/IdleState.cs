using System;
using Godot;

/// <summary>
/// Idle ai state, simple wandering around
/// </summary>
public partial class IdleState : AiBehaviorState
{

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
	}

	/// <inheritdoc />
    public override void Enter()
    {
        RandomizeWander();
    }

	/// <inheritdoc />
    public override void Update(double delta)
    {
		if (GetPlayer() is not null)
		{
			EmitSignal(SignalName.Transitioned, this, nameof(ChaseState));
			return;
		}
        if (wanderTime > 0)
		{
			wanderTime -= (float)delta;
			return;
		}
		var parent = GetParentMoveable();
		RandomizeWander();
		GetFlightComputerBridge().SetGlobalVelocity(moveDirection);
		GetFlightComputerBridge().SetSpeed(1);
		
    }

}

using System;
using Godot;

public partial class IdleState : AiBehaviorState
{
	private Vector2 moveDirection;
	private float wanderTime;

	private void RandomizeWander()
	{
		moveDirection = new Vector2(Random.Shared.Next(-1, 1), Random.Shared.Next(-1, 1)).Normalized();
		wanderTime = Random.Shared.Next(5, 10);
	}

    public override void Enter()
    {
        RandomizeWander();
    }

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

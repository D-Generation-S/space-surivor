using Godot;
using System;

public partial class EntityMovement : CharacterBody2D
{
	[Signal]
	public delegate void PoweringForwardEventHandler();

	[Signal]
	public delegate void IdleForwardEventHandler();

	[Export]
	private Vector2 maxVelocity = new Vector2(15, 15);

	[Export]
	private float speed = 25;

	private Vector2 currentInput;



	public override void _PhysicsProcess(double delta)
	{
		currentInput = Vector2.Up;

		Velocity += currentInput * speed;	

		if (Velocity.X > maxVelocity.X || Velocity.Y > maxVelocity.Y)
		{
			float newXVelocity = Velocity.X > maxVelocity.X ? maxVelocity.X : Velocity.X;
			float newYVelocity = Velocity.Y > maxVelocity.Y ? maxVelocity.Y : Velocity.Y;
			Velocity = new Vector2(newXVelocity, newYVelocity);
		}

		if (currentInput.Y < 0)
		{
			EmitSignal(SignalName.PoweringForward);
		}
		if (currentInput.Y >= 0)
		{
			EmitSignal(SignalName.IdleForward);
		}

		MoveAndSlide();
	}
}

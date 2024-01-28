using Godot;
using System;
using System.Diagnostics;

public partial class EntityMovement : CharacterBody2D
{
	[Signal]
	public delegate void PoweringForwardEventHandler();

	[Signal]
	public delegate void IdleForwardEventHandler();

	[ExportGroup("Base Movement")]
	[Export]
	private float maxForward = 250;

	[ExportGroup("Base Movement")]
	[Export]
	private float maxStrafe = 150;

	[ExportGroup("Base Movement")]
	[Export]
	private Vector2 accelerationSpeed = new Vector2(10, 5);

	[ExportGroup("Rotation")]
	[Export]
	private float maxRotationVelocity = 0.2f;

	[ExportGroup("Rotation")]
	[Export]
	private float rotationSpeedDegree = 0.2f;

	
	[ExportGroup("Rotation")]
	[Export]
	private float cancelRotationBelow = 0.003f;

	private float rotationSpeedRadian => DegreeToRad(rotationSpeedDegree);

	private Vector2 velocityInput;

	private float rotationInput;

	private float rotationVelocity;

	private float deviation = 0.001f;

	public override void _PhysicsProcess(double delta)
	{
		var accelerationMultiplier = velocityInput.X < 0 ? accelerationSpeed.X : accelerationSpeed.Y;
		Velocity += velocityInput * accelerationMultiplier;
		rotationVelocity += rotationInput * rotationSpeedRadian;
		rotationVelocity = Math.Abs(rotationVelocity) < cancelRotationBelow ? 0 : rotationVelocity;
		Rotate(rotationVelocity);

		if (velocityInput.Y < 0)
		{
			EmitSignal(SignalName.PoweringForward);
		}
		if (velocityInput.Y >= 0)
		{
			EmitSignal(SignalName.IdleForward);
		}

		MoveAndSlide();

		Vector2 relativeVelocity = GetRealVelocity().Rotated(Rotation);
		if (Math.Abs(relativeVelocity.Y) > maxForward + deviation || Math.Abs(relativeVelocity.X) > maxStrafe + deviation)
		{
			Vector2 FixedVelocity = FixVelocity(relativeVelocity);
			//Velocity = FixedVelocity;
		}

		if (Math.Abs(rotationVelocity) > maxRotationVelocity)
		{
			rotationVelocity = rotationVelocity < 0 ? -maxRotationVelocity : maxRotationVelocity;
		}

		rotationInput = 0f;
		velocityInput = Vector2.Zero;
	}

    private Vector2 FixVelocity(Vector2 shipVelocity)
    {
		float newXVelocity = shipVelocity.X;
        float newYVelocity = shipVelocity.Y;
		if (Math.Abs(shipVelocity.X) > maxStrafe)
		{
			newXVelocity = shipVelocity.X < 0 ? -maxStrafe : maxStrafe;
			Debug.WriteLine("Fix X");
			Debug.WriteLine(newXVelocity);
		}		
		if (Math.Abs(shipVelocity.Y) > maxForward)
		{
			newYVelocity = shipVelocity.Y < 0 ? -maxForward : maxForward;
			Debug.WriteLine("Fix Y");
			Debug.WriteLine(newYVelocity);
		}
		Debug.WriteLine("Fix called!");
		Debug.WriteLine(Rotation);
		return new Vector2(newXVelocity, newYVelocity).Rotated(-Rotation);
    }

    private float DegreeToRad(float degree)
	{
		return (float)(degree * (Math.PI / 180));
	}

	public void InputVelocity(Vector2 currentBaseVelocity)
	{
		velocityInput = currentBaseVelocity.Rotated(Rotation).Normalized();
	}

	public void InputRotation(float rotation)
	{
		rotationInput = Math.Clamp(rotation, -1, 1);
	}

	public float GetRotationVelocity()
	{
		return rotationVelocity;
	}
}

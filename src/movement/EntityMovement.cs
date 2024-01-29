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
	private float cancelRotationBelow = 0.000001f;

	private float rotationSpeedRadian => DegreeToRad(rotationSpeedDegree);

	private Vector2 velocityInput;

	private float rotationInput;

	private float rotationVelocity;

	private float deviation = 0.001f;

	public override void _PhysicsProcess(double delta)
    {
        Velocity += GetVelocityInput();
        rotationVelocity += rotationInput * rotationSpeedRadian;
        rotationVelocity = Math.Abs(rotationVelocity) < cancelRotationBelow ? 0 : rotationVelocity;
        Rotate(rotationVelocity);

        MoveAndSlide();
        if (Math.Abs(rotationVelocity) > maxRotationVelocity)
        {
            rotationVelocity = rotationVelocity < 0 ? -maxRotationVelocity : maxRotationVelocity;
        }

        rotationInput = 0f;
        velocityInput = Vector2.Zero;
    }

    private Vector2 GetVelocityInput()
    {	
        var accelerationMultiplier = velocityInput.X < 0 ? accelerationSpeed.X : accelerationSpeed.Y;
		var velocityToAdd = velocityInput * accelerationMultiplier;
        var targetVelocity = Velocity + velocityToAdd;
		targetVelocity = targetVelocity.Rotated(Rotation);
        if (Math.Abs(targetVelocity.Y) > maxForward + deviation || Math.Abs(targetVelocity.X) > maxStrafe + deviation)
        {
            float newXVelocityInput = velocityInput.X;
            float newYVelocityInput = velocityInput.Y;
            if (Math.Abs(targetVelocity.X) > maxStrafe && targetVelocity.X < Velocity.X)
            {
                newXVelocityInput = 0;
            }
            if (Math.Abs(targetVelocity.Y) > maxForward && targetVelocity.Y < Velocity.Y)
            {
                newYVelocityInput = 0;
            }
            velocityInput = new Vector2(newXVelocityInput, newYVelocityInput);
        }
		return velocityInput.Rotated(Rotation) * accelerationMultiplier;
    }


    private float DegreeToRad(float degree)
	{
		return (float)(degree * (Math.PI / 180));
	}

	public void InputVelocity(Vector2 currentBaseVelocity)
	{
		if (currentBaseVelocity.Y < 0)
		{
			EmitSignal(SignalName.PoweringForward);
		}
		if (currentBaseVelocity.Y >= 0)
		{
			EmitSignal(SignalName.IdleForward);
		}
		velocityInput = currentBaseVelocity.Normalized();
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

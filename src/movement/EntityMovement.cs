using Godot;
using System;

/// <summary>
/// This class does describe the entity movement for each ship in the game
/// </summary>
public partial class EntityMovement : CharacterBody2D
{
	/// <summary>
	/// The ship is currently powering forward
	/// </summary>
	[Signal]
	public delegate void PoweringForwardEventHandler();

	/// <summary>
	/// The ship is currently powering backward
	/// </summary>
	[Signal]
	public delegate void IdleForwardEventHandler();

	/// <summary>
	/// The ship did collide with an entity
	/// </summary>
	/// <param name="entity">A copy if this entity</param>
	[Signal]
	public delegate void EntityDidCollideEventHandler(EntityMovement entity);

	/// <summary>
	/// The maximal allowed forward and backward speed
	/// </summary>
	[ExportGroup("Base Movement")]
	[Export]
	private float maxForward = 250;

	/// <summary>
	/// The maximal allowed strafe speed
	/// </summary>
	[ExportGroup("Base Movement")]
	[Export]
	private float maxStrafe = 150;

	/// <summary>
	/// The speed value used to accelerate or decelerate the ship
	/// </summary>
	[ExportGroup("Base Movement")]
	[Export]
	private Vector2 accelerationSpeed = new Vector2(10, 5);

	/// <summary>
	/// The maximal allowed rotation speed
	/// </summary>
	[ExportGroup("Rotation")]
	[Export]
	private float maxRotationVelocity = 0.2f;

	/// <summary>
	/// The rotation speed in degree
	/// </summary>
	[ExportGroup("Rotation")]
	[Export]
	private float rotationSpeedDegree = 0.2f;
	
	/// <summary>
	/// If the rotation is below this value the rotation will be stopped completely
	/// </summary>
	[ExportGroup("Rotation")]
	[Export]
	private float cancelRotationBelow = 0.000001f;

	/// <summary>
	/// The rotation speed as radian
	/// </summary>
	private float rotationSpeedRadian => DegreeToRad(rotationSpeedDegree);

	/// <summary>
	/// The velocity input by the pilot
	/// </summary>
	private Vector2 velocityInput;

	/// <summary>
	/// The rotation input
	/// </summary>
	private float rotationInput;

	/// <summary>
	/// The current rotation velocity
	/// </summary>
	private float rotationVelocity;

	/// <summary>
	/// Allowed deviation for numbers to prevent float errors
	/// </summary>
	private float deviation = 0.001f;

	public override void _PhysicsProcess(double delta)
    {
        Velocity += GetVelocityInput();
        rotationVelocity += rotationInput * rotationSpeedRadian;
        rotationVelocity = Math.Abs(rotationVelocity) < cancelRotationBelow ? 0 : rotationVelocity;
        Rotate(rotationVelocity);

        if (MoveAndSlide())
		{
			EmitSignal(SignalName.EntityDidCollide, this);
		}
        if (Math.Abs(rotationVelocity) > maxRotationVelocity)
        {
            rotationVelocity = rotationVelocity < 0 ? -maxRotationVelocity : maxRotationVelocity;
        }

        rotationInput = 0f;
        velocityInput = Vector2.Zero;
    }

	/// <summary>
	/// Compute the velocity input based on the current input value
	/// </summary>
	/// <returns>The computed input</returns>
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

	/// <summary>
	/// Convert degree to rad
	/// Note: This could be a math extension method
	/// </summary>
	/// <param name="degree">The degree value to convert</param>
	/// <returns>The radian value of the input degree</returns>
    private float DegreeToRad(float degree)
	{
		return (float)(degree * (Math.PI / 180));
	}

	/// <summary>
	/// Method to change the input velocity
	/// The velocity will be normalized
	/// </summary>
	/// <param name="currentBaseVelocity">The velocity commanded for this ship</param>
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

	/// <summary>
	/// The rotation command for the ship,
	/// the value will be clamped between -1 and 1
	/// </summary>
	/// <param name="rotation">The rotation to command</param>
	public void InputRotation(float rotation)
	{
		rotationInput = Math.Clamp(rotation, -1, 1);
	}

	/// <summary>
	/// Get the current rotation velocity
	/// </summary>
	/// <returns>The rotation velocity as radian</returns>
	public float GetRotationVelocity()
	{
		return rotationVelocity;
	}
}

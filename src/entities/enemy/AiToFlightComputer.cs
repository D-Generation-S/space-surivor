using System;
using Godot;


public partial class AiToFlightComputer : Node
{	
	[Export]
	private EngineComponent engineComponent;

	[Export]
	private FlightComputer flightComputer;

	[Export]
	private EntityMovement entityMovement;

	[Export(PropertyHint.Range, "-360, 360")]
	private float RotationFixInDegree = 0;

	/// <summary>
    /// The allowed difference between target and real rotation, this prevents some osculation
    /// </summary>
    [Export]
    private float rotationDifference = 0.001f;

	private float RotationFixInRadians => RotationFixInDegree.DegreeToRadians();

	private Vector2 globalFlightDirection;

	private float globalTargetRotation;

	private float speed;

    public override void _Ready()
    {
        globalFlightDirection = Vector2.Zero;
		speed = 0;
    }

	public void SetGlobalVelocity(Vector2 velocity)
	{
		globalFlightDirection = velocity.Normalized();
		globalTargetRotation = globalFlightDirection.AngleTo(Vector2.Up) * -1;
	}

	public void SetSpeed(float speed)
	{
		this.speed = Math.Clamp(speed, 0, Math.Abs(engineComponent.GetAccelerationSpeed().Y));
	}

    public override void _Process(double delta)
    {
		SetRotation();
		SpeedUp();
    }

	public EntityMovement GetControlledEntity()
	{
		return entityMovement;
	}

	private void SetRotation()
	{
		var diff = globalTargetRotation - entityMovement.GlobalRotation;
        var absoluteDiff = Math.Abs(diff);
        if (absoluteDiff > FloatExtension.HALF_ROTATION)
        {
            absoluteDiff = FloatExtension.HALF_ROTATION - diff * -1;
            diff *= -1;
        }
        if ((diff < rotationDifference && diff > -rotationDifference) || absoluteDiff < 0.1f)
        {
            return;
        }
        
        var clamped = Math.Clamp(absoluteDiff, 0, 1);
        float direction = diff < 0 ? -1f : 1f;
        if (diff > 1)
        {
			flightComputer.CommandRotation(direction);
            return;
        }
        float targetTurnRate = FloatExtension.Lerp(clamped, 0, 0.04f) * direction;
        float turnDifference = targetTurnRate - entityMovement.GetRotationVelocity();

        float percentage = Math.Clamp(Math.Abs(turnDifference / targetTurnRate), 0, 1);
        direction = turnDifference > 0 ? 1f : -1f;
        
			flightComputer.CommandRotation(percentage.Lerp(0, 1) * direction);
	}

	private void SpeedUp()
	{
		flightComputer.CommandBaseFlightVector(Vector2.Up * speed);
	}
}

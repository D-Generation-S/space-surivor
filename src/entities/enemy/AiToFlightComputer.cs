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

	/// <summary>
	/// The global flight direction to achieve
	/// </summary>
	private Vector2 globalFlightDirectionTarget;

	/// <summary>
	/// The global target rotation to achieve
	/// </summary>
	private float globalTargetRotation;

	/// <summary>
	/// The speed to accelerate
	/// </summary>
	private float speed;

    public override void _Ready()
    {
        globalFlightDirectionTarget = Vector2.Zero;
		speed = 0;
    }

    public override void _PhysicsProcess(double delta)
    {
		SetRotation();
		SpeedUp();
    }

	/// <summary>
	/// Set the global target velocity
	/// this method will set the rotation automatically
	/// </summary>
	/// <param name="velocity">The new global target verlocity</param>
	public void SetGlobalVelocity(Vector2 velocity)
	{
		globalFlightDirectionTarget = velocity.Normalized();
		globalTargetRotation = globalFlightDirectionTarget.AngleTo(Vector2.Up) * -1;
	}

	/// <summary>
	/// Set the speed to accelerate the ship	
	/// </summary>
	/// <param name="speedPercentage">The percentage to accelerate, should be between 0 and 1</param>
	public void SetSpeed(float speedPercentage)
	{
		speed = Math.Clamp(speedPercentage, 0, 1);
	}

	/// <summary>
	/// Get the entity which is controlled by this ai to flight computer instance
	/// </summary>
	/// <returns>The entity movement which is controlled</returns>
	public EntityMovement GetControlledEntity()
	{
		return entityMovement;
	}

	/// <summary>
	/// Rotate the ship to the planned global target rotation
	/// </summary>
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

	/// <summary>
	/// Accelerate the ship forward by the speed percentage given
	/// </summary>
	private void SpeedUp()
	{
		flightComputer.CommandBaseFlightVector(Vector2.Up * speed);
	}
}

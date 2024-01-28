using System.Linq;
using Godot;

public partial class PlayerInputController : Node
{
	[ExportGroup("Base Movement")]
	[Export]
	private ControlConfiguration accelerate;
	
	[ExportGroup("Base Movement")]
	[Export]
	private ControlConfiguration decelerate;

	[ExportGroup("Base Movement")]
	[Export]
	private ControlConfiguration strafeLeft;
	
	[ExportGroup("Base Movement")]
	[Export]
	private ControlConfiguration strafeRight;

	[ExportGroup("Flight Computer")]
	[Export]
	private ControlConfiguration nextFlightComputerMode;

	[ExportGroup("Flight Computer")]
	[Export]
	private ControlConfiguration previousFlightComputerMode;

	[ExportGroup("Rotation Control")]
	[Export]
	private ControlConfiguration rotateLeft;
	
	[ExportGroup("Rotation Control")]
	[Export]
	private ControlConfiguration rotateRight;

	private FlightComputer flightComputer;

    public override void _Ready()
    {
		flightComputer = GetParent().GetChildren().OfType<FlightComputer>().FirstOrDefault();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		Vector2 commandedVelocity = new Vector2(GetStrafing(), GetAccelerationOrDeceleration());
		float rotation = GetRotation();
		flightComputer.CommandBaseFlightVector(commandedVelocity);
		flightComputer.CommandRotation(GetRotation());
		if (rotation == 0)
		{
			flightComputer.NoRotationCommands();
		}
		if (commandedVelocity == Vector2.Zero)
		{
			flightComputer.NoVelocityCommands();
		}

		if (Input.IsActionPressed(nextFlightComputerMode.GetInputName()))
		{
			flightComputer.SwitchToNextModeInterpret();
		}

		if (Input.IsActionPressed(previousFlightComputerMode.GetInputName()))
		{
			flightComputer.SwitchToPreviousModeInterpret();
		}


	}

	private float GetAccelerationOrDeceleration()
	{
		if (Input.IsActionPressed(accelerate.GetInputName()))
		{
			return -Input.GetActionStrength(accelerate.GetInputName());
		}
		if (Input.IsActionPressed(decelerate.GetInputName()))
		{
			return Input.GetActionStrength(decelerate.GetInputName());
		}
		return 0f;
	}

	private float GetStrafing()
	{
		if (Input.IsActionPressed(strafeLeft.GetInputName()))
		{
			return -Input.GetActionStrength(strafeLeft.GetInputName());
		}
		if (Input.IsActionPressed(strafeRight.GetInputName()))
		{
			return Input.GetActionStrength(strafeRight.GetInputName());
		}
		return 0f;
	}

	private float GetRotation()
	{
		if (Input.IsActionPressed(rotateLeft.GetInputName()))
		{
			return -Input.GetActionStrength(rotateLeft.GetInputName());
		}
		if (Input.IsActionPressed(rotateRight.GetInputName()))
		{
			return Input.GetActionStrength(rotateRight.GetInputName());
		}
		return 0f;
	}
}

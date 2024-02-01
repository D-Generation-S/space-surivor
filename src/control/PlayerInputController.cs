using System.Linq;
using Godot;

/// <summary>
/// Class to handle the player Input
/// </summary>
public partial class PlayerInputController : Node
{
    /// <summary>
    /// The control configuration to accelerate the space ship
    /// </summary>
    [ExportGroup("Base Movement")]
    [Export]
    private ControlConfiguration accelerate;

    /// <summary>
    /// The control configuration to decelerate the space ship
    /// </summary>
    [ExportGroup("Base Movement")]
    [Export]
    private ControlConfiguration decelerate;


    /// <summary>
    /// The control configuration to strafe the space ship to the left
    /// </summary>
    [ExportGroup("Base Movement")]
    [Export]
    private ControlConfiguration strafeLeft;


    /// <summary>
    /// The control configuration to strafe the space ship to the right
    /// </summary>
    [ExportGroup("Base Movement")]
    [Export]
    private ControlConfiguration strafeRight;

    /// <summary>
    /// The control configuration to cycle to the next flight computer mode
    /// </summary>
    [ExportGroup("Flight Computer")]
    [Export]
    private ControlConfiguration nextFlightComputerMode;

    /// <summary>
    /// The control configuration to cycle to the previous flight computer mode
    /// </summary>
    [ExportGroup("Flight Computer")]
    [Export]
    private ControlConfiguration previousFlightComputerMode;

    /// <summary>
    /// The control configuration to rotate the ship to the left
    /// </summary>
    [ExportGroup("Rotation Control")]
    [Export]
    private ControlConfiguration rotateLeft;

    /// <summary>
    /// The control configuration to rotate the ship to the right
    /// </summary>
    [ExportGroup("Rotation Control")]
    [Export]
    private ControlConfiguration rotateRight;

    /// <summary>
    /// The flight computer used to interpret the user input
    /// </summary>
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

        if (Input.IsActionJustPressed(nextFlightComputerMode.GetInputName()))
        {
            flightComputer.SwitchToNextModeInterpret();
        }

        if (Input.IsActionJustPressed(previousFlightComputerMode.GetInputName()))
        {
            flightComputer.SwitchToPreviousModeInterpret();
        }
    }

    /// <summary>
    /// The method to define if the ship should accelerate or decelerate
    /// </summary>
    /// <returns>Does return a value between -1 and 1</returns>
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

    /// <summary>
    /// The method does get the strafe direction commanded by the player
    /// </summary>
    /// <returns>The strafe direction, does return a float between -1 and 1</returns>
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

    /// <summary>
    /// Get the commanded rotation for the ship
    /// </summary>
    /// <returns>The rotation, does return a float between -1 and 1</returns>
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

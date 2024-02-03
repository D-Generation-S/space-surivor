using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// This class does represent the flight computer
/// It will use the underlying Flight Command Interpret nodes 
/// to allow controlling the space ship
/// </summary>
public partial class FlightComputer : Node
{
    /// <summary>
    /// The mode for the flight computer has changed
    /// </summary>
    /// <param name="name">The name of the new mode</param>
    [Signal]
    public delegate void FlightModeChangedEventHandler(string name);

    /// <summary>
    /// All the modes available for this flight computer
    /// </summary>
    private List<FlightCommandInterpret> flightCommandModes;

    /// <summary>
    /// The currently active command interpret
    /// </summary>
    private FlightCommandInterpret activeFlightCommandInterpret;

    /// <summary>
    /// The index of the current command interpret, used to cycle them
    /// </summary>
    private int currentCommandInterpretIndex;

    /// <summary>
    /// The object controlled by the flight computer
    /// </summary>
    private EntityMovement controlledEntity;

    /// <summary>
    /// The position of the controlled entity in the last frame
    /// </summary>T
    private Vector2 lastPosition;

    /// <summary>
    /// The local velocity of the controlled entity
    /// </summary>
    private Vector2 localVelocity;

    public override void _Ready()
    {
        controlledEntity = GetParent().GetParent<EntityMovement>();
        flightCommandModes = GetChildren().OfType<FlightCommandInterpret>().ToList();
        activeFlightCommandInterpret = flightCommandModes.FirstOrDefault();
        ComputerSetup();
        currentCommandInterpretIndex = 0;
        lastPosition = controlledEntity.Position;

        EmitSignal(SignalName.FlightModeChanged, GetActiveComputerMode().GetDisplayName());
    }

    public override void _PhysicsProcess(double delta)
    {
        localVelocity = controlledEntity.Position - lastPosition;
        lastPosition = controlledEntity.Position;

        Vector2 forwardFlightDirection = Vector2.Up.Rotated(controlledEntity.Rotation);
        Vector2 strafeLeftDirection = Vector2.Left.Rotated(controlledEntity.Rotation);
        var transformation = new Transform2D(strafeLeftDirection, forwardFlightDirection, Vector2.Zero);
        localVelocity = transformation.BasisXformInv(localVelocity) * -1;

        base._PhysicsProcess(delta);
    }

    /// <summary>
    /// Setup the command flight interpret after switching it
    /// </summary>
    public void ComputerSetup()
    {
        activeFlightCommandInterpret.SetupInterpret(controlledEntity.Velocity, controlledEntity.GetRotationVelocity(), GetEntityRotation());
    }

    /// <summary>
    /// Get the rotation of the entity
    /// </summary>
    /// <returns>The rotation changed to be always positive</returns>
    private float GetEntityRotation()
    {
        float rotation = controlledEntity.Rotation;
        rotation = rotation < 0 ? (float)(Math.PI * 2 + rotation) : rotation;
        return rotation;
    }

    /// <summary>
    /// Command to trigger if no rotation command is send by the pilot
    /// </summary>
    public void NoRotationCommands()
    {
        float computedRotation = activeFlightCommandInterpret.IdleBaseRotation(localVelocity, controlledEntity.GetRotationVelocity(), GetEntityRotation());
        controlledEntity.InputRotation(computedRotation);
    }

    /// <summary>
    /// Command to trigger if not course change is send by the pilot
    /// </summary>
    public void NoVelocityCommands()
    {
        Vector2 computedVelocity = activeFlightCommandInterpret.IdleBaseVelocity(localVelocity,
                                                                                 controlledEntity.GetRotationVelocity(),
                                                                                 GetEntityRotation());
        controlledEntity.InputVelocity(computedVelocity);
    }

    /// <summary>
    /// Method used to interpret the commanded velocity and parse it to the controlled entity
    /// </summary>
    /// <param name="commandedVelocity">The commanded velocity</param>
    public void CommandBaseFlightVector(Vector2 commandedVelocity)
    {
        Vector2 computedVector = activeFlightCommandInterpret.InterpretBaseVelocity(commandedVelocity,
                                                                                    localVelocity,
                                                                                    controlledEntity.GetRotationVelocity(),
                                                                                    GetEntityRotation());
        controlledEntity.InputVelocity(computedVector);
    }

    /// <summary>
    /// Method used to interpret the commanded rotation and parse it to the controlled entity
    /// </summary>
    /// <param name="commandedRotation">The rotation commanded by the pilot</param>
    public void CommandRotation(float commandedRotation)
    {
        float computedRotation = activeFlightCommandInterpret.InterpretRotation(commandedRotation,
                                                                                localVelocity,
                                                                                controlledEntity.GetRotationVelocity(),
                                                                                GetEntityRotation());
        controlledEntity.InputRotation(computedRotation);
    }

    /// <summary>
    /// Switch to the next command interpret
    /// </summary>
    public void SwitchToNextModeInterpret()
    {   
        currentCommandInterpretIndex++;
        currentCommandInterpretIndex = currentCommandInterpretIndex > flightCommandModes.Count - 1? 0 : currentCommandInterpretIndex;
        activeFlightCommandInterpret = flightCommandModes[currentCommandInterpretIndex];
        ComputerSetup();

        EmitSignal(SignalName.FlightModeChanged, GetActiveComputerMode().GetDisplayName());
    }


    /// <summary>
    /// Switch to the previous command interpret
    /// </summary>
    public void SwitchToPreviousModeInterpret()
    {
        currentCommandInterpretIndex--;
        currentCommandInterpretIndex = currentCommandInterpretIndex < 0? flightCommandModes.Count - 1 : currentCommandInterpretIndex;
        activeFlightCommandInterpret = flightCommandModes[currentCommandInterpretIndex];
        ComputerSetup();
        
        EmitSignal(SignalName.FlightModeChanged, GetActiveComputerMode().GetDisplayName());
    }

    /// <summary>
    /// Get all the computer modes available for this flight computer
    /// </summary>
    /// <returns>A list with computer modes</returns>
    public List<FlightCommandInterpret> GetComputerModes()
    {
        return flightCommandModes;
    }

    /// <summary>
    /// Get the currently active computer mode
    /// </summary>
    /// <returns>The active flight command interpret</returns>
    public FlightCommandInterpret GetActiveComputerMode()
    {
        return activeFlightCommandInterpret;
    }
}
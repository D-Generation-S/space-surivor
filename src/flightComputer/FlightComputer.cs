using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class FlightComputer : Node
{
    
	[Signal]
	public delegate void FlightModeChangedEventHandler(string name);

    private List<FlightCommandInterpret> flightCommandModes;

    private FlightCommandInterpret activeFlightCommandInterpret;

    private int currentCommandInterpretIndex;

    private EntityMovement entityMovement;

    private Vector2 lastPosition;

    private Vector2 localVelocity;

    public override void _Ready()
    {
        entityMovement = GetParent().GetParent<EntityMovement>();
        flightCommandModes = GetChildren().OfType<FlightCommandInterpret>().ToList();
        activeFlightCommandInterpret = flightCommandModes.FirstOrDefault();
        ComputerSetup();
        currentCommandInterpretIndex = 0;
        lastPosition = entityMovement.Position;

        EmitSignal(SignalName.FlightModeChanged, GetActiveComputerMode().GetDisplayName());
    }

    public override void _PhysicsProcess(double delta)
    {
        localVelocity = entityMovement.Position - lastPosition;
        lastPosition = entityMovement.Position;

        Vector2 forwardFlightDirection = Vector2.Up.Rotated(entityMovement.Rotation);
        Vector2 strafeLeftDirection = Vector2.Left.Rotated(entityMovement.Rotation);
        var transformation = new Transform2D(strafeLeftDirection, forwardFlightDirection, Vector2.Zero);
        localVelocity = transformation.BasisXformInv(localVelocity).Normalized() * -1;

        base._PhysicsProcess(delta);
    }

    public void ComputerSetup()
    {
        activeFlightCommandInterpret.SetupInterpret(entityMovement.Velocity, entityMovement.GetRotationVelocity(), GetEntityRotation());
    }

    private float GetEntityRotation()
    {
        float rotation = entityMovement.Rotation;
        rotation = rotation < 0 ? (float)(Math.PI * 2 + rotation) : rotation;
        return rotation;
    }

    public void NoRotationCommands()
    {
        float computedRotation = activeFlightCommandInterpret.IdleBaseRotation(localVelocity, entityMovement.GetRotationVelocity(), GetEntityRotation());
        entityMovement.InputRotation(computedRotation);
    }

    public void NoVelocityCommands()
    {
        Vector2 computedVelocity = activeFlightCommandInterpret.IdleBaseVelocity(localVelocity,
                                                                                 entityMovement.GetRotationVelocity(),
                                                                                 GetEntityRotation());
        entityMovement.InputVelocity(computedVelocity);
    }

    public void CommandBaseFlightVector(Vector2 commandedVelocity)
    {
        Vector2 computedVector = activeFlightCommandInterpret.InterpretBaseVelocity(commandedVelocity,
                                                                                    localVelocity,
                                                                                    entityMovement.GetRotationVelocity(),
                                                                                    GetEntityRotation());
        entityMovement.InputVelocity(computedVector);
    }

    public void CommandRotation(float commandedRotation)
    {
        float computedRotation = activeFlightCommandInterpret.InterpretRotation(commandedRotation,
                                                                                localVelocity,
                                                                                entityMovement.GetRotationVelocity(),
                                                                                GetEntityRotation());
        entityMovement.InputRotation(computedRotation);
    }

	public void SwitchToNextModeInterpret()
	{   
        currentCommandInterpretIndex++;
        currentCommandInterpretIndex = currentCommandInterpretIndex > flightCommandModes.Count - 1? 0 : currentCommandInterpretIndex;
        activeFlightCommandInterpret = flightCommandModes[currentCommandInterpretIndex];
        ComputerSetup();

        EmitSignal(SignalName.FlightModeChanged, GetActiveComputerMode().GetDisplayName());
	}

	public void SwitchToPreviousModeInterpret()
	{
        currentCommandInterpretIndex--;
        currentCommandInterpretIndex = currentCommandInterpretIndex < 0? flightCommandModes.Count - 1 : currentCommandInterpretIndex;
        activeFlightCommandInterpret = flightCommandModes[currentCommandInterpretIndex];
        ComputerSetup();
        
		EmitSignal(SignalName.FlightModeChanged, GetActiveComputerMode().GetDisplayName());
	}

    public List<FlightCommandInterpret> GetComputerModes()
    {
        return flightCommandModes;
    }

    public FlightCommandInterpret GetActiveComputerMode()
    {
        return activeFlightCommandInterpret;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class FlightComputer : Node
{
    private List<FlightCommandInterpret> flightCommandModes;

    private FlightCommandInterpret activeFlightCommandInterpret;

    private int currentCommandInterpretIndex;

    private EntityMovement entityMovement;

    public override void _Ready()
    {
        entityMovement = GetParent().GetParent<EntityMovement>();
        flightCommandModes = GetChildren().OfType<FlightCommandInterpret>().ToList();
        activeFlightCommandInterpret = flightCommandModes.FirstOrDefault();
        ComputerSetup();
        currentCommandInterpretIndex = 0;
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
        float computedRotation = activeFlightCommandInterpret.IdleBaseRotation(entityMovement.Velocity, entityMovement.GetRotationVelocity(), GetEntityRotation());
        entityMovement.InputRotation(computedRotation);
    }

    public void NoVelocityCommands()
    {
        Vector2 computedVelocity = activeFlightCommandInterpret.IdleBaseVelocity(entityMovement.Velocity,
                                                                                 entityMovement.GetRotationVelocity(),
                                                                                 GetEntityRotation());
        entityMovement.InputVelocity(computedVelocity);
    }

    public void CommandBaseFlightVector(Vector2 commandedVelocity)
    {
        Vector2 computedVector = activeFlightCommandInterpret.InterpretBaseVelocity(commandedVelocity,
                                                                                    entityMovement.Velocity,
                                                                                    entityMovement.GetRotationVelocity(),
                                                                                    GetEntityRotation());
        entityMovement.InputVelocity(computedVector);
    }

    public void CommandRotation(float commandedRotation)
    {
        float computedRotation = activeFlightCommandInterpret.InterpretRotation(commandedRotation,
                                                                                entityMovement.Velocity,
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
	}

	public void SwitchToPreviousModeInterpret()
	{
        currentCommandInterpretIndex--;
        currentCommandInterpretIndex = currentCommandInterpretIndex < 0? flightCommandModes.Count - 1 : currentCommandInterpretIndex;
        activeFlightCommandInterpret = flightCommandModes[currentCommandInterpretIndex];
        ComputerSetup();
	}

    public List<FlightCommandInterpret> GetComputerModes()
    {
        return flightCommandModes;
    }
}
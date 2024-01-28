using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;

public partial class FlightComputer : Node
{
    private List<FlightCommandInterpret> flightCommandModes;

    private FlightCommandInterpret activeFlightCommandInterpret;

    private EntityMovement entityMovement;

    public override void _Ready()
    {
        entityMovement = GetParent().GetParent<EntityMovement>();
        flightCommandModes = GetChildren().OfType<FlightCommandInterpret>().ToList();
        activeFlightCommandInterpret = flightCommandModes.FirstOrDefault();
    }

    public void NoRotationCommands()
    {
        float computedRotation = activeFlightCommandInterpret.IdleBaseRotation(entityMovement.Velocity, entityMovement.GetRotationVelocity());
        entityMovement.InputRotation(computedRotation);
    }

    public void NoVelocityCommands()
    {
        Vector2 computedVelocity = activeFlightCommandInterpret.IdleBaseVelocity(entityMovement.Velocity, entityMovement.GetRotationVelocity());
        entityMovement.InputVelocity(computedVelocity);
    }

    public void CommandBaseFlightVector(Vector2 commandedVelocity)
    {
        Vector2 computedVector = activeFlightCommandInterpret.InterpretBaseVelocity(commandedVelocity, entityMovement.Velocity, entityMovement.GetRotationVelocity());
        entityMovement.InputVelocity(computedVector);
    }

    public void CommandRotation(float commandedRotation)
    {
        float computedRotation = activeFlightCommandInterpret.InterpretRotation(commandedRotation, entityMovement.Velocity, entityMovement.GetRotationVelocity());
        entityMovement.InputRotation(computedRotation);
    }

    public List<FlightCommandInterpret> GetComputerModes()
    {
        return flightCommandModes;
    }
}
using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

public partial class PlayerBasedUi : Label
{
	[Export]
	private EntityMovement player;

	[Export]
	private string componentNodeName = "Components";	

	[Export]
	private string prefix = "Flight Computer Mode: ";

	private FlightComputer playerFlightComputer;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var components = player.GetChildren().OfType<Node>().Where(node => node.Name == "Components").FirstOrDefault();
		if (components is null)
		{
			GD.PushError($"No components found on player {player.Name}");
			return;
		}

		playerFlightComputer = components.GetChildren().OfType<FlightComputer>().FirstOrDefault();
		if ( playerFlightComputer is null)
		{
			GD.PushError($"No flight computer found for player {player.Name}");
		}

		playerFlightComputer.FlightModeChanged += OnFlightModeChanged;
		OnFlightModeChanged(playerFlightComputer.GetActiveComputerMode().GetDisplayName());
	}

    private void OnFlightModeChanged(string name)
    {
        Text = $"{prefix}{name}";
    }

}

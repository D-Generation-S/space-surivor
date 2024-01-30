using Godot;
using System.Linq;

/// <summary>
/// Method to show the player ur
/// </summary>
public partial class PlayerBasedUi : Label
{
	/// <summary>
	/// reference to the player
	/// </summary>
	[Export]
	private EntityMovement player;

	/// <summary>
	/// Name of the component node on the player object
	/// </summary>
	[Export]
	private string componentNodeName = "Components";	

	/// <summary>
	/// String prefix for the current computer mode
	/// </summary>
	[Export]
	private string prefix = "Flight Computer Mode: ";

	/// <summary>
	/// Reference to the player flight computer
	/// </summary>
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

	/// <summary>
	/// Method to inform script that the flight mode has changed
	/// </summary>
	/// <param name="name">The name of the new flight mode</param>
    private void OnFlightModeChanged(string name)
    {
        Text = $"{prefix}{name}";
    }

}

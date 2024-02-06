using Godot;

/// <summary>
/// Script to check if the player is still alive
/// </summary>
public partial class CheckForPlayer : Control
{
	/// <summary>
	/// Signal to fire if the player is gone
	/// </summary>
	[Signal]
	public delegate void PlayerIsGoneEventHandler();

	/// <summary>
	/// The player to watch out
	/// </summary>
	[Export]
	private EntityMovement player;

    public override void _Ready()
    {
        player.TreeExiting += () => 
		{
			EmitSignal(SignalName.PlayerIsGone);
		};
    }
}

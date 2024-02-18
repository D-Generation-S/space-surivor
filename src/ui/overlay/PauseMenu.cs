using Godot;

/// <summary>
/// Class to control the pause menu
/// </summary>
public partial class PauseMenu : CanvasLayer
{
	/// <summary>
	/// The button to focus on if control is shown again
	/// </summary>
	[Export]
	private Button focusButton;

	/// <summary>
	/// Method to display the pause menu
	/// </summary>
	public void ShowMenu()
	{
		Visible = true;
		GetTree().Paused = true;
		if (focusButton is not null)
		{
			focusButton.GrabFocus();
		}
	}

	/// <summary>
	/// Method to hide the pause menu
	/// </summary>
	public void HideMenu()
	{
		Visible = false;
		GetTree().Paused = false;
	}
}

using Godot;

/// <summary>
/// Class to control the pause menu
/// </summary>
public partial class PauseMenu : CanvasLayer
{
	/// <summary>
	/// Method to display the pause menu
	/// </summary>
	public void ShowMenu()
	{
		Visible = true;
		GetTree().Paused = true;
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

using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{
	public void ShowMenu()
	{
		Visible = true;
		GetTree().Paused = true;
	}

	public void HideMenu()
	{
		Visible = false;
		GetTree().Paused = false;
	}
}

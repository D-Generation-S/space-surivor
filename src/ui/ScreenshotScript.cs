using Godot;
using System;

/// <summary>
/// Method to take screenshots of the scene
/// </summary>
public partial class ScreenshotScript : Node2D
{
	/// <summary>
	/// The screenshot configuration to use
	/// </summary>
	[Export]
	private ControlConfiguration screenshotConfiguration;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed(screenshotConfiguration.GetInputName()))
		{
			var capture = GetViewport().GetTexture().GetImage();
			var time = DateTime.Now.ToString("yyyyMMdd_HHmmss");
			var access = DirAccess.Open("user://");
			if (!access.DirExists("screenshots"))
			{
				access.MakeDir("screenshots");
			}
			var fileName = $"user://screenshots/{time}-screenshot.png";

			capture.SavePng(fileName);
		}
	}
}

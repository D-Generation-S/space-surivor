using Godot;
using System;

public partial class HelpPanel : Control
{
	[Signal]
	public delegate void ShowNextEventHandler();

	[Signal]
	public delegate void ShowPreviousEventHandler();

	[Signal]
	public delegate void CloseEventHandler();

	[Export]
	private AutoAnimate animationPlayer;

	public void ShowNextHelpPanel()
	{
		EmitSignal(SignalName.ShowNext);
	}

	public void ShowPreviousHelpPanel()
	{
		EmitSignal(SignalName.ShowPrevious);
	}

	public void CloseHelpPanel()
	{
		EmitSignal(SignalName.Close);
	}

	public void ShowingPanel()
	{
		SetDeferred("visible", true);
		animationPlayer?.ReplayAnimation();
	}

	public void HidingPanel()
	{
		SetDeferred("visible", false);
		animationPlayer?.ReplayAnimation();
	}
}

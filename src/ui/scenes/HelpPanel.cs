using Godot;

/// <summary>
/// This class does control a single help panel.
/// It mainly does forward events to be catched from the help scene
/// </summary>
public partial class HelpPanel : Control
{
	/// <summary>
	/// Show the next help panel
	/// </summary>
	[Signal]
	public delegate void ShowNextEventHandler();

	/// <summary>
	/// Show the previous help panel
	/// </summary>
	[Signal]
	public delegate void ShowPreviousEventHandler();

	/// <summary>
	/// Close the help panels and reset them
	/// </summary>
	[Signal]
	public delegate void CloseEventHandler();

	/// <summary>
	/// The animation player of the help panel
	/// </summary>
	[Export]
	private AutoAnimate animationPlayer;

	/// <summary>
	/// Method to trigger showing the next help panel
	/// </summary>
	public void ShowNextHelpPanel()
	{
		EmitSignal(SignalName.ShowNext);
	}

	/// <summary>
	/// Method to trigger showing the previous help panel
	/// </summary>
	public void ShowPreviousHelpPanel()
	{
		EmitSignal(SignalName.ShowPrevious);
	}

	/// <summary>
	/// Close the help panels
	/// </summary>
	public void CloseHelpPanel()
	{
		EmitSignal(SignalName.Close);
	}

	/// <summary>
	/// Show this panel
	/// </summary>
	public void ShowingPanel()
	{
		SetDeferred("visible", true);
		animationPlayer?.ReplayAnimation();
	}

	/// <summary>
	/// Hide this panel
	/// </summary>
	public void HidingPanel()
	{
		SetDeferred("visible", false);
		animationPlayer?.ReplayAnimation();
	}
}

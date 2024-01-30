using Godot;

/// <summary>
/// Script to display the help screen
/// It will cycle the screen entries
/// </summary>
public partial class DisplayHelpScript : Control
{
	/// <summary>
	/// All the help screens available
	/// </summary>
	[Export]
	private HelpPanel[] helpScenes;

	/// <summary>
	/// The current index
	/// </summary>
	private int index;

	/// <summary>
	/// The currently shown window
	/// </summary>
	private int currentlyShownWindow;

    public override void _Ready()
    {
		index = -1;
		currentlyShownWindow = 0;
		MouseFilter = MouseFilterEnum.Ignore;
    }

	/// <summary>
	/// Hide the previous help text and show the next one
	/// </summary>
    public void NextHelpText()
	{		
		helpScenes[currentlyShownWindow].HidingPanel();
		index++;
		index = index >= helpScenes.Length ? helpScenes.Length - 1 : index;

		helpScenes[index].ShowingPanel();
		currentlyShownWindow = index;
	}

	/// <summary>
	/// Hide the previous help text and show the one before one
	/// </summary>
	public void PreviousHelpText()
	{
		helpScenes[currentlyShownWindow].HidingPanel();
		index--;
		if (index < 0)
		{
			GD.PushError("Trying to show window with negative index!");
			return;
		}
		helpScenes[index].ShowingPanel();
		currentlyShownWindow = index;
	}

	/// <summary>
	/// Close the help panels and reset the index
	/// </summary>
	public void CloseHelpPanels()
	{
		helpScenes[currentlyShownWindow].HidingPanel();
		index = -1;
	}
}

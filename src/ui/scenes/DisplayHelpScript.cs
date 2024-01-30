using Godot;
using System;
using System.Linq;
using System.Runtime.InteropServices;

public partial class DisplayHelpScript : Control
{
	[Export]
	private HelpPanel[] helpScenes;

	private bool firstShown = false;

	private int index;

	private int previousIndex;

	private HelpPanel firstHelpScene;

    public override void _Ready()
    {
        firstHelpScene = helpScenes.FirstOrDefault();
		index = 0;
		previousIndex = 0;
		MouseFilter = MouseFilterEnum.Ignore;
    }

    public void NextHelpText()
	{
		if (!firstShown)
		{
			firstHelpScene.ShowingPanel();
			firstShown = true;
			index++;
			index = index >= helpScenes.Length ? 0 : index;

			return;
		}
		

		helpScenes[previousIndex].HidingPanel();
		previousIndex = index;
		helpScenes[index].ShowingPanel();
		index++;
		index = index >= helpScenes.Length ? 0 : index;
	}

	public void PreviousHelpText()
	{
		helpScenes[previousIndex].HidingPanel();
		previousIndex = index;
		helpScenes[index].ShowingPanel();
		index--;
		index = index < 0 ? helpScenes.Length - 1 : index;
	}

	public void CloseHelpPanels()
	{
		helpScenes[previousIndex].HidingPanel();
		index = 0;
	}
}

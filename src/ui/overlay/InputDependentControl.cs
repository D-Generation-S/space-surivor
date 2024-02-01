using Godot;

/// <summary>
/// Class to switch the visibility of a control based on the input device
/// </summary>

public partial class InputDependentControl : Control
{
	/// <summary>
	/// All the controls which are used for keyboards
	/// </summary>
	[Export]
	private Control[] keyBoardControls;

	/// <summary>
	/// All the controls which are used for controllers
	/// </summary>
	[Export]
	private Control[] controllerControls;

    public override void _Process(double delta)
    {
		if (!Visible)
		{
			return;
		}
		
		SwitchControlVisibility(SessionUserSettings.Instance.InputDevice);
        base._Process(delta);
    }

	/// <summary>
	/// Switch the visibility of the controls based on the input device
	/// </summary>
	/// <param name="inputDevice">The input device to use</param>
    public void SwitchControlVisibility(InputDevice inputDevice)
	{
		foreach (var keyboardControl in keyBoardControls)
		{
			keyboardControl.SetDeferred("visible", inputDevice == InputDevice.Keyboard);	
		}
		foreach (var controllerControl in controllerControls)
		{
			controllerControl.SetDeferred("visible", inputDevice == InputDevice.Controller);	
		}
	}

}

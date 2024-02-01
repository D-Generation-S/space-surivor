using Godot;
using System.Linq;

public partial class InputDependentControl : Control
{
	[Export]
	private Control[] keyBoardControls;

	[Export]
	private Control[] controllerControls;

	private InputDevice inputDevice;

	private bool firstTick;


    public override void _Process(double delta)
    {
		if (!Visible)
		{
			return;
		}
		
		SwitchControlVisibility(SessionUserSettings.Instance.InputDevice);
        base._Process(delta);
    }

    public void SetInputDevice()
	{

	}

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

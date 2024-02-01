using Godot;

/// <summary>
/// Class to detect the last used input device
/// </summary>
public partial class InputDeviceControlledControl : Node
{
    /// <summary>
    /// Event if the input device was changed
    /// </summary>
	[Signal]
	public delegate void InputChangedEventHandler();

    public override void _Input(InputEvent inputEvent)
    {
		SessionUserSettings.Instance.InputDevice = inputEvent is InputEventJoypadButton || inputEvent is InputEventJoypadMotion ? InputDevice.Controller : InputDevice.Keyboard;
		EmitSignal(SignalName.InputChanged);
        base._Input(inputEvent);
    }
}

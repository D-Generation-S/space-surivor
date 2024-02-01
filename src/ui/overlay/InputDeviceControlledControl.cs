using Godot;
using System;
using System.Diagnostics;

public partial class InputDeviceControlledControl : Node
{
	[Signal]
	public delegate void InputChangedEventHandler();

    public override void _Input(InputEvent inputEvent)
    {
		SessionUserSettings.Instance.InputDevice = inputEvent is InputEventJoypadButton || inputEvent is InputEventJoypadMotion ? InputDevice.Controller : InputDevice.Keyboard;
		EmitSignal(SignalName.InputChanged);
        base._Input(inputEvent);
    }
}

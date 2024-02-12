using Godot;


/// <summary>
/// Singelton class to hold the user settings for the current session
/// This contains information like the used input device
/// </summary>
public partial class SessionUserSettings : Node
{
    /// <summary>
    /// The currently used input device
    /// </summary>
    public InputDevice InputDevice;

    /// <summary>
    /// Private constructor to prevent additional instance creation
    /// </summary>
    private SessionUserSettings()
    {
        InputDevice = InputDevice.Unknown;
    }
}

/// <summary>
/// The possible input devices
/// </summary>
public enum InputDevice
{
    Unknown,
    Keyboard,
    Controller
}
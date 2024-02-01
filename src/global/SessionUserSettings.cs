/// <summary>
/// Singelton class to hold the user settings for the current session
/// This contains information like the used input device
/// </summary>
public class SessionUserSettings
{

    /// <summary>
    /// The private singelton instance of this class
    /// </summary>
    private static SessionUserSettings instance;

    /// <summary>
    /// Get the singelton instance
    /// </summary>
    public static SessionUserSettings Instance => instance ?? (instance = new SessionUserSettings());

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
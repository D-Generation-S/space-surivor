using Godot;


/// <summary>
/// Global godot autoload class to hold the user settings for the current session
/// This contains information like the used input device
/// </summary>
public partial class SessionUserSettings : Node
{
    /// <summary>
    /// The currently used input device
    /// </summary>
    public InputDevice InputDevice;

    private GameSessionData gameSessionData;

    /// <summary>
    /// Private constructor to prevent additional instance creation
    /// </summary>
    private SessionUserSettings()
    {
        InputDevice = InputDevice.Unknown;
    }

    public void StartGameSession()
    {
        gameSessionData = new GameSessionData();
    }

    public GameSessionData GetGameSessionData()
    {
        gameSessionData ??= new GameSessionData();
        return gameSessionData;
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
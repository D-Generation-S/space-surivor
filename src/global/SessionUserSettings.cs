using System;

public class SessionUserSettings
{

    private static SessionUserSettings instance;
    public static SessionUserSettings Instance => instance ?? (instance = new SessionUserSettings());

    public InputDevice InputDevice;

    private SessionUserSettings()
    {
        InputDevice = InputDevice.Unknown;
    }
}

[Flags]
public enum InputDevice
{
    Unknown,
    Keyboard,
    Controller
}
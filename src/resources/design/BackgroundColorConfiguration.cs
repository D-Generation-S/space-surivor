using Godot;

/// <summary>
/// Class to define a background color configuration
/// </summary>
[GlobalClass]
public partial class BackgroundColorConfiguration : Resource
{
    /// <summary>
    /// The first cloud layer color
    /// </summary>
    [Export]
    private Color cloudColor1 = new Color(0.41f, 0.64f, 0.78f, 0.4f);
    
    
    /// <summary>
    /// The second cloud layer color
    /// </summary>
    [Export]
    private Color cloudColor2 = new Color(0.99f, 0.79f, 0.46f, 0.2f);

    
    /// <summary>
    /// The third cloud layer color
    /// </summary>
    [Export]
    private Color cloudColor3 = new Color(0.81f, 0.31f, 0.59f, 1.0f);

    
    /// <summary>
    /// The fourth cloud layer color
    /// </summary>
    [Export]
    private Color cloudColor4 = new Color(0.27f, 0.15f, 0.33f, 1.0f);

    /// <summary>
    /// The background space layer color
    /// </summary>
    [Export]
    private Color spaceColor = new Color(0.09f, 0.06f, 0.28f, 0.3f);

    /// <summary>
    /// Get the first cloud level color
    /// </summary>
    /// <returns>The first cloud level color</returns>
    public Color GetCloudColor1()
    {
        return cloudColor1;
    }

    /// <summary>
    /// Get the second cloud level color
    /// </summary>
    /// <returns>The second cloud level color</returns>
    public Color GetCloudColor2()
    {
        return cloudColor2;
    }

    /// <summary>
    /// Get the third cloud level color
    /// </summary>
    /// <returns>The third cloud level color</returns>
    public Color GetCloudColor3()
    {
        return cloudColor3;
    }

    /// <summary>
    /// Get the fourth cloud level color
    /// </summary>
    /// <returns>The fourth cloud level color</returns>
    public Color GetCloudColor4()
    {
        return cloudColor4;
    }

    /// <summary>
    /// Get the background color
    /// </summary>
    /// <returns>The background color</returns>
    public Color GetSpaceColor()
    {
        return spaceColor;
    }
}

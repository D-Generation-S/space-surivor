using Godot;

/// <summary>
/// Class to hold the configuration for a background shader
/// </summary>
[GlobalClass]
public partial class BackgroundConfiguration : Resource
{
    /// <summary>
    /// All possible color configurations for this background configuration
    /// </summary>
    [Export]
    private BackgroundColorConfiguration[] backgroundColorConfiguration;

    /// <summary>
    /// The minimum zoom scale for this background configuration
    /// </summary>
    [Export(PropertyHint.Range, "5.0,20.0")]
    private float minZoomScale = 10;

    /// <summary>
    /// The maximum zoom scale for this background configuration
    /// </summary>
    [Export(PropertyHint.Range, "5.0,20.0")]
    private float maxZoomScale = 13;

    /// <summary>
    /// Method to get the min zoom level for this configuration
    /// </summary>
    /// <returns>The min zoom level</returns>
    public float GetMinZoomScale()
    {
        return minZoomScale;
    }

    /// <summary>
    /// Method to get the max zoom scale level for this configuration
    /// </summary>
    /// <returns>The max zoom scale</returns>
    public float GetMaxZoomScale()
    {
        return maxZoomScale <= minZoomScale ? minZoomScale + 1 : minZoomScale;
    }

    /// <summary>
    /// Get the background color configurations for this configuration
    /// </summary>
    /// <returns>Get all the background color configurations</returns>
    public BackgroundColorConfiguration[] GetBackgroundColorConfigurations()
    {
        return backgroundColorConfiguration;
    }
}

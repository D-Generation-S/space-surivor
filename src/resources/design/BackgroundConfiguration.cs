using Godot;

[GlobalClass]
public partial class BackgroundConfiguration : Resource
{
    [Export]
    private BackgroundColorConfiguration[] backgroundColorConfiguration;

    [Export(PropertyHint.Range, "5.0,20.0")]
    private float minZoomScale = 10;

    [Export(PropertyHint.Range, "5.0,20.0")]
    private float maxZoomScale = 13;

    public float GetMinZoomScale()
    {
        return minZoomScale;
    }

    public float GetMaxZoomScale()
    {
        return maxZoomScale;
    }

    public BackgroundColorConfiguration[] GetBackgroundColorConfigurations()
    {
        return backgroundColorConfiguration;
    }
}

using System.Reflection.Metadata.Ecma335;
using Godot;

[GlobalClass]
public partial class BackgroundColorConfiguration : Resource
{
    [Export]
    private Color cloudColor1 = new Color(0.41f, 0.64f, 0.78f, 0.4f);
    
    [Export]
    private Color cloudColor2 = new Color(0.99f, 0.79f, 0.46f, 0.2f);

    [Export]
    private Color cloudColor3 = new Color(0.81f, 0.31f, 0.59f, 1.0f);

    [Export]
    private Color cloudColor4 = new Color(0.27f, 0.15f, 0.33f, 1.0f);

    
    [Export]
    private Color spaceColor = new Color(0.09f, 0.06f, 0.28f, 0.3f);

    public Color GetCloudColor1()
    {
        return cloudColor1;
    }

    public Color GetCloudColor2()
    {
        return cloudColor2;
    }

    public Color GetCloudColor3()
    {
        return cloudColor3;
    }

    public Color GetCloudColor4()
    {
        return cloudColor4;
    }

    public Color GetSpaceColor()
    {
        return spaceColor;
    }
}

using Godot;
using System;

public partial class BackgroundControl : Sprite2D
{
    [Export]
    private Node2D bindTarget;

    [Export]
    private BackgroundConfiguration backgroundConfiguration;

    private BackgroundColorConfiguration colorConfiguration;

    private ShaderMaterial shaderMaterial;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        int selection = Random.Shared.Next(0, backgroundConfiguration.GetBackgroundColorConfigurations().Length - 1);
        colorConfiguration = backgroundConfiguration.GetBackgroundColorConfigurations()[selection];
        float zoomLevel = (float)Math.Floor(Random.Shared.NextDouble() * (backgroundConfiguration.GetMaxZoomScale() - backgroundConfiguration.GetMinZoomScale() + 1) + backgroundConfiguration.GetMinZoomScale());
        shaderMaterial = Material as ShaderMaterial;

        shaderMaterial.SetShaderParameter("CLOUD1_COL", colorConfiguration.GetCloudColor1());
        shaderMaterial.SetShaderParameter("CLOUD2_COL", colorConfiguration.GetCloudColor2());
        shaderMaterial.SetShaderParameter("CLOUD3_COL", colorConfiguration.GetCloudColor3());
        shaderMaterial.SetShaderParameter("CLOUD4_COL", colorConfiguration.GetCloudColor4());
        shaderMaterial.SetShaderParameter("SPACE", colorConfiguration.GetSpaceColor());
        shaderMaterial.SetShaderParameter("zoomScale", zoomLevel);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (bindTarget == null)
        {
            return;
        }
        GlobalPosition = bindTarget.GlobalPosition;
    }
}

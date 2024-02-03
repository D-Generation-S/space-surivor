using Godot;
using System;
using System.Linq;

/// <summary>
/// Pointer which will always point to a given target
/// </summary>
public partial class TargetPointer : Sprite2D
{
    /// <summary>
    /// The view port size to shrink on each side
    /// </summary>
    [Export]
    private int viewportBoundLimiter = 40;

    /// <summary>
    /// Additional rotation of the pointer sprite, if required
    /// </summary>
    [Export]
    private float additionalRotation = 0;

    /// <summary>
    /// The rotator for the distance
    /// </summary>
    private Node2D textRotator;

    /// <summary>
    /// The distance to the target
    /// </summary>
    private Label distance;

    /// <summary>
    /// The player object
    /// </summary>
    private EntityMovement player;
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        textRotator = GetChildren().OfType<Node2D>()
                                   .FirstOrDefault();
        distance = textRotator.GetChildren()
                              .OfType<Label>()
                              .FirstOrDefault();
        player = GetTree().Root.GetChildren()
                               .FirstOrDefault()
                               .GetChildren()
                               .OfType<EntityMovement>()
                               .Where(node => node.IsInGroup("player"))
                               .FirstOrDefault();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {	
        var cameraGlobalPosition = GetViewport().GetCamera2D().GlobalPosition;
        var viewport = GetViewportRect();
        viewport.Size = new Vector2(viewport.Size.X - viewportBoundLimiter * 2, viewport.Size.Y - viewportBoundLimiter * 2);
        var viewportXPosition  = cameraGlobalPosition.X - viewport.Size.X / 2;
        var viewportYPosition  = cameraGlobalPosition.Y - viewport.Size.Y / 2;
        viewport.Position = new Vector2(viewportXPosition, viewportYPosition);
        SetMarkerPosition(viewport);
        if (viewport.HasPoint(GetParent<Node2D>().GlobalPosition))	
        {
            SetDeferred("visible", false);
            distance.SetDeferred("visible", false);
            return;
        }		
        if (!Visible)
        {
            SetDeferred("visible", true);
            distance.SetDeferred("visible", true);
        }
        
        var targetPosition = GetParent<Node2D>().GlobalPosition;
        var distanceValue = (int)Math.Floor(targetPosition.DistanceTo(player.GlobalPosition));
        distance.Text = distanceValue.ToString();
        LookAt(targetPosition);
        Rotation += (float)(additionalRotation * (Math.PI / 180));

    }

    /// <summary>
    /// Method to clamp the marker to the current bounding rect
    /// </summary>
    /// <param name="bounds">The rectangle to fixate the marker in</param>
    private void SetMarkerPosition(Rect2 bounds)
    {
        var globalX = Math.Clamp(GetParent<Node2D>().GlobalPosition.X, bounds.Position.X, bounds.End.X);
        var globalY = Math.Clamp(GetParent<Node2D>().GlobalPosition.Y, bounds.Position.Y, bounds.End.Y);
        GlobalPosition = new Vector2(globalX, globalY);
    }
}

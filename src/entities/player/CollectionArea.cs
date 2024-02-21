using Godot;
using System;
using System.Linq;

public partial class CollectionArea : Area2D
{
    private CollisionShape2D collisionCircle;

    private float baseRadius;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        collisionCircle = GetChildren().OfType<CollisionShape2D>()
                                       .FirstOrDefault();

        var circle = collisionCircle.Shape as CircleShape2D;
        baseRadius = circle.Radius;
    }

    public void SetRadiusMultiplier(float multiplier)
    {
        var circle = collisionCircle.Shape as CircleShape2D;
        circle.Radius = baseRadius * multiplier;
    }
}

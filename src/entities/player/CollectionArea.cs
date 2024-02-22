using Godot;
using System.Linq;

/// <summary>
/// Class to control the collection area of an entity
/// </summary>
public partial class CollectionArea : Area2D
{
    /// <summary>
    /// The collision shape for the collection area,
    /// this will be modified
    /// </summary>
    private CollisionShape2D collisionCircle;

    /// <summary>
    /// The initial radius the collection radius was at. Will be used to multiple it
    /// </summary>
    private float baseRadius;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        collisionCircle = GetChildren().OfType<CollisionShape2D>()
                                       .FirstOrDefault();

        var circle = collisionCircle.Shape as CircleShape2D;
        baseRadius = circle.Radius;
    }

    /// <summary>
    /// Set the new radius based on a multiplier
    /// </summary>
    /// <param name="multiplier">The multiplier to apply</param>
    public void SetRadiusMultiplier(float multiplier)
    {
        var circle = collisionCircle.Shape as CircleShape2D;
        circle.Radius = baseRadius * multiplier;
    }
}

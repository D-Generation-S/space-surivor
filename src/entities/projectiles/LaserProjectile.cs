using Godot;
using System;
using System.Linq;

/// <summary>
/// Control script for a laser projectile
/// </summary>
public partial class LaserProjectile : Area2D
{
    /// <summary>
    /// THe maximal distance for a projectile to travel,
    /// exceeding this will delete the projectile
    /// </summary>
    [Export]
    private float maxDistance = 3500;

    /// <summary>
    /// The health component for the laser projectile
    /// </summary>
    [Export]
    private HealthComponent healthComponent;

    /// <summary>
    /// The initial spawn location where this projectile was created at,
    /// used to calculate the traveled distance
    /// </summary>
    private Vector2 initialSpawnLocation;

    /// <summary>
    /// The current velocity of the projectile
    /// </summary>
    private Vector2 velocity;

    /// <summary>
    /// The entity which did fire this projectile, 
    /// this is used to disable damage for that entity
    /// </summary>
    private Node2D firingEntity;

    /// <summary>
    /// The damage of the projectile
    /// </summary>
    private int damage;
 
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    /// <summary>
    /// Event if the project does enter a body2d
    /// </summary>
    /// <param name="body">The body which was entered</param>
    public void OnBodyEntered(Node2D body)
    {
        if (body == firingEntity)
        {
            return;
        }
        var health = body.GetNode("%Components").GetChildren().OfType<HealthComponent>().FirstOrDefault();
        health.Damage(damage);
        QueueFree();
    }

    /// <inheritdoc/>
    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += velocity * (float)delta;
        if (GlobalPosition.DistanceTo(initialSpawnLocation) > maxDistance)
        {
            QueueFree();
        }
    }

    /// <summary>
    /// Method to fire this projectile
    /// </summary>
    /// <param name="rotation">The rotation to fire the projectile to</param>
    /// <param name="spread">The spread of the projectile to use on spawn to alter rotation</param>
    /// <param name="firingEntity">The entity which fired this projectile</param>
    /// <param name="configuration">The configuration of the fired laser weapon</param>
    public void FireProjectile(float rotation, Node2D firingEntity, LaserWeaponConfiguration configuration)
    {
        damage = configuration.GetProjectileDamage();
        this.firingEntity = firingEntity;
        initialSpawnLocation = firingEntity.GlobalPosition;
        Rotation = rotation;
        var random = Random.Shared.NextDouble();
        var direction = Random.Shared.NextDouble() - 0.5;
        var realSpread = configuration.GetWeaponSpread() * random;
        if (direction < 0)
        {
            realSpread *= -1;
        }
        var fireDirection = new Vector2((float)realSpread, -1).Normalized();
        velocity = fireDirection.Rotated(Rotation) * configuration.GetProjectileSpeed();
    }
}

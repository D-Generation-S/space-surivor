using Godot;
using System;
using System.Linq;

public partial class LaserProjectile : Area2D
{
    private Vector2 initialSpawnLocation;

    [Export]
    private float maxDistance = 3500;

    [Export]
    private HealthComponent healthComponent;

    private Vector2 velocity;

    private Node2D firingEntity;

    private int damage;
 
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

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

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += velocity * (float)delta;
        if (GlobalPosition.DistanceTo(initialSpawnLocation) > maxDistance)
        {
            QueueFree();
        }
    }

    public void FireProjectile(float rotation, float spread, Node2D firingEntity, LaserWeaponConfiguration configuration)
    {
        damage = configuration.GetProjectileDamage();
        this.firingEntity = firingEntity;
        initialSpawnLocation = firingEntity.GlobalPosition;
        Rotation = rotation;
        var random = Random.Shared.NextDouble();
        var direction = Random.Shared.NextDouble() - 0.5;
        var realSpread = spread * random;
        if (direction < 0)
        {
            realSpread *= -1;
        }
        var fireDirection = new Vector2((float)realSpread, -1).Normalized();
        velocity = fireDirection.Rotated(Rotation) * configuration.GetProjectileSpeed();
    }
}

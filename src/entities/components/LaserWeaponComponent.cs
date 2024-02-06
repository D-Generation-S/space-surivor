using Godot;

/// <summary>
/// Laser weapon component class
/// </summary>
public partial class LaserWeaponComponent : WeaponComponent
{
    /// <summary>
    /// The configuration for this laser weapon
    /// </summary>
    [Export]
    private LaserWeaponConfiguration laserWeaponConfiguration;

    /// <inheritdoc/>
    public override void _Ready()
    {
        SetBaseWeaponConfiguration(laserWeaponConfiguration);
        base._Ready();
    }

    /// <inheritdoc/>
    protected override void WeaponWasFired()
    {
        base.WeaponWasFired();
        var projectile = laserWeaponConfiguration.GetPackedScene().Instantiate<LaserProjectile>();
        projectile.FireProjectile(GetShip().Rotation, GetParent<Node>().GetParent<Node2D>(), laserWeaponConfiguration);
        projectile.GlobalPosition = GetWeaponSpawnPoint().GlobalPosition;
        GetTree().Root.CallDeferred("add_child", projectile);
        base.FireWeapon();
    }
}
using Godot;

public partial class LaserWeaponComponent : WeaponComponent
{
    [Export]
    private LaserWeaponConfiguration laserWeaponConfiguration;



    public override void _Ready()
    {
        BaseWeaponConfiguration(laserWeaponConfiguration);
        base._Ready();
    }

    public override void FireWeapon()
    {
        if (!CanFireWeapon())
        {
            return;
        }
        var projectile = laserWeaponConfiguration.GetPackedScene().Instantiate<LaserProjectile>();
        projectile.FireProjectile(GetShip().Rotation, laserWeaponConfiguration.GetWeaponSpread(), GetParent<Node>().GetParent<Node2D>(), laserWeaponConfiguration);
        projectile.GlobalPosition = GetWeaponSpawnPoint().GlobalPosition;
        GetTree().Root.CallDeferred("add_child", projectile);
        base.FireWeapon();
    }
}
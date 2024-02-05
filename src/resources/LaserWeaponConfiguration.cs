using System;
using Godot;

[GlobalClass]
public partial class LaserWeaponConfiguration : WeaponConfiguration
{
    [ExportGroup("Projectile Movement")]
    [Export]
    private PackedScene laserWeaponScene;

    [ExportGroup("Projectile Movement")]
    [Export(PropertyHint.Range, "0, 1")]
    private float weaponSpread = 0f;

    [ExportGroup("Projectile Damage")]
    [Export(PropertyHint.Range, "550, 700")]
    private int projectileSpeed = 550;

    [ExportGroup("Projectile Damage")]
    [Export]
    private int projectileDamage = 10;

    public PackedScene GetPackedScene()
    {
        return laserWeaponScene;
    }

    public float GetWeaponSpread()
    {
        return weaponSpread;
    }

    public int GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    public int GetProjectileDamage()
    {
        return projectileDamage;
    }
}
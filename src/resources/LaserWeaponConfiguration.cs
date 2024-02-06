using Godot;

/// <summary>
/// Configuration for a laser weapon
/// </summary>
[GlobalClass]
public partial class LaserWeaponConfiguration : WeaponConfiguration
{
    /// <summary>
    /// The projectile which should be spawned
    /// </summary>
    [ExportGroup("Projectile Movement")]
    [Export]
    private PackedScene laserWeaponScene;

    /// <summary>
    /// The spread of the weapon
    /// </summary>
    [ExportGroup("Projectile Movement")]
    [Export(PropertyHint.Range, "0, 1")]
    private float weaponSpread = 0f;

    /// <summary>
    /// The speed of each projectile
    /// </summary>
    [ExportGroup("Projectile Damage")]
    [Export(PropertyHint.Range, "550, 700")]
    private int projectileSpeed = 550;

    /// <summary>
    /// The damage each projectile will deal
    /// </summary>
    [ExportGroup("Projectile Damage")]
    [Export]
    private int projectileDamage = 10;

    /// <summary>
    /// Get the template for a new projectile
    /// </summary>
    /// <returns>The projectile template</returns>
    public PackedScene GetPackedScene()
    {
        return laserWeaponScene;
    }

    /// <summary>
    /// Get the spread for this weapon
    /// </summary>
    /// <returns>The spread if firing</returns>
    public float GetWeaponSpread()
    {
        return weaponSpread;
    }

    /// <summary>
    /// Get the speed for the projectiles
    /// </summary>
    /// <returns>The speed of the projectile</returns>
    public int GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    /// <summary>
    /// Get the damage per projectile
    /// </summary>
    /// <returns>The damage for each projectile</returns>
    public int GetProjectileDamage()
    {
        return projectileDamage;
    }
}
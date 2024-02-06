using Godot;

/// <summary>
/// Base component for any weapon component
/// </summary>
public partial class WeaponComponent : ConsumerComponent
{
    /// <summary>
    /// The ship component this weapon is attached to
    /// </summary>
    [Export]
    private EntityMovement ship;

    /// <summary>
    /// The node to spawn the projectiles on
    /// </summary>
    private Node2D weaponSpawnPoint;

    /// <summary>
    /// The current ticks since the last fired shot
    /// </summary>
    private int currentFirePauseTick = 0;

    /// <summary>
    /// The head stored by this component
    /// </summary>
    private int storedHeat = 0;

    /// <summary>
    /// Was the weapon fired in the last frame
    /// </summary>
    private bool wasFired = false;

    /// <summary>
    /// Can the weapon fire already
    /// </summary>
    private bool canFire = true;
    
    /// <summary>
    /// The configuration for this weapon
    /// </summary>
    private WeaponConfiguration baseComponent;

    /// <summary>
    /// The audio manager to use for sound effects
    /// </summary>
    private AudioManager audioManager;

    public override void _Ready()
    {
        base._Ready();
        weaponSpawnPoint = ship.GetNode<Node2D>("%WeaponSpawn");
        audioManager = new AudioManager(2, () => new AudioStreamPlayer2D
        {
            MaxDistance = 2000,
            Bus = "Effects"
        });
        GetShip().CallDeferred("add_child", audioManager);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.

    public override void _Process(double delta)
    {
        if (wasFired && Active())
        {
            WeaponWasFired();
        }
        wasFired = false;

        base._Process(delta);
    }

    /// <inheritdoc/>
    public override void ConsumerTick(int tickNumber)
    {
        if (!canFire)
        {
            currentFirePauseTick--;
            if (currentFirePauseTick < 0)
            {
                canFire = true;
                currentFirePauseTick = baseComponent.GetFireRate();
            }
        }
    }

    /// <summary>
    /// Set the base weapon configuration
    /// </summary>
    /// <param name="baseComponent">The base component to use</param>
    protected void SetBaseWeaponConfiguration(WeaponConfiguration baseComponent)
    {
        this.baseComponent = baseComponent;
    }

    /// <inheritdoc/>
    public override int GetConsumption()
    {
        return wasFired ? baseComponent.GetFiringConsumption() : baseComponent.GetIdleConsumption();
    }

    /// <inheritdoc/>
    public override int GetStoredHeat()
    {
        if (!Active())
        {
            return 0;
        }
        var returnHeat = storedHeat;
        storedHeat = 0;
        return returnHeat > 0 ? returnHeat : baseComponent.GetIdleHeat();
    }

    /// <summary>
    /// Was the weapon fired in the last frame,
    /// do everything required to actually fire the weapon in this method
    /// </summary>
    protected virtual void WeaponWasFired()
    {
        storedHeat += baseComponent.GetFiringHeat();     
        
        var fireEffect = baseComponent.GetFireEffect();   
        if (fireEffect is not null)
        {
            audioManager.QueueSoundToPlay(fireEffect);
        }
    }

    /// <summary>
    /// External trigger to try firing the weapon
    /// </summary>
    public void FireWeapon()
    {
        if (!CanFireWeapon())
        {
            return;
        }
        canFire = false;
        wasFired = true;
    }

    /// <summary>
    /// Get a reference to the ship
    /// </summary>
    /// <returns>The ship this weapon is assigned to</returns>
    protected EntityMovement GetShip()
    {
        return ship;
    }

    /// <summary>
    /// Can the weapon be fired
    /// </summary>
    /// <returns>True if can be fired in this frame</returns>
    protected bool CanFireWeapon()
    {
        return Active() && canFire;
    }

    /// <summary>
    /// Get the node2d where projectiles should be spawned
    /// </summary>
    /// <returns>The node 2d to spawn projectiles on</returns>
    public Node2D GetWeaponSpawnPoint()
    {
        return weaponSpawnPoint;
    }
}

using Godot;
public partial class WeaponComponent : ConsumerComponent
{
    [Export]
    private EntityMovement ship;    

    [Export]
    private int ticksBetweenFiring = 15;

    private Node2D weaponSpawnPoint;

    private int currentFirePauseTick = 0;

    private int storedHeat = 0;

    private bool wasFired = false;

    private bool canFire = true;
    
    private WeaponConfiguration baseComponent;

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

    public override void ConsumerTick(int tickNumber)
    {
        if (!canFire)
        {
            currentFirePauseTick--;
            if (currentFirePauseTick < 0)
            {
                canFire = true;
                currentFirePauseTick = ticksBetweenFiring;
            }
        }
    }

    protected void BaseWeaponConfiguration(WeaponConfiguration baseComponent)
    {
        this.baseComponent = baseComponent;
    }

    public override int GetConsumption()
    {
        return wasFired ? baseComponent.GetFiringConsumption() : baseComponent.GetIdleConsumption();
    }

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

    protected virtual void WeaponWasFired()
    {
        storedHeat += baseComponent.GetFiringHeat();     
        
        var fireEffect = baseComponent.GetFireEffect();   
        if (fireEffect is not null)
        {
            audioManager.QueueSoundToPlay(fireEffect);
        }
    }

    public void FireWeapon()
    {
        if (!CanFireWeapon())
        {
            return;
        }
        canFire = false;
        wasFired = true;
    }

    protected EntityMovement GetShip()
    {
        return ship;
    }

    protected bool CanFireWeapon()
    {
        return Active() && canFire;
    }

    public Node2D GetWeaponSpawnPoint()
    {
        return weaponSpawnPoint;
    }
}

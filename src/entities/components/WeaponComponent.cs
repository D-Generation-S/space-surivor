using Godot;
using System;
using System.Net.NetworkInformation;

public partial class WeaponComponent : ConsumerComponent
{
    [Export]
    private EntityMovement ship;

    private Node2D weaponSpawnPoint;

    [Export]
    private int ticksBetweenFiring = 15;

    private int currentFirePauseTick = 0;

    private int storedHeat = 0;

    private bool wasFired = false;

    private bool canFire = true;
    
    private WeaponConfiguration baseComponent;

    public override void _Ready()
    {
        weaponSpawnPoint = ship.GetNode<Node2D>("%WeaponSpawn");
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.

    public override void _Process(double delta)
    {
        if (wasFired && Active())
        {
            storedHeat += baseComponent.GetFiringHeat();
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
        var returnHeat = storedHeat;
        storedHeat = 0;
        return returnHeat > 0 ? returnHeat : baseComponent.GetIdleHeat();
    }

    public virtual void FireWeapon()
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

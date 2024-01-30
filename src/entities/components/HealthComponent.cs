using Godot;

public partial class HealthComponent : Node
{
    [Signal]
    public delegate void DiedEventHandler();

    [Export]
    private int health;

    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            EmitSignal(SignalName.Died);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
using Godot;

public partial class HealthComponent : Node
{
    [Signal]
    public delegate void DiedEventHandler();

    [Signal]
    public delegate void TookDamageEventHandler(int newHealth);
    
    [Signal]
    public delegate void MaxHealthChangedEventHandler(int newMaxHealth);

    [Export]
    private int maxHealth;

    private int currentHealth;

    public override void _Ready()
    {
        currentHealth = maxHealth;
        base._Ready();
    }

    public void Damage(int amount)
    {
        currentHealth -= amount;
        EmitSignal(SignalName.TookDamage, currentHealth);
        if (currentHealth <= 0)
        {
            EmitSignal(SignalName.Died);
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
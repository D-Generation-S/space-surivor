using Godot;

/// <summary>
/// Component to represent the health of the ship
/// </summary>
public partial class HealthComponent : Node
{
    /// <summary>
    /// Did the ship die
    /// </summary>
    [Signal]
    public delegate void DiedEventHandler();

    /// <summary>
    /// Did the ship take damage
    /// </summary>
    /// <param name="newHealth">The new health after taking damage</param>
    [Signal]
    public delegate void TookDamageEventHandler(int newHealth);
    
    /// <summary>
    /// The maximal health did change
    /// </summary>
    /// <param name="newMaxHealth">The max health</param>
    [Signal]
    public delegate void MaxHealthChangedEventHandler(int newMaxHealth);

    /// <summary>
    /// The current max health of this ship
    /// </summary>
    [Export]
    private int maxHealth;

    /// <summary>
    /// The current health for this ship
    /// </summary>
    private int currentHealth;

    public override void _Ready()
    {
        currentHealth = maxHealth;
        base._Ready();
    }

    /// <summary>
    /// Method to damage the ship health component
    /// </summary>
    /// <param name="amount">The damage amount</param>
    public void Damage(int amount)
    {
        currentHealth -= amount;
        EmitSignal(SignalName.TookDamage, currentHealth);
        if (currentHealth <= 0)
        {
            EmitSignal(SignalName.Died);
        }
    }

    /// <summary>
    /// Get the max health of this ship
    /// </summary>
    /// <returns>The max health</returns>
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>
    /// Get the current health of this ship
    /// </summary>
    /// <returns>The current health</returns>
    public int GetHealth()
    {
        return currentHealth;
    }
}
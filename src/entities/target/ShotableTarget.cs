using Godot;
using System.Linq;

/// <summary>
/// A target which can be shot
/// </summary>
public partial class ShotableTarget : BaseTarget
{
    /// <summary>
    /// The progress bar of the target to show current health
    /// </summary>
    [Export]
    private ProgressBar progressBar;

    /// <summary>
    /// The collision shape of the object used for receiving damage
    /// </summary>
    private CollisionShape2D collisionShape2D;

    public override void _Ready()
    {
        collisionShape2D = GetChildren().OfType<CollisionShape2D>().FirstOrDefault();
        base._Ready();
    }

    /// <summary>
    /// Change the max health of the object
    /// </summary>
    /// <param name="maxHealth">The new max health to display</param>
    public void MaxHealthChanged(int maxHealth)
    {
        progressBar.MaxValue = maxHealth;
    }

    /// <summary>
    /// Receive some damage
    /// </summary>
    /// <param name="healthLeft">The health which is left after damage was taken</param>
    public void ReceivedDamage(int healthLeft)
    {
        progressBar.Value = healthLeft;
    }

    /// <inheritdoc/>
    protected override void ToggleTargetVisibility(bool newState)
    {
        base.ToggleTargetVisibility(newState);
        if (collisionShape2D is null)
        {
            return;
        }
        collisionShape2D.SetDeferred("disabled",!newState);
    }

    /// <summary>
    /// Method if the object did die
    /// </summary>
    public void Died()
    {
        EmitSignal(SignalName.TargetCompleted);
        QueueFree();
    }
}

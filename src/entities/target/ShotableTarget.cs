using Godot;
using System;
using System.Linq;

public partial class ShotableTarget : BaseTarget
{
    [Export]
    private ProgressBar progressBar;

    private CollisionShape2D collisionShape2D;

    public override void _Ready()
    {
        collisionShape2D = GetChildren().OfType<CollisionShape2D>().FirstOrDefault();
        base._Ready();
    }

    public void MaxHealthChanged(int maxHealth)
    {
        progressBar.MaxValue = maxHealth;
    }

    public void ReceivedDamage(int healthLeft)
    {
        progressBar.Value = healthLeft;
    }

    protected override void ToggleTargetVisibility(bool newState)
    {
        base.ToggleTargetVisibility(newState);
        collisionShape2D.SetDeferred("disabled",!newState);
    }


    public void Died()
    {
        EmitSignal(SignalName.TargetCompleted);
        QueueFree();
    }
}

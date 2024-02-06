using System.Linq;
using Godot;

/// <summary>
/// Class to bundle multiple targets
/// </summary>
public partial class MultiTarget : BaseTarget
{
    /// <summary>
    /// An array with all the target related to this multi target
    /// </summary>
    [Export]
    private BaseTarget[] targets;

    /// <summary>
    /// The number of targets left
    /// </summary>
    private int targetCounter;

    /// <inheritdoc/>
    public override void _Ready()
    {
        targetCounter = targets.Length;
        base._Ready();
    }

    /// <inheritdoc/>
    protected override void ToggleTargetVisibility(bool newState)
    {
        foreach(var target in targets.OfType<BaseTarget>())
        {
            if (target is null)
            {
                continue;
            }
            if (newState)
            {
                target.MakeTargetVisible();
                continue;
            }
            target.HideTarget();
        }
        base.ToggleTargetVisibility(false);
    }

    /// <summary>
    /// Method to check if the target was completed successfully
    /// </summary>
    public void TargetWasCompleted()
    {
        targetCounter--;
        if (targetCounter <= 0)
        {
            EmitSignal(SignalName.TargetCompleted);
            QueueFree();
        }
    }
}
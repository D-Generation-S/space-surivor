using Godot;

/// <summary>
/// Base class for targets
/// </summary>
public partial class BaseTarget : Node2D
{   
    /// <summary>
    /// Should the target be visible after spawning
    /// </summary>
    [Export]
    private bool targetVisible; 

    /// <summary>
    /// Event handler if the target was hit
    /// </summary>
    [Signal]
    public delegate void TargetCompletedEventHandler();

    /// <inheritdoc/>
    public override void _Ready()
    {
        ToggleTargetVisibility(targetVisible);
    }

    /// <summary>
    /// Toggle the visibility of this target
    /// </summary>
    /// <param name="newState">The new state to set the target</param>
    protected virtual void ToggleTargetVisibility(bool newState)
    {
        SetDeferred("visible", newState);
    }

    /// <summary>
    /// Hide the target
    /// </summary>
    public virtual void HideTarget()
    {
        ToggleTargetVisibility(false);
    }

    /// <summary>
    /// Make the target visible again
    /// </summary>
    public virtual void MakeTargetVisible()
    {
        ToggleTargetVisibility(true);
    }
}

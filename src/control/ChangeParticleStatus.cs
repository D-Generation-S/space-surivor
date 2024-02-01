using Godot;

/// <summary>
/// Simple class to change the status of an particle engine
/// This class is build as a receiver only
/// </summary>
public partial class ChangeParticleStatus : GpuParticles2D
{
    /// <summary>
    /// Activate the particle emitter
    /// </summary>
    public void ActivateParticleEmitter()
    {
        Emitting = true;
    }

    /// <summary>
    /// Disable the particle emitter
    /// </summary>
    public void DisableParticleEmitter()
    {
        Emitting = false;
    }

    /// <summary>
    /// Toggle the particle emitter on or off
    /// </summary>
    public void ToggleParticleEmitter()
    {
        Emitting = !Emitting;
    }
}

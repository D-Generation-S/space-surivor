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
        SetParticleEmitterState(true);
    }

    /// <summary>
    /// Disable the particle emitter
    /// </summary>
    public void DisableParticleEmitter()
    {
        SetParticleEmitterState(false);
    }

    /// <summary>
    /// Method to set the state of the particle emitter
    /// </summary>
    /// <param name="newState">The new state to set</param>
    public void SetParticleEmitterState(bool newState)
    {
        Emitting = newState;
    }

    /// <summary>
    /// Toggle the particle emitter on or off
    /// </summary>
    public void ToggleParticleEmitter()
    {
        Emitting = !Emitting;
    }
}

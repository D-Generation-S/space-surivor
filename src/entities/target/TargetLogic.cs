using Godot;
using System.Linq;

/// <summary>
/// Class to define a fly through target.
/// It does hold the complete logic for such an packed scene
/// </summary>
public partial class TargetLogic : BaseTarget
{
    /// <summary>
    /// The visual component for this target
    /// </summary>
    private Sprite2D visuals;

    /// <summary>
    /// The collider of this target
    /// </summary>
    private Area2D areaCollider;

    /// <summary>
    /// The particle emitter for the death animation
    /// </summary>
    private GpuParticles2D particleEmitter;

    /// <summary>
    /// The audio component sued for the death animation
    /// </summary>
    private AudioStreamPlayer2D audio;

    /// <summary>
    /// Is this target already destroyed
    /// </summary>
    private bool destroyed;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        visuals = GetChildren().OfType<Sprite2D>().FirstOrDefault();
        particleEmitter = GetChildren().OfType<GpuParticles2D>().FirstOrDefault();
        areaCollider = GetChildren().OfType<Area2D>().FirstOrDefault();
        audio = GetChildren().OfType<AudioStreamPlayer2D>().FirstOrDefault();


        base._Ready();
        areaCollider.BodyEntered += (entity) => {
            if (entity.Name != "Player")
            {
                return;
            }
            HideTarget();
            particleEmitter.Emitting = true;
            audio.Play();
            EmitSignal(SignalName.TargetCompleted);
            destroyed = true;
        };

        particleEmitter.Finished += () => {
            if (!destroyed)
            {
                return;
            }
            
            GetParent().CallDeferred("remove_child", this);
        };
    }

    protected override void ToggleTargetVisibility(bool newState)
    {
        base.ToggleTargetVisibility(newState);
        areaCollider.SetDeferred("monitorable", newState);
        areaCollider.SetDeferred("monitoring", newState);
    }


}

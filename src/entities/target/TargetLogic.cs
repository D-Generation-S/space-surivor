using Godot;
using System.Linq;

public partial class TargetLogic : Node2D
{
	[Signal]
	public delegate void TargetHitEventHandler();

	[Export]
	private bool targetVisible;

	private Sprite2D visuals;

	private GpuParticles2D particleEngine;

	private Area2D areaCollider;

	private AudioStreamPlayer2D audio;

	private bool destroyed;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		visuals = GetChildren().OfType<Sprite2D>().FirstOrDefault();
		particleEngine = GetChildren().OfType<GpuParticles2D>().FirstOrDefault();
		areaCollider = GetChildren().OfType<Area2D>().FirstOrDefault();
		audio = GetChildren().OfType<AudioStreamPlayer2D>().FirstOrDefault();

		HideTarget();
		if (targetVisible)
		{
			MakeTargetVisible();
		}

		areaCollider.BodyEntered += (entity) => {
			if (entity.Name != "Player")
			{
				return;
			}
			HideTarget();
			particleEngine.Emitting = true;
			audio.Play();
			EmitSignal(SignalName.TargetHit);
			destroyed = true;
		};

		particleEngine.Finished += () => {
			if (!destroyed)
			{
				return;
			}
			
			GetParent().CallDeferred("remove_child", this);
		};

	}

	private void ToggleTargetVisibility(bool newState)
	{

		visuals.Visible = newState;
		areaCollider.Monitorable = newState;
		areaCollider.Monitoring = newState;
	}

	public void HideTarget()
	{
		ToggleTargetVisibility(false);
	}

	public void MakeTargetVisible()
	{
		ToggleTargetVisibility(true);
	}

	
}

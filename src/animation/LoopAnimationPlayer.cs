using Godot;

/// <summary>
/// Simple loop animation player.
/// </summary>
public partial class LoopAnimationPlayer : AnimationPlayer
{

	/// <summary>
	/// The name of the animation to loop
	/// </summary>
	[Export]
	private string animationName;

	public override void _Ready()
	{
		Play(animationName);
		AnimationFinished += (name) => {
			Play(name);
		};
	}
}

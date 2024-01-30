using Godot;

/// <summary>
/// Start a animation automatically
/// </summary>
public partial class AutoAnimate : AnimationPlayer
{
	public override void _Ready()
	{
		ReplayAnimation();
	}

	/// <summary>
	/// Replay the animation
	/// </summary>
	public void ReplayAnimation()
	{
		Play("MoveForward");
	}
}

using Godot;
using System;

public partial class LoopAnimationPlayer : AnimationPlayer
{

	[Export]
	private string animationName;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Play(animationName);
		AnimationFinished += (name) => {
			Play(name);
		};
	}
}

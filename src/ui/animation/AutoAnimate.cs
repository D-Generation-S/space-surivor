using Godot;
using System;

public partial class AutoAnimate : AnimationPlayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ReplayAnimation();
	}

	public void ReplayAnimation()
	{
		Play("MoveForward");
	}
}

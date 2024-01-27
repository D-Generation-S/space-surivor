using Godot;
using System;

public partial class ChangeParticleStatus : GpuParticles2D
{
	public void ActivateParticleEmitter()
	{
		Emitting = true;
	}

	public void DisableParticleEmitter()
	{
		Emitting = false;
	}

	public void ToggleParticleEmitter()
	{
		Emitting = !Emitting;
	}
}

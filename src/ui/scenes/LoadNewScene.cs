using Godot;
using System;

public partial class LoadNewScene : CloseUi
{
	[Export(PropertyHint.File, "*.tscn")]
	private string scenePath;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (scenePath is null)
		{
			Disabled = true;
		}
	}

    public override void _Pressed()
    {
		var packedScene = ResourceLoader.Load<PackedScene>(scenePath);
		
		GetTree().Root.CallDeferred("add_child", packedScene.Instantiate());
        base._Pressed();
    }
}

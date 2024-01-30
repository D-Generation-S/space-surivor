using Godot;
using System;

public partial class LoadNewScene : CloseUi
{
	[Export(PropertyHint.File, "*.tscn")]
	private string scenePath;

	private Node createdObject;

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
		if (createdObject is not null)
		{
			return;
		}
		var packedScene = ResourceLoader.Load<PackedScene>(scenePath);
		createdObject = packedScene.Instantiate();
		//createdObject.TreeExiting += () => {
			//createdObject = null;
		//};
		if (targetNode is not null)
		{
			targetNode.CallDeferred("add_child", createdObject);
		} else {
			GetTree().Root.CallDeferred("add_child", createdObject);
		}
		if (nodeToClose is not null)
		{
			createdObject = null;
		}
        base._Pressed();
    }
}

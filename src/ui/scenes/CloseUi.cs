using Godot;
using System;

public partial class CloseUi : Button
{
	[Export]
	private Node nodeToClose;

    public override void _Pressed()
    {
        base._Pressed();
		if (nodeToClose == null)
		{
			return;
		}
		GetTree().Root.CallDeferred("remove_child", nodeToClose);
    }
}

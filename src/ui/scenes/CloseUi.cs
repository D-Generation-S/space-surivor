using Godot;
using System;

public partial class CloseUi : Button
{
	[Export]
	protected Node nodeToClose;
	
	[Export]
	protected Node targetNode;

    public override void _Pressed()
    {
        base._Pressed();
		if (nodeToClose is null)
		{
			return;
		}
		if ( targetNode is null)
		{
			if (GetTree().Root == GetParent())
			{
				GetTree().Root.CallDeferred("remove_child", nodeToClose);
			}
			GetParent().CallDeferred("remove_child", nodeToClose);
			return;
		}
		targetNode.CallDeferred("remove_child", nodeToClose);
    }
}

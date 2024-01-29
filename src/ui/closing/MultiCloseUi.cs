using Godot;
using System;

public partial class MultiCloseUi : VBoxContainer
{
	[Export]
	private Node[] nodes;


	public void CloseUi()
	{
		foreach(var node in nodes)
		{
			CallDeferred("remove_child", node);
		}
	}
}

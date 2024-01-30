using Godot;
using System;

public partial class CloseGame : Button
{
    public override void _Pressed()
    {
        base._Pressed();
		GetTree().Quit();
    }
}

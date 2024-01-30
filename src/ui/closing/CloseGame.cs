using Godot;

/// <summary>
/// Script to close the game
/// </summary>
public partial class CloseGame : Button
{
    public override void _Pressed()
    {
        base._Pressed();
		GetTree().Quit();
    }
}

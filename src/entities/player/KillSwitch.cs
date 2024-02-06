using System.Linq;
using Godot;

/// <summary>
/// Simple class to kill the player
/// </summary>
public partial class KillSwitch : Node
{
    /// <summary>
    /// Trigger if the player died
    /// </summary>
    public void Died()
    {
        var camera = GetParent().GetChildren().OfType<Camera2D>().FirstOrDefault();
        if (camera is not null)
        {
            GetParent().CallDeferred("remove_child", camera);
            GetParent().GetParent().CallDeferred("add_child", camera);
            camera.GlobalPosition = GetParent<Node2D>().GlobalPosition;
        }
        GetParent().QueueFree();
    }
}

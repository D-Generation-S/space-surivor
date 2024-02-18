using Godot;

/// <summary>
/// Close multiple UI Nodes
/// </summary>
public partial class MultiCloseUi : Control
{
    /// <summary>
    /// The nodes to close
    /// </summary>
    [Export]
    private Node[] nodes;

    /// <summary>
    /// Method to trigger which will close all the ui nodes specified
    /// </summary>
    public void CloseUi()
    {
        foreach(var node in nodes)
        {
            if (node == this)
            {
                QueueFree();
                continue;
            }
            CallDeferred("remove_child", node);
        }
    }
}

using Godot;

/// <summary>
/// Close multiple UI Nodes
/// </summary>
public partial class MultiCloseUi : VBoxContainer
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
            CallDeferred("remove_child", node);
        }
    }
}

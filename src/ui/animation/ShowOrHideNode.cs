using Godot;

/// <summary>
/// Show or hide a node
/// </summary>
public partial class ShowOrHideNode : Control
{
    /// <summary>
    /// The initial state of the node
    /// </summary>
    [Export]
    private bool initialState = false;

    public override void _Ready()
    {
        Visible = initialState;
        GetParent<Control>().MouseFilter = initialState ? MouseFilterEnum.Stop : MouseFilterEnum.Ignore;
    }

    /// <summary>
    /// Show the control node
    /// </summary>
    public void ShowControl()
    {
        SetDeferred("visible", true);
        GetParent<Control>().SetDeferred("mouse_filter", 0);		
        
    }

    /// <summary>
    /// Hide the control node
    /// </summary>
    public void HideControl()
    {
        SetDeferred("visible", false);
        GetParent<Control>().SetDeferred("mouse_filter", 2);	
    }
}

using Godot;

/// <summary>
/// Class to get the focus of a button
/// </summary>
public partial class GetFocus : Button
{
    /// <summary>
    /// Get the auto focus right on the start if possible
    /// </summary>
    [Export]
    private bool getAutoFocus;

    /// <summary>
    /// Get the auto focus if this component gets visible
    /// </summary>
    [Export]
    private bool getFocusIfVisible;

    public override void _Ready()
    {
        if (getFocusIfVisible)
        {
            VisibilityChanged += () => {
                if (Visible)
                {
                    GetFocusNow();
                }
            };
        }
        if (!getAutoFocus)
        {
            return;
        }
        GetFocusNow();
    }

    /// <summary>
    /// Method to call to get the focus right away
    /// </summary>
    public void GetFocusNow()
    {
        GrabFocus();
    }
}

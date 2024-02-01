using Godot;
using System;

public partial class GetFocus : Button
{
	[Export]
	private bool getAutoFocus;

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

    public void GetFocusNow()
	{
		GrabFocus();
	}
}

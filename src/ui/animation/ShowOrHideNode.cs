using Godot;

public partial class ShowOrHideNode : Control
{
	[Export]
	private bool initialState = false;

	public override void _Ready()
	{
		Visible = initialState;
		GetParent<Control>().MouseFilter = initialState ? MouseFilterEnum.Stop : MouseFilterEnum.Ignore;
	}

	public void ShowControl()
	{
		SetDeferred("visible", true);
		GetParent<Control>().SetDeferred("mouse_filter", 0);		
		
	}

	public void HideControl()
	{
		SetDeferred("visible", false);
		GetParent<Control>().SetDeferred("mouse_filter", 2);	
	}
}

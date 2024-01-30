using Godot;

public partial class ShowOrHideNode : Control
{
	[Export]
	private bool initialState = false;

	public override void _Ready()
	{
		Visible = initialState;
	}

	public void ShowControl()
	{
		Visible = true;
	}

	public void HideControl()
	{
		Visible = false;
	}
}

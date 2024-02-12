using Godot;

public partial class EnemyDeath : Node
{
	[Export]
	private PackedScene deathEffect;

	public void Died()
	{
		if (deathEffect is not null)
		{
			var effect = deathEffect.Instantiate<Node2D>();
			effect.GlobalPosition = GetParent<Node2D>().GlobalPosition;
			GetParent().GetParent().CallDeferred("add_child", effect);
		}
		GetParent().QueueFree();
	}
}

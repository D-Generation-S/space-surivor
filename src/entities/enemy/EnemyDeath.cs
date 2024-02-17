using Godot;

/// <summary>
/// Simple script to handle the death of an enemy
/// </summary>
public partial class EnemyDeath : Node
{
	/// <summary>
	/// The exp given to the player if this enemy dies
	/// </summary>
	[Export]
	private int expOnDeath;

	/// <summary>
	/// The effect to play if this enemy dies
	/// </summary>
	[Export]
	private PackedScene deathEffect;

	/// <summary>
	/// Triggerable method to handle the death of this enemy
	/// </summary>
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

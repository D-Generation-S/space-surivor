using System.Linq;
using Godot;

/// <summary>
/// Simple script to handle the death of an enemy
/// </summary>
public partial class EnemyDeath : Node
{
	/// <summary>
	/// The ai data of this enemy
	/// </summary>
	[Export]
	private EnemyEntity enemyData;

	/// <summary>
	/// The effect to play if this enemy dies
	/// </summary>
	[Export]
	private PackedScene deathEffect;

	[Export]
	private PackedScene expOrb;

	private bool waitingForExpSpawn;
	/// <summary>
	/// Triggerable method to handle the death of this enemy
	/// </summary>
	public void Died(DamageType damageType)
	{
		if (waitingForExpSpawn)
		{
			return;
		}
		if (deathEffect is not null)
		{
			var effect = deathEffect.Instantiate<Node2D>();
			effect.GlobalPosition = GetParent<Node2D>().GlobalPosition;
			GetParent().GetParent().CallDeferred("add_child", effect);
		}
		if (expOrb is not null && damageType != DamageType.Collision)
		{
			var spawnOrb = expOrb.Instantiate<ExperiencePoint>();
			spawnOrb.SetExperiencePoints(enemyData.GetExperienceToGrant());
			spawnOrb.GlobalPosition = enemyData.GlobalPosition;
			GetTree().Root.GetNodesInGroup<Node>("game_screen").FirstOrDefault()?.AddChild(spawnOrb);
			//GetTree().Root.GetNodesInGroup<Node>("game_screen").FirstOrDefault()?.SetDeferred("add_child", spawnOrb);
		}
		GetParent().QueueFree();
	}
}

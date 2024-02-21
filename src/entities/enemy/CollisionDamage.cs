using Godot;
using System.Linq;

/// <summary>
/// Script to handle collision damage if a enemy hits the player
/// </summary>
public partial class CollisionDamage : Node
{
	/// <summary>
	/// The base damage which should be dealt to the collision target
	/// </summary>
	[Export]
	private int baseCollisionDamage;

	/// <summary>
	/// The damage this entity takes if it did collide with the player
	/// </summary>
	[Export]
	private int damageToTakeOnCollision;

	/// <summary>
	/// A reference to the health component of this entity
	/// </summary>
	[Export]
	private HealthComponent healthComponent;

	/// <summary>
	/// Trigger damage if a character was hit
	/// </summary>
	/// <param name="entity">The entity which was hit</param>
	public void HitCharacter(CharacterBody2D entity)
	{
		if (!entity.IsInGroup("player"))
		{
			return;
		}
		var components = entity.GetNode("%Components");
		if (components is null)
		{
			return;
		}
		healthComponent.Damage(damageToTakeOnCollision, DamageType.Collision);
		components.GetChildren().OfType<HealthComponent>().FirstOrDefault()?.Damage(baseCollisionDamage, DamageType.Collision);
	}
}

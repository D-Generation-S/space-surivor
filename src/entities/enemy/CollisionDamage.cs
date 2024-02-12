using Godot;
using System.Linq;

public partial class CollisionDamage : Node
{
	[Export]
	private int baseCollisionDamage;

	[Export]
	private int selfCollideDamage;

	[Export]
	private HealthComponent healthComponent;

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
		healthComponent.Damage(selfCollideDamage);
		components.GetChildren().OfType<HealthComponent>().FirstOrDefault()?.Damage(baseCollisionDamage);
	}
}

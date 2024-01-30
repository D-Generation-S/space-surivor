using Godot;

public partial class TempBorderCollision : Node2D
{
	public void SomethingCollided(EntityMovement entity)
	{
		entity.Velocity = Vector2.Zero;
	}
}


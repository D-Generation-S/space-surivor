using Godot;

/// <summary>
/// Temp class for testing to cancel the player velocity
/// </summary>
public partial class TempBorderCollision : Node2D
{
	/// <summary>
	/// Something collided with the wall, cancel the velocity
	/// </summary>
	/// <param name="entity">The entity which did collide</param>
	public void SomethingCollided(EntityMovement entity)
	{
		entity.Velocity = Vector2.Zero;
	}
}


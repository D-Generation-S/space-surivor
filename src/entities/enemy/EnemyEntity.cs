using Godot;

/// <summary>
/// All the ai data to be used
/// </summary>
public partial class EnemyEntity : EntityMovement
{
	/// <summary>
	/// The exp granted if this enemy dies
	/// </summary>
	[Export]
	private int experienceOnDeath = 1;

	public int GetExperienceToGrant()
	{
		return experienceOnDeath;
	}
}

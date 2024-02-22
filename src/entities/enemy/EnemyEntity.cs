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

	/// <summary>
	/// The method to get the experience this enemy does grant if killed
	/// </summary>
	/// <returns>The experience points to grant</returns>
	public int GetExperienceToGrant()
	{
		return experienceOnDeath;
	}
}

using Godot;

/// <summary>
/// All the ai data to be used
/// </summary>
public partial class AiData : Node
{
	/// <summary>
	/// The exp granted if this enemy dies
	/// </summary>
	[Export]
	private int expOnDeath;
}

using Godot;
using System;

/// <summary>
/// The player entity extending the Entity Movement class
/// this class will also contain information about the player level, experience and other things
/// </summary>
public partial class PlayerEntity : EntityMovement
{
    /// <summary>
    /// Event if the level of the player did change
    /// </summary>
    /// <param name="newLevel">The new player level</param>
    [Signal]
    public delegate void LevelChangedEventHandler(int newLevel);

    /// <summary>
    /// Event if the experience of the player did increase
    /// </summary>
    /// <param name="newExperience">The new experience of the player</param>
    [Signal]
    public delegate void ExperienceIncreasedEventHandler(int newExperience);

    /// <summary>
    /// Event if the experience requirement for the player did increase
    /// </summary>
    /// <param name="newRequiredExperience"></param>
    [Signal]
    public delegate void NextLevelRequiredExperienceChangedEventHandler(int newRequiredExperience);

    /// <summary>
    /// The base experience required by the player to level up
    /// </summary>
    [Export]
    private int baseExperience = 15;

    /// <summary>
    /// The experience required by the player to level up for the next level
    /// </summary>
    private int requiredExp;

    /// <summary>
    /// The current experience the player did collect
    /// </summary>
    private int currentExperience = 0;

    /// <summary>
    /// The current level of the player
    /// </summary>
    private int level = 0;

    public override void _Ready()
    {
        base._Ready();
        requiredExp = baseExperience;
        EmitSignal(SignalName.NextLevelRequiredExperienceChanged, requiredExp);
    }

    /// <summary>
    /// Increase the experience of the player
    /// </summary>
    /// <param name="amount">The points the player did get</param>
    public void IncreaseExperience(int amount)
    {
        currentExperience += amount;
        if (amount > requiredExp)
        {
            level++;
            requiredExp = CalculateExperienceForNewLevel(level);
            EmitSignal(SignalName.NextLevelRequiredExperienceChanged, requiredExp);
            EmitSignal(SignalName.LevelChanged, level);
        }
        EmitSignal(SignalName.ExperienceIncreased, currentExperience);
    }

    /// <summary>
    /// The method to calculate the experience required for the next level
    /// </summary>
    /// <param name="currentLevel">The current level the player ist at</param>
    /// <returns>The number of experience for the player required to level up again</returns>
    private int CalculateExperienceForNewLevel(int currentLevel)
    {
        return (int)Math.Pow(baseExperience, currentLevel + 1);
    }
}

using Godot;
using System;

public partial class PlayerEntity : EntityMovement
{
    [Signal]
    public delegate void LevelChangedEventHandler(int level);

    
    [Signal]
    public delegate void ExperienceIncreasedEventHandler(int newExperience);

    [Signal]
    public delegate void NextLevelRequiredExperienceChangedEventHandler(int newRequiredExperience);

    [Export]
    private int baseExperience = 15;

    private int requiredExp;

    private int currentExperience = 0;

    private int level = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        requiredExp = baseExperience;
        EmitSignal(SignalName.NextLevelRequiredExperienceChanged, requiredExp);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

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

    private int CalculateExperienceForNewLevel(int currentLevel)
    {
        return (int)Math.Pow(baseExperience, currentLevel + 1);
    }
}

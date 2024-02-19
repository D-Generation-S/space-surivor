using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

[Tool]
public partial class ExperienceCollectionArea : Area2D
{
    [Export]
    private float magnetArea;

    private List<CharacterBody2D> trackedExperience;

    private CollisionShape2D collisionShape2D;

    public override void _Ready()
    {
        trackedExperience = new List<CharacterBody2D>();
        collisionShape2D = GetChildren().OfType<CollisionShape2D>().First();
        BodyEntered += BodyEnteredHandler;
    }

    private void BodyEnteredHandler(Node2D enteredBody)
    {
        if (!enteredBody.IsInGroup("experience"))
        {
            return;
        }
        if (enteredBody is CharacterBody2D experience)
        {
            trackedExperience.Add(experience);
            experience.TreeExiting += () => 
            {
                trackedExperience.Remove(experience);
            };
        }
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            var shape = GetChildren().OfType<CollisionShape2D>().First();
            if (shape is null)
            {
                return;
            }
            shape.Scale = new Vector2(magnetArea, magnetArea);

            return;
        }
        foreach (var experience in trackedExperience)
        {
            var targetVector = GlobalPosition - experience.GlobalPosition;
            experience.Velocity = targetVector.Normalized();
        }
        Debug.WriteLine(trackedExperience.Count);
        
    }
}

using System.Diagnostics;
using Godot;

public partial class ExperiencePoint : CharacterBody2D
{
    private int experienceAmount;

    [Export]
    private float movementSpeed = 50;

    public override void _Ready()
    {
        experienceAmount = 0;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (experienceAmount == 0)
        {
            QueueFree();
            return;
        }
        var collider = MoveAndCollide(Velocity * (float)delta * movementSpeed);
        if (collider is not null && collider.GetCollider() is CharacterBody2D character)
        {
            if (character.IsInGroup("player"))
            {
                QueueFree();
                var sessionInfo = GetTree().Root.GetNode<SessionUserSettings>("SessionUserSettings");
                if (sessionInfo is null)
                {
                    return;
                }
                sessionInfo.GetGameSessionData().AddExperience(experienceAmount);
                Debug.WriteLine(sessionInfo.GetGameSessionData().GetExperience());
            }
        }
    }
}

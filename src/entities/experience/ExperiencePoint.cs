using System.Diagnostics;
using Godot;

public partial class ExperiencePoint : CharacterBody2D
{
    [Signal]
    public delegate void AttractorChangedEventHandler(Node2D target);

    [Export]
    private Label debugLabel;

    [Export]
    private Curve areaBoostCurve;

    [Export]
    private float expGrowCap = 4000;

    private CollisionObject2D searchArea;

    private Vector2 initialSearchAreaScale;

    private int experienceAmount;

    private bool consumed;

    public override void _Ready()
    {
        consumed = false;
        searchArea = GetNode<CollisionObject2D>("%SearchArea");
        initialSearchAreaScale = searchArea.Scale;
    }

    public int Consume()
    {
        if (consumed)
        {
            return 0;
        }
        consumed = true;
        return experienceAmount;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (experienceAmount == 0 || consumed)
        {
            QueueFree();
            return;
        }
        var collider = MoveAndCollide(Velocity * (float)delta);
        if (collider is not null && collider.GetCollider() is CharacterBody2D character)
        {
            if (character.IsInGroup("player"))
            {
                QueueFree();
                var player = character as PlayerEntity;
                player.IncreaseExperience(experienceAmount);
            }
            if (character.IsInGroup("experience"))
            {
                var experience = character as ExperiencePoint;
                experienceAmount += experience.Consume();
                float flatBoost = experienceAmount / expGrowCap;
                float boostValue = areaBoostCurve.Sample(flatBoost);
                searchArea.Scale = initialSearchAreaScale * boostValue;
            }
        }

        debugLabel.Text = experienceAmount.ToString();
    }

    public void Attract(Node2D target)
    {
        EmitSignal(SignalName.AttractorChanged, target);
    }

    public void SetExperiencePoints(int points)
    {
        experienceAmount = points;
    }
}

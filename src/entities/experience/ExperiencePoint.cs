using Godot;

/// <summary>
/// Object instance of an experience point.
/// Can be collected by the player to gain experience points.
/// </summary>
public partial class ExperiencePoint : CharacterBody2D
{
    /// <summary>
    /// The attractor changed for this experience point,
    /// this represents the node to drift to
    /// </summary>
    /// <param name="target"></param>
    [Signal]
    public delegate void AttractorChangedEventHandler(Node2D target);

    /// <summary>
    /// The debug label for writing down the experience value
    /// </summary>
    [Export]
    private Label debugLabel;

    /// <summary>
    /// The curve used to boost the search area
    /// </summary>
    [Export]
    private Curve areaBoostCurve;

    /// <summary>
    /// The maximal experience which can be gained until the curve stops growing
    /// </summary>
    [Export]
    private float experienceGrowCap = 4000;

    /// <summary>
    /// The search area in which other experience points will be searched for merging
    /// </summary>
    private CollisionObject2D searchArea;

    /// <summary>
    /// The initial scale of the search area
    /// </summary>
    private Vector2 initialSearchAreaScale;

    /// <summary>
    /// The current amount of experience points
    /// </summary>
    private int experienceAmount;

    /// <summary>
    /// Was this experience point already consumed by another point,
    /// this means the value was merged into the other collider
    /// </summary>
    private bool consumed;

    public override void _Ready()
    {
        consumed = false;
        searchArea = GetNode<CollisionObject2D>("%SearchArea");
        initialSearchAreaScale = searchArea.Scale;
    }

    /// <summary>
    /// Consume this experience point
    /// </summary>
    /// <returns>The amount of experience stored inside of this point</returns>
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
                float flatBoost = experienceAmount > experienceGrowCap ? 1 : experienceAmount / experienceGrowCap;
                float boostValue = areaBoostCurve.Sample(flatBoost);
                searchArea.Scale = initialSearchAreaScale * boostValue;
            }
        }

        debugLabel.Text = experienceAmount.ToString();
    }

    /// <summary>
    /// Set the target this experience point should attract to
    /// </summary>
    /// <param name="target">The target to attract to</param>
    public void Attract(Node2D target)
    {
        EmitSignal(SignalName.AttractorChanged, target);
    }

    /// <summary>
    /// Set the value for this experience point, this is used for initial setup only
    /// </summary>
    /// <param name="points">The points to grant if this experience point is collected</param>
    public void SetExperiencePoints(int points)
    {
        experienceAmount = points;
    }
}

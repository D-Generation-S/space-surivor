using System.Linq;
using System.Threading.Tasks;
using Godot;

public partial class MultiTarget : BaseTarget
{
    [Export]
    private BaseTarget[] targets;

    private int targetCounter;

    public override void _Ready()
    {
        targetCounter = targets.Length;
        base._Ready();
    }

    protected override void ToggleTargetVisibility(bool newState)
    {
        foreach(var target in targets.OfType<BaseTarget>())
        {
            if (target is null)
            {
                continue;
            }
            if (newState)
            {
                target.MakeTargetVisible();
                continue;
            }
            target.HideTarget();
        }
        base.ToggleTargetVisibility(false);
    }

    public void TargetWasCompleted()
    {
        targetCounter--;
        if (targetCounter <= 0)
        {
            EmitSignal(SignalName.TargetCompleted);
            QueueFree();
        }
    }
}
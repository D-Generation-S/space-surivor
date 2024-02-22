using Godot;

/// <summary>
/// A initial state to setup a blackboard for a FSM
/// </summary>
public partial class EnemyAiSetupState : State
{
    /// <summary>
    /// The blackboard key for the ai to flight computer adapter
    /// </summary>
    [Export]
    private string aiToFlightComputerBlackboardKey = "aiToFlightComputer";

    /// <summary>
    /// The ai to flight computer to use
    /// </summary>
    [Export]
    private AiToFlightComputer aiToFlightComputer;

    /// <summary>
    /// The next state to went to if this state is completed
    /// </summary>
    [Export]
    private State nextState;

    /// <inheritdoc />
    public override void Enter()
    {
        if (blackboard is null)
        {
            GD.PushError("Requires blackboard");
            return;
        }
        if (aiToFlightComputer is null || nextState is null)
        {
            return;
        }
        blackboard.SetData(aiToFlightComputerBlackboardKey, aiToFlightComputer);

        EmitSignal(SignalName.Transitioned, this, nextState.GetType().Name);
    }
}

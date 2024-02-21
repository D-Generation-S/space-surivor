using Godot;

public partial class EnemyAiSetupState : State
{
	[Export]
	private string aiToFlightComputerBlackboardKey = "aiToFlightComputer";

	[Export]
	private AiToFlightComputer aiToFlightComputer;

	[Export]
	private State nextState;

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

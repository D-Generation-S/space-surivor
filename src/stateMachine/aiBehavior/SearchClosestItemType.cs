using Godot;

public partial class SearchClosestItemType : State
{
	[Export]
	private string blackboardKeyName;

	[Export]
	private string groupName;
	
	[Export]
	private Node2D referenceNode;

	[Export]
	private State nextState;
	
    [Export]
    private int searchAttempts = 5;

	[Export]
	private State pauseState;

	private int currentAttempt;

    // Called when the node enters the scene tree for the first time.
    public override void Enter()
    {
		if (blackboard is null)
		{
			GD.PushError("Requires blackboard");
			return;
		}
		currentAttempt = 0;
    }

    public override void Update(double delta)
    {
		if (currentAttempt > searchAttempts && pauseState is not null)
		{
			EmitSignal(SignalName.Transitioned, this, pauseState.GetType().Name);
		}
		var entities = GetTree().Root.GetNodesInGroup<Node2D>(groupName, true);
		float distance = float.MaxValue;
		Node2D foundEntity = null;
		foreach(var entity in entities) 
		{
			float currentDistance = referenceNode.GlobalPosition.DistanceTo(entity.GlobalPosition);
			if (currentDistance < distance)
			{
				distance = currentDistance;
				foundEntity = entity;
			}
		}
		if ( foundEntity is not null && nextState is not null)
		{
			blackboard.SetData(blackboardKeyName, foundEntity);
			EmitSignal(SignalName.Transitioned, this, nextState.GetType().Name);
		}

		currentAttempt++;

    }
}

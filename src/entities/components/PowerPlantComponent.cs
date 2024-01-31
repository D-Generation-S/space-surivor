using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class PowerPlantComponent : Node
{
	[Export]
	private PowerPlantConfiguration plantConfiguration;
	

	private List<ConsumerComponent> consumers;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ComponentsChanged();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ComponentsChanged()
	{
		consumers = GetParent().GetChildren().OfType<ConsumerComponent>().OrderBy(component => component.GetPriority()).ToList();
		int currentConsumption = 0;
		foreach (var consumer in consumers)
		{
			var consumerConsumption = consumer.GetConsumption();
			if (currentConsumption + consumerConsumption > plantConfiguration.GetCapacity())
			{
				consumer.Disable();
				continue;
			}
			consumer.Enable();
			currentConsumption += consumer.GetConsumption();
		}
	}
}

using System.Linq;
using Godot;

public partial class UserInterface : Control
{
	[Export]
	private Node player;

	private ProgressBar health;

	private ProgressBar heat;

	private Label flightMode;

	private FlightComputer playerFlightComputer;

	private HealthComponent playerHealthComponent;

	private CoolingUnitComponent playerCoolingUnit;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		health = GetNode<ProgressBar>("%HealthBar");
		heat = GetNode<ProgressBar>("%HeatBar");
		flightMode = GetNode<Label>("%CurrentFlightMode");

		var playerComponents = player.GetNode("./Components").GetChildren();

		playerFlightComputer = playerComponents.OfType<FlightComputer>().FirstOrDefault();
		playerHealthComponent = playerComponents.OfType<HealthComponent>().FirstOrDefault();
		playerCoolingUnit = playerComponents.OfType<CoolingUnitComponent>().FirstOrDefault();

		playerHealthComponent.MaxHealthChanged += _ => UpdateMaxHealth();
		playerHealthComponent.TookDamage += _ => UpdateHealth();
		playerCoolingUnit.MaxHeatCapacityChanged += _ => UpdateMaxHeat();
		playerCoolingUnit.StoredHeatChanged += _ => UpdateHeat();
		playerFlightComputer.FlightModeChanged += _ => UpdateFlightMode();

		UpdateMaxHealth();
		UpdateHealth();
		UpdateMaxHeat();
		UpdateHeat();

		UpdateFlightMode();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateMaxHealth()
	{
		health.MaxValue = playerHealthComponent.GetMaxHealth();
	}

	public void UpdateHealth()
	{
		health.Value = playerHealthComponent.GetHealth();
	}	
	
	public void UpdateMaxHeat()
	{
		heat.MaxValue = playerCoolingUnit.GetMaxHeatCapacity();
	}

	public void UpdateHeat()
	{
		heat.Value = playerCoolingUnit.GetStoredHeat();
	}

	public void UpdateFlightMode()
	{
		flightMode.Text = playerFlightComputer.GetActiveComputerMode().GetDisplayName();
	}
}

using System;
using System.Linq;
using Godot;

public partial class UserInterface : Control
{
	[Export]
	private Node player;

	private ProgressBar health;

	private ProgressBar powerConsumption;
	
	private ProgressBar battery;

	private ProgressBar heat;

	private Label flightMode;

	private FlightComputer playerFlightComputer;

	private HealthComponent playerHealthComponent;

	private CoolingUnitComponent playerCoolingUnit;

	private PowerPlantComponent powerPlant;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		health = GetNode<ProgressBar>("%HealthBar");
		heat = GetNode<ProgressBar>("%HeatBar");
		powerConsumption = GetNode<ProgressBar>("%PowerUsageBar");
		battery = GetNode<ProgressBar>("%BatteryLevelBar");
		flightMode = GetNode<Label>("%CurrentFlightMode");

		var playerComponents = player.GetNode("./Components").GetChildren();

		playerFlightComputer = playerComponents.OfType<FlightComputer>().FirstOrDefault();
		playerHealthComponent = playerComponents.OfType<HealthComponent>().FirstOrDefault();
		playerCoolingUnit = playerComponents.OfType<CoolingUnitComponent>().FirstOrDefault();
		powerPlant = playerComponents.OfType<PowerPlantComponent>().FirstOrDefault();

		playerHealthComponent.MaxHealthChanged += _ => UpdateMaxHealth();
		playerHealthComponent.TookDamage += _ => UpdateHealth();
		playerCoolingUnit.MaxHeatCapacityChanged += _ => UpdateMaxHeat();
		playerCoolingUnit.StoredHeatChanged += _ => UpdateHeat();
		playerFlightComputer.FlightModeChanged += _ => UpdateFlightMode();
		
		powerPlant.UsedPlantPowerChanged += UpdatePowerConsumption;
		powerPlant.MaxBatteryCapacityChanged += _ => UpdateMaxBatteryCapacity();
		powerPlant.BatteryCapacityChanged += _ => UpdateBatteryCapacity();

		UpdateMaxHealth();
		UpdateHealth();
		UpdateMaxBatteryCapacity();
		UpdateBatteryCapacity();
		UpdatePowerConsumption(0);
		UpdateMaxHeat();
		UpdateHeat();

		UpdateFlightMode();
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	

    private void UpdatePowerConsumption(float percentage)
    {
		percentage = Math.Clamp(percentage, 0, 1);
		powerConsumption.Value = powerConsumption.MaxValue * percentage;
    }	
	
	public void UpdateMaxBatteryCapacity()
	{
		battery.MaxValue = powerPlant.GetMaxBatteryCapacity();
		bool visible = battery.MaxValue > 0;
		battery.SetDeferred("visible", visible);
		GetNode<TextureRect>("%BatteryLevel").SetDeferred("visible", visible);
	}

	public void UpdateBatteryCapacity()
	{
		battery.Value = powerPlant.GetCurrentBatteryCapacity();
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

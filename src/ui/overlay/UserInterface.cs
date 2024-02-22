using System;
using System.Linq;
using Godot;

/// <summary>
/// Class for the user interface
/// </summary>
public partial class UserInterface : Control
{
    /// <summary>
    /// The player node to get all the components from
    /// </summary>
    [Export]
    private Node player;

    /// <summary>
    /// The health progress bar
    /// </summary>
    private ProgressBar health;

    /// <summary>
    /// The power consumption progress bar
    /// </summary>
    private ProgressBar powerConsumption;

    /// <summary>
    /// The battery progress bar
    /// </summary>	
    private ProgressBar battery;

    /// <summary>
    /// The heat progress bar
    /// </summary>
    private ProgressBar heat;

    /// <summary>
    /// The label for the flight mode
    /// </summary>
    private Label flightMode;

    /// <summary>
    /// Reference to the player flight computer
    /// </summary>
    private FlightComputer playerFlightComputer;

    /// <summary>
    /// Reference to the player health component
    /// </summary>
    private HealthComponent playerHealthComponent;

    /// <summary>
    /// Reference to the player cooling aggregate
    /// </summary>
    private CoolingUnitComponent playerCoolingUnit;

    /// <summary>
    /// Reference to the player power plant
    /// </summary>
    private PowerPlantComponent powerPlant;

    /// <summary>
    /// The label used to display the current experience points
    /// </summary>
    private Label experienceCount;

    /// <summary>
    /// The experience bar used to display the progress to the next level
    /// </summary>
    private ProgressBar experienceBar;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        health = GetNode<ProgressBar>("%HealthBar");
        heat = GetNode<ProgressBar>("%HeatBar");
        powerConsumption = GetNode<ProgressBar>("%PowerUsageBar");
        battery = GetNode<ProgressBar>("%BatteryLevelBar");
        flightMode = GetNode<Label>("%CurrentFlightMode");
        experienceBar = GetNode<ProgressBar>("%ExperienceBar");
        experienceCount = GetNode<Label>("%ExperienceCount");

        experienceCount.Text = "0";

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
    
    /// <summary>
    /// Update the power consumption bar
    /// </summary>
    /// <param name="percentage">The percentage of power used</param>
    private void UpdatePowerConsumption(float percentage)
    {
        percentage = Math.Clamp(percentage, 0, 1);
        powerConsumption.Value = powerConsumption.MaxValue * percentage;
    }	
    
    /// <summary>
    /// Update the max battery capacity bar
    /// </summary>
    public void UpdateMaxBatteryCapacity()
    {
        battery.MaxValue = powerPlant.GetMaxBatteryCapacity();
        bool visible = battery.MaxValue > 0;
        battery.SetDeferred("visible", visible);
        GetNode<TextureRect>("%BatteryLevel").SetDeferred("visible", visible);
    }

    /// <summary>
    /// Update the battery bar used capacity
    /// </summary>
    public void UpdateBatteryCapacity()
    {
        battery.Value = powerPlant.GetCurrentBatteryCapacity();
    }

    /// <summary>
    /// Update the max health bar
    /// </summary>
    public void UpdateMaxHealth()
    {
        health.MaxValue = playerHealthComponent.GetMaxHealth();
    }

    /// <summary>
    /// Update the current health for the health bar
    /// </summary>
    public void UpdateHealth()
    {
        health.Value = playerHealthComponent.GetHealth();
    }	
    
    /// <summary>
    /// Update the max heat which can be stored by the ship
    /// </summary>
    public void UpdateMaxHeat()
    {
        heat.MaxValue = playerCoolingUnit.GetMaxHeatCapacity();
    }

    /// <summary>
    /// Update the amount of heat already stored
    /// </summary>
    public void UpdateHeat()
    {
        heat.Value = playerCoolingUnit.GetStoredHeat();
    }

    /// <summary>
    /// Update the flight mode currently used
    /// </summary>
    public void UpdateFlightMode()
    {
        flightMode.Text = playerFlightComputer.GetActiveComputerMode().GetDisplayName();
    }

    /// <summary>
    /// Method to change the experience required to get to the next level
    /// </summary>
    /// <param name="newRequiredExperienceAmount">The new amount of experience required to level up>/param>
    public void RequiredExperienceChanged(int newRequiredExperienceAmount)
    {
        experienceBar ??= GetNode<ProgressBar>("%ExperienceBar");
        experienceBar.MaxValue = newRequiredExperienceAmount;
    }

    /// <summary>
    /// The amount of experience the player got has changed
    /// </summary>
    /// <param name="newExperienceAmount">The new amount of experience for the player</param>
    public void ExperienceChanged(int newExperienceAmount)
    {
        experienceBar.Value = newExperienceAmount;
        experienceCount.Text = newExperienceAmount.ToString();
    }

    /// <summary>
    /// The level of the player did change
    /// </summary>
    /// <param name="newLevel">The new level of the player</param>
    public void LevelChanged(int newLevel)
    {

    }

    /// <summary>
    /// Show the interface
    /// </summary>
    public void ShowInterface()
    {
        ToggleInterface(true);
    }

    /// <summary>
    /// Hide the interface
    /// </summary>
    public void HideInterface()
    {
        ToggleInterface(false);
    }

    /// <summary>
    /// Toggle the visibility of this interface
    /// </summary>
    /// <param name="newState">The new interface state</param>
    public void ToggleInterface(bool newState)
    {
        Visible = newState;
    }
}

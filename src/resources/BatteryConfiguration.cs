using Godot;

[GlobalClass]
public partial class BatteryConfiguration : Resource
{
    [Export]
    private int batteryCapacity;

    [Export]
    private int maxLoadAmount;

    [Export]
    private int loadHeat;

    [Export]
    private int maxUnloadAmount;

    public int GetCapacity()
    {
        return batteryCapacity;
    }

    public int GetMaxLoadAmount()
    {
        return maxLoadAmount;
    }

    public int GetMaxUnloadAmount()
    {
        return maxUnloadAmount;
    }

    public int GetLoadHeat()
    {
        return 50;
    }
}
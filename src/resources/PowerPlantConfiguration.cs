using Godot;

[GlobalClass]
public partial class PowerPlantConfiguration : Resource
{
    [Export]
    private string name;

    [Export]
    private int production;

    [Export]
    private int size;

    public string GetName()
    {
        return name;
    }

    public int GetProduction()
    {
        return production;
    }

    public int GetSize()
    {
        return size;
    }
}

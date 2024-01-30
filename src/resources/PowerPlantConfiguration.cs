using Godot;

[GlobalClass]
public partial class PowerPlantConfiguration : Resource
{
    [Export]
    private string name;

    [Export]
    private int capacity;

    [Export]
    private int size;

    public string GetName()
    {
        return name;
    }

    public int GetCapacity()
    {
        return capacity;
    }

    public int GetSize()
    {
        return size;
    }
}

using Godot;

public partial class GameSessionData : Resource
{
    private int experience;

    public void AddExperience(int amount)
    {
        experience += amount;
    }

    public int GetExperience()
    {
        return experience;
    }

}

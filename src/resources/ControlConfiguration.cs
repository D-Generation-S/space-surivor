using Godot;

/// <summary>
/// Resource method for a control input
/// </summary>
[GlobalClass]
public partial class ControlConfiguration : Resource
{
    /// <summary>
    /// The name of the input
    /// </summary>
    [Export]
    private string inputName;

    /// <summary>
    /// Get the input name for the input mapper
    /// </summary>
    /// <returns>The name of the input</returns>
    public string GetInputName()
    {
        return inputName;
    }
}

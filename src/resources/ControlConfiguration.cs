using Godot;
using System;

[GlobalClass]
public partial class ControlConfiguration : Resource
{
    [Export]
    private string inputName;

    public string GetInputName()
    {
        return inputName;
    }
}

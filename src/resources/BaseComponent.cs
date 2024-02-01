using System;
using Godot;

/// <summary>
/// Base configuration for each component
/// </summary>
public partial class BaseComponent : Resource
{
    /// <summary>
    /// The name of the component
    /// </summary>
    [Export]
    private string name;

    /// <summary>
    /// The description of this component
    /// </summary>
    [Export(PropertyHint.MultilineText)]
    private string description;

    /// <summary>
    /// The icon to show for this component
    /// </summary>
    [Export]
    private Texture2D icon;

    /// <summary>
    /// The size of this component
    /// </summary>
    [Export(PropertyHint.Range, "1, 5")]
    private int slotSize = 1;

    /// <summary>
    /// The slot required by this component
    /// </summary>
    [Export(PropertyHint.Enum, "Core, Internal, Hardpoint")]
    private int componentSlot;

    /// <summary>
    /// Get the name of this component
    /// </summary>
    /// <returns>The name of this component</returns>
    public string GetName()
    {
        return name;
    }

    /// <summary>
    /// Get the description of this component
    /// </summary>
    /// <returns>The description of this component</returns>
    public string GetDescription()
    {
        return name;
    }

    /// <summary>
    /// Get the icon if this component
    /// </summary>
    /// <returns>The icon to display for this component</returns>
    public Texture2D GetIcon()
    {
        return icon;
    }

    /// <summary>
    /// Get the slot size for this component
    /// </summary>
    /// <returns>The required slot size</returns>
    public int GetSlotSize()
    {
        return slotSize;
    }

    /// <summary>
    /// Get the component type for this component
    /// </summary>
    /// <returns>The component type</returns>
    public ComponentType GetComponentType()
    {
        return (ComponentType)componentSlot;
    }

    
}

/// <summary>
/// The available component types
/// </summary>
public enum ComponentType
{
    Core = 0,
    Internal = 1,
    Hardpoint = 2
}
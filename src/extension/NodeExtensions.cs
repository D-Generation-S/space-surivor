using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// Method for the node components
/// @TODO Test all methods inside this class.
/// </summary>
public static class NodeExtensions
{
    /// <summary>
    /// Method to get all the nodes in this specific group
    /// </summary>
    /// <typeparam name="T">The type if the node to scan</typeparam>
    /// <param name="rootNode">The root node to start the scan from</param>
    /// <param name="groupName">The group name to search for</param>
    /// <returns>A list with all the components inside of this group which are child of the given root</returns>
    public static IEnumerable<T> GetNodesInGroup<T>(this Node rootNode, string groupName) where T : Node
    {
        return rootNode.GetNodesInGroup<T>(groupName, false);
    }

    /// <summary>
    /// Method to get all the nodes in this specific group
    /// </summary>
    /// <typeparam name="T">The type if the node to scan</typeparam>
    /// <param name="rootNode">The root node to start the scan from</param>
    /// <param name="groupName">The group name to search for</param>
    /// <param name="recursive">Search sub nodes as well</param>
    /// <returns>A list with all the components inside of this group which are child of the given root</returns>
    public static IEnumerable<T> GetNodesInGroup<T>(this Node rootNode, string groupName, bool recursive) where T : Node
    {
        if (rootNode is null)
        {
            return Enumerable.Empty<T>();
        }

        var nodesOnRootNode = rootNode.GetChildren().Where(childNode => childNode.IsInGroup(groupName)).ToList();
        if (recursive)
        {
            foreach (var childNode in rootNode.GetChildren())
            {
                nodesOnRootNode.AddRange(childNode.GetNodesInGroup<T>(groupName, recursive));
            }
        }

        return nodesOnRootNode.OfType<T>().ToList();
    }
}
using System.Collections.Generic;
using System.Linq;
using Godot;

public static class NodeExtensions
{
    public static IEnumerable<T> GetNodesInGroup<T>(this Node node, string groupName) where T : Node
    {
        return node.GetNodesInGroup<T>(groupName, false);
    }

    public static IEnumerable<T> GetNodesInGroup<T>(this Node node, string groupName, bool recursive) where T : Node
    {
        if (node is null)
        {
            return Enumerable.Empty<T>();
        }

        var nodesOnRootNode = node.GetChildren().Where(childNode => childNode.IsInGroup(groupName)).ToList();
        if (recursive)
        {
            foreach (var childNode in nodesOnRootNode)
            {
                nodesOnRootNode.AddRange(childNode.GetNodesInGroup<T>(groupName, recursive));
            }
        }

        return nodesOnRootNode.OfType<T>().ToList();
    }
}
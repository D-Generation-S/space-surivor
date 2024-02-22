using Godot;
using Godot.Collections;

/// <summary>
/// A object instance used as a blackboard for the state machine
/// </summary>
public class Blackboard
{
    /// <summary>
    /// The dictionary of nodes to store in the blackboard
    /// </summary>
    Dictionary<string, Node> nodes;

    /// <summary>
    /// Constructor of a new blackboard
    /// </summary>
    public Blackboard()
    {
        nodes = new Dictionary<string, Node>();
    }

    /// <summary>
    /// Get a data set from the blackboard, if any 
    /// </summary>
    /// <typeparam name="T">The type of data to cast the found entry to</typeparam>
    /// <param name="key">The key of the entry to get</param>
    /// <returns>The data casted to <c>T</c> or null if nothing was found or value is from another type</returns>
    public T GetData<T>(string key) where T : Node
    {
        Node item  = null;
        try
        {
            item = nodes.ContainsKey(key) ? nodes[key] : null;    
        }
        catch (System.Exception)
        {
            ClearData(key);
        }
        
        if (item is not null && !Node.IsInstanceValid(item))
        {
            item = null;
        }
        return item is null ? default : item as T;
    }

    /// <summary>
    /// Set a data set to the blackboard
    /// </summary>
    /// <param name="key">The key of the data set to write</param>
    /// <param name="data">The data to store</param>
    public void SetData(string key, Node data)
    {
        ClearData(key);
        nodes.Add(key, data);
        data.TreeExiting += () => 
        {
            ClearData(key);
        };
    }

    /// <summary>
    /// Clear a data set from the blackboard, if it exists
    /// </summary>
    /// <param name="key">The key of the item to clear</param>
    public void ClearData(string key)
    {
        if (nodes.ContainsKey(key))
        {
            nodes.Remove(key);
        }
    }
}
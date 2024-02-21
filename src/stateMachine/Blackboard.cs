using Godot;
using Godot.Collections;

public class Blackboard
{
    Dictionary<string, Node> nodes;

    public Blackboard()
    {
        nodes = new Dictionary<string, Node>();
    }

    public T GetData<T>(string name) where T : Node
    {
        Node item  = null;
        try
        {
            item = nodes.ContainsKey(name) ? nodes[name] : null;    
        }
        catch (System.Exception)
        {
            ClearData(name);
        }
        
        if (item is not null && !Node.IsInstanceValid(item))
        {
            item = null;
        }
        return item is null ? default : item as T;
    }

    public void SetData(string name, Node data)
    {
        ClearData(name);
        nodes.Add(name, data);
        data.TreeExiting += () => 
        {
            ClearData(name);
        };
    }

    public void ClearData(string name)
    {
        if (nodes.ContainsKey(name))
        {
            nodes.Remove(name);
        }
    }
}
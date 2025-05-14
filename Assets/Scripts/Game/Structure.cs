using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour, IAttackable
{
    public Axial Position { get; set; }
    private HashSet<Structure> _structures = new HashSet<Structure>();
    private Structure _parent;

    public void TakeDamage()
    {
        // demo
        DestroyAllChildren();
    }

    public void AddChild(Structure structure)
    {
        _structures.Add(structure);
    }

    public void RemoveChild(Structure structure)
    {
        _structures.Remove(structure);
    }

    public HashSet<Structure> GetStructures()
    {
        return _structures;
    }

    public HashSet<Structure> GetAllChildren()
    {
        HashSet<Structure> children = new HashSet<Structure>();
        foreach (Structure child in GetStructures())
        {
            children.Add(child);
            children.UnionWith(child.GetAllChildren());
        }
        return children;
    }

    protected abstract void Remove();

    public void DestroyAllChildren()
    {
        HashSet<Structure> structuresCopy = new HashSet<Structure>(_structures);
        foreach (Structure child in structuresCopy)
        {
            child.DestroyAllChildren();
            child.Remove();
            _structures.Remove(child);
        }
    }
}

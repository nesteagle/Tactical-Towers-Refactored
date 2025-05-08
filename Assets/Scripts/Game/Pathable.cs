using System.Collections.Generic;
using UnityEngine;

public abstract class Pathable : MonoBehaviour
{
    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H;

    public Axial Position { get; set; } // Z = -x -y
    private readonly List<Pathable> _edges;
    public bool Occupied { get; set; }
    public int Weight { get; set; }

    public Pathable()
    {
        Weight = 0;
        Occupied = false;
        _edges = new List<Pathable>();
    }

    public void AddEdge(Pathable p)
    {
        _edges.Add(p);
    }

    public List<Pathable> GetEdges()
    {
        return _edges;
    }

    public void Initialize(Axial a)
    {
        Position = a;
    }
}
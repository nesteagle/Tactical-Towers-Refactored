using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H;

    public Axial Position { get; set; } // Z = -x -y
    private List<Node> _edges;
    public bool Occupied { get; set; }
    public int Weight { get; set; }

    public Node()
    {
        Weight = 0;
        Occupied = false;
        _edges = new List<Node>();
    }

    public void AddEdge(Node p)
    {
        _edges.Add(p);
    }

    public List<Node> GetEdges()
    {
        return _edges;
    }

    public void SetEdges(List<Node> edges)
    {
        _edges = edges;
    }

    public void Initialize(Axial a)
    {
        Position = a;
    }
}
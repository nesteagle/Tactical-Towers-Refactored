using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H;

    public Axial Position { get; set; } // Z = -x -y
    private HashSet<Node> _edges;
    public bool Occupied { get; set; }
    public int Weight { get; set; }

    public Node()
    {
        Weight = 0;
        Occupied = false;
        _edges = new HashSet<Node>();
    }

    public void AddEdge(Node p)
    {
        _edges.Add(p);
    }

    public void RemoveEdge(Node p)
    {
        _edges.Remove(p);
    }

    public HashSet<Node> GetEdges()
    {
        return _edges;
    }

    public void SetEdges(HashSet<Node> edges)
    {
        _edges = edges;
    }

    public void Initialize(Axial a)
    {
        Position = a;
    }
}
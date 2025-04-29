using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H;

    public Vector2Int Position { get; set; } // Z = -x -y
    private readonly List<Node> _adjacentTiles;
    public bool Occupied { get; set; }
    public int Weight { get; set; }

    public Node()
    {
        Weight = 0;
        Occupied = false;
        _adjacentTiles = new List<Node>();
    }

    public void AddAdjacentTile(HexCell cell)
    {
        _adjacentTiles.Add(cell);
    }

    public List<Node> GetAdjacentTiles()
    {
        return _adjacentTiles;
    }

    public void Initialize(Axial a)
    {
        Position = new Vector2Int(a.Q, a.R);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;

    private readonly Dictionary<Axial, HexCell> _cells;
    private readonly int _radius = 12;

    public HexMap()
    {
        GenerateMap(_radius);
    }

    private void GenerateMap(int radius)
    {
        // make symmetrical by doing zero to radius, or -radius to 0
        for (int q = -radius; q <= radius; q++)
        {
            int lowerR = Mathf.Max(-radius, -q - radius);
            int higherR = Mathf.Min(radius, -q + radius);
            for (int r = lowerR; r <= higherR; r++)
            {
                AddCell(q, r);
            }
        }
        GenerateAdjacentTiles();
    }

    private void GenerateAdjacentTiles()
    {
        foreach (Axial axial in _cells.Keys)
        {
            HexCell c = _cells[axial];
            for (int i = 0; i < 6; i++)
            {
                int q = axial.Q + HexDirections.Directions[i].Q;
                int r = axial.R + HexDirections.Directions[i].R;

                if (_cells.TryGetValue(new Axial(q, r), out HexCell adjacentCell))
                {
                    c.AddAdjacentTile(adjacentCell);
                }
            }
        }
    }

    private void AddCell(int q, int r)
    {
        Axial a = new(q, r);
        HexCell c = new(q, r);
        _cells.Add(a, c);
    }

    public HexCell GetCell(int q, int r)
    {
        if (_cells.TryGetValue(new Axial(q, r), out HexCell cell))
        {
            return cell;
        }
        return null;
    }
}

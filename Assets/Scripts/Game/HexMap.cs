using System;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;

    private Dictionary<Axial, HexCell> _cells;
    private readonly int _radius = 12;
    private const int ZERO = 0;

    private void Awake()
    {
        _cells = new Dictionary<Axial, HexCell>();
        GenerateMap(_radius);
        GenerateAdjacentTiles();
        GenerateTerrain();
        Debug.Log("Generated map");
    }

    private void GenerateMap(int radius)
    {
        for (int q = -radius; q <= radius; q++)
        {
            int lowerR = Mathf.Max(-radius, -q - radius);
            int higherR = Mathf.Min(radius, -q + radius);
            for (int r = lowerR; r <= higherR; r++)
            {
                AddCell(q, r);
            }
        }
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

    private void GenerateTerrain()
    {
        foreach (Axial axial in _cells.Keys)
        {
            if (axial.Q < 0 || (axial.Q == 0 && axial.R < 0))
            {
                continue;
            }
            HexCell cell = _cells[axial];
            HexCell oppositeCell = GetCell(-axial.Q, -axial.R);

            Terrain t = GetRandomTerrain();

            cell.SetTerrain(t);
            oppositeCell.SetTerrain(t);
        }
    }

    private Terrain GetRandomTerrain()
    {
        int randomValue = UnityEngine.Random.Range(0, TerrainChances.TotalChance);
        if (randomValue < TerrainChances.PlainChance)
        {
            return Terrain.Plain;
        }
        else if (randomValue < TerrainChances.PlainChance + TerrainChances.ForestChance)
        {
            return Terrain.Forest;
        }
        else
        {
            return Terrain.Mountain;
        }
    }

    private void AddCell(int q, int r)
    {
        Axial a = new(q, r);

        Vector3 position = new(a.Q * (HexData.InnerRadius * 2f) + (a.R * HexData.InnerRadius), a.R * (HexData.OuterRadius * 1.5f), 10f);

        GameObject cellObj = Instantiate(_cellPrefab);
        cellObj.transform.SetParent(transform);
        cellObj.transform.localPosition = position;
        HexCell cell = cellObj.GetComponent<HexCell>();
        if (GetCell(q, r) == null)
        {
            _cells.Add(a, cell);
        }
        cell.Initialize(a);
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

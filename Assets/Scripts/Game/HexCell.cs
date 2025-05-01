using System.Collections.Generic;
using UnityEngine;

public class HexCell : Pathable
{
    private Terrain _type;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _type = Terrain.Plain;
        _renderer = GetComponent<SpriteRenderer>();
    }

    public Terrain GetTerrain()
    {
        return _type;
    }

    public void SetTerrain(Terrain type)
    {
        _type = type;
        SetColor(GetTileColor(_type));
    }

    private void SetColor(Color c)
    {
        _renderer.color = c;
    }

    public void AddAdjacentTile(HexCell tile)
    {
        AddEdge(tile);
    }

    private Color GetTileColor(Terrain type)
    {
        switch (type)
        {
            case Terrain.Plain:
                return Color.white;
            case Terrain.Forest:
                return Color.green;
            case Terrain.Mountain:
                return Color.gray;
            default:
                return Color.white;
        }
    }
}

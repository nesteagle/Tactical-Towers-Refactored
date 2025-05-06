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
        SetColor(TerrainUtil.GetTerrainColor(type));
        Occupied = TerrainUtil.IsTerrainObstructed(type);
    }

    private void SetColor(Color c)
    {
        _renderer.color = c;
    }

    public void AddAdjacentTile(HexCell tile)
    {
        AddEdge(tile);
    }
}

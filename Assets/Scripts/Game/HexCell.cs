using UnityEngine;

public class HexCell : Node
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
        Occupied = TerrainUtil.IsTerrainObstructed(type);
        ResetColor();
    }

    public void SetColor(Color c)
    {
        _renderer.color = c;
    }

    public void ResetColor()
    {
        SetColor(TerrainUtil.GetTerrainColor(_type));
    }
}

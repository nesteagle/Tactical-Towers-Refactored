using UnityEngine;

public class TerrainUtil : MonoBehaviour
{
    public static Color GetTerrainColor(Terrain type)
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

    public static bool IsTerrainObstructed(Terrain type)
    {
        if (type == Terrain.Mountain) return true;
        else return false;
    }
}

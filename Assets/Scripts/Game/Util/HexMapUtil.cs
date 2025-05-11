using System.Collections.Generic;
using UnityEngine;

public static class HexMapUtil
{
    private const float Z = 0f;
    public static Axial GetCellAxialFromPosition(Vector3 position)
    {
        float x = position.x;
        float y = position.y;

        int r = Mathf.RoundToInt(y / (HexData.OuterRadius * 1.5f));
        int q = Mathf.RoundToInt((x / HexData.InnerRadius - r) / 2f);

        return new Axial(q, r);
    }

    public static Vector3 GetCellPositionFromAxial(Axial a)
    {
        return new(HexData.InnerRadius * (2f * a.Q + a.R), a.R * (1.5f * HexData.OuterRadius), Z);
    }

    public static HashSet<HexCell> GetCellsInRange(Axial center, int range)
    {
        HashSet<HexCell> cells = new();
        for (int q = -range; q <= range; q++)
        {
            for (int r = Mathf.Max(-range, -q - range); r <= Mathf.Min(range, -q + range); r++)
            {
                Axial axial = new(q + center.Q, r + center.R);
                cells.Add(HexMap.GetCell(axial));
            }
        }
        return cells;
    }

    public static int GetDistance(Vector3Int startPosition, Vector3Int targetPosition)
    {
        return Mathf.Max(Mathf.Abs(startPosition.z - targetPosition.z), Mathf.Max(Mathf.Abs(startPosition.x - targetPosition.x), Mathf.Abs(startPosition.y - targetPosition.y)));
    }

    public static int GetDistance(Axial startPosition, Axial targetPosition)
    {
        return GetDistance(startPosition.ToCubic(), targetPosition.ToCubic());
    }
}
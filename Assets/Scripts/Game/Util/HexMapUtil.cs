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
}
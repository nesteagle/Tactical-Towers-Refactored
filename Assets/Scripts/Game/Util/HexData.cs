public static class HexData
{
    public const float HexRadius = 0.5f;
    public const float GapOffset = HexRadius * 0.075f;
    public const float OuterRadius = HexRadius + GapOffset;
    public const float InnerRadius = OuterRadius * 0.86602540378f; // Outer * sqrt(3) / 2
}
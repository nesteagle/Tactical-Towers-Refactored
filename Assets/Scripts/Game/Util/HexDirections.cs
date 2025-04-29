public static class HexDirections
{
    public static readonly Axial[] Directions = new Axial[]
    {
        new(0, -1), // up-left
        new(1, -1), // up-right
        new(1, 0),  // right
        new(0, 1),  // down-right
        new(-1, 1), // down-left
        new(-1, 0)  // left
    };
}

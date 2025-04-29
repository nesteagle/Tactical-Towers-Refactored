public static class HexDirections
{
    public static readonly Axial[] Directions = new Axial[]
    {
        new Axial(0, -1), // up-left
        new Axial(1, -1), // up-right
        new Axial(1, 0),  // right
        new Axial(0, 1),  // down-right
        new Axial(-1, 1), // down-left
        new Axial(-1, 0)  // left
    };
}

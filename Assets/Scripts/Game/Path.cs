using System.Collections.Generic;
using UnityEngine;

public class Path : Structure
{
    private Building _origin;
    private Building _dest;
    private List<Node> _path;
    private LineRenderer _line;

    public void Initialize(Building from, Building to)
    {
        _origin = from;
        _dest = to;
        Position = from.Position; // set path position to the origin

        _line = to.GetComponent<LineRenderer>(); // Get the LineRenderer component from the "to" building
        DrawPath();
    }

    private void DrawPath()
    {
        _path = Pathfinding.FindPath(HexMap.GetCell(_origin.Position), HexMap.GetCell(_dest.Position));

        _line.positionCount = _path.Count;

        List<Vector3> pathPositions = new();
        foreach (Node p in _path)
        {
            Axial axialPos = p.Position;
            Vector3 pos = HexMapUtil.GetCellPositionFromAxial(axialPos);
            GameMap.SetAttackableStructure(axialPos, this);
            pos.z = 0;
            pathPositions.Add(pos);
        }
        _line.SetPositions(pathPositions.ToArray());
        _line.enabled = true;
    }

    protected override void Remove()
    {
        // TODO: refactor to fix removing stacked paths from structure map
        foreach (Node n in _path)
        {
            GameMap.RemoveAttackableStructure(n.Position);
        }
        _origin.RemoveChild(this);
        Destroy(gameObject);
        Destroy(this);
    }
}

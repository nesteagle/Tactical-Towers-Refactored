using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour, IAttackable
{
    private Building _origin;
    private Building _dest;
    private List<Node> _path;
    private LineRenderer _line;

    public void Initialize(Building from, Building to)
    {
        _origin = from;
        _dest = to;
        if (from == null || to == null)
        {
            return;
        }
        _line = GetComponent<LineRenderer>();
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
            GameMap.AddAttackable(axialPos, this);
            pos.z = 0;
            pathPositions.Add(pos);
        }
        _line.SetPositions(pathPositions.ToArray());
        _line.enabled = true;
    }

    public void TakeDamage()
    {
        // stub
        Debug.Log("Path attacked");
    }
}

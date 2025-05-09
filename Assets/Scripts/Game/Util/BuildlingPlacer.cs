using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.PlayerSettings;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _buildingPrefab;
    private Dictionary<Axial, Node> _buildings = new();

    public void PlaceBuilding(HexCell c)
    {
        if (c.Occupied)
        {
            Debug.Log("Cell is occupied");
            return;
        }
        if (GetBuilding(c.Position) != null)
        {
            Debug.Log("Cell already has a building");
            return;
        }
        AddBuilding(c);
        //establish building connections
    }

    public Node GetBuilding(Axial a)
    {
        if (_buildings.TryGetValue(a, out Node n))
        {
            return n;
        }
        return null;
    }

    private void AddBuilding(HexCell c)
    {
        GameObject buildingObject = Instantiate(_buildingPrefab);
        buildingObject.transform.position = c.transform.position;
        Node n = buildingObject.GetComponent<Node>();
        _buildings.Add(c.Position, n);
        AddPathTo(c.Position);
    }

    public void RemoveBuilding(Axial a)
    {
        if (_buildings.TryGetValue(a, out Node n))
        {
            Destroy(n.gameObject);
            _buildings.Remove(a);
        }
    }

    private void AddPathTo(Axial pos)
    {
        Axial closestPos = GetClosestBuilding(pos);
        if (closestPos == null)
        {
            Debug.Log("No closest building found");
            return;
        }
        _buildings[pos].AddEdge(_buildings[closestPos]);
        CreatePathBetween(closestPos, pos);
    }

    private Axial GetClosestBuilding(Axial origin)
    {
        Axial closest = null;
        int closestDistance = int.MaxValue;
        foreach (Axial a in _buildings.Keys)
        {
            if (a.Equals(origin))
            {
                continue;
            }
            int distance = Pathfinding.GetEstimatedPathCost(origin, a);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = a;
            }
        }
        return closest;
    }

    private void CreatePathBetween(Axial origin, Axial dest)
    {
        GameObject buildingObj = _buildings[dest].gameObject;
        List<Pathable> path = Pathfinding.FindPath(HexMap.GetCell(origin), HexMap.GetCell(dest));

        LineRenderer line = buildingObj.GetComponent<LineRenderer>();
        line.positionCount = path.Count;

        List<Vector3> pathPositions = new();
        foreach (Pathable p in path)
        {
            Vector3 pos = HexMapUtil.GetCellPositionFromAxial(p.Position);
            pos.z = 0;
            pathPositions.Add(pos);
        }
        line.SetPositions(pathPositions.ToArray());
        line.enabled = true;
    }
}

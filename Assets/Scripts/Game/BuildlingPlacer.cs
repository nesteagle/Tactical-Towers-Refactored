using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private GameObject buildingPrefab;
    private Dictionary<Axial, Building> buildings = new();

    public void PlaceBuilding(HexCell cell)
    {
        if (cell.Occupied)
        {
            Debug.Log("Cell is occupied");
            return;
        }
        if (GetBuilding(cell.Position) != null)
        {
            Debug.Log("Cell already has a building");
            return;
        }
        AddBuilding(cell);
    }

    public Building GetBuilding(Axial axialPosition)
    {
        buildings.TryGetValue(axialPosition, out Building node);
        return node;
    }

    private void AddBuilding(HexCell cell)
    {
        Axial cellPos = cell.Position;
        GameObject buildingObject = Instantiate(buildingPrefab);
        buildingObject.transform.position = cell.transform.position;
        Building node = buildingObject.GetComponent<Building>();
        node.Position = cellPos;
        buildings.Add(cellPos, node);
        GameMap.AddAttackable(cellPos, node);
        AddPathTo(cellPos);
    }

    public void RemoveBuilding(Axial axialPosition)
    {
        if (buildings.ContainsKey(axialPosition))
        {
            Building node = buildings[axialPosition];
            Destroy(node.gameObject);
            buildings.Remove(axialPosition);
        }
    }

    private void AddPathTo(Axial position)
    {
        if (!buildings.ContainsKey(position))
        {
            Debug.Log("Building not found");
            return;
        }
        Axial closestPosition = GetClosestBuilding(position);
        if (closestPosition == null)
        {
            Debug.Log("No closest building found");
            return;
        }
        buildings[position].AddEdge(buildings[closestPosition]);
        CreatePathBetween(closestPosition, position);
    }

    private Axial GetClosestBuilding(Axial origin)
    {
        Axial closest = null;
        int closestDistance = int.MaxValue;
        foreach (Axial axialPosition in buildings.Keys)
        {
            if (axialPosition.Equals(origin))
            {
                continue;
            }
            int distance = HexMapUtil.GetDistance(origin, axialPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = axialPosition;
            }
        }
        return closest;
    }

    private void CreatePathBetween(Axial origin, Axial destination)
    {
        Building destinationNode = buildings[destination];
        Path path = destinationNode.GetComponent<Path>();
        path.Initialize(buildings[origin], buildings[destination]);
    }
}

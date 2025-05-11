using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMap : MonoBehaviour
{
    [SerializeField] private GameObject _buildingPrefab;
    private static Dictionary<Axial, Building> _buildings = new();

    public void PlaceBuilding(HexCell cell)
    {
        if (cell.Occupied || GetBuilding(cell.Position) != null)
            return; // invalid placement attempt
        CreateBuilding(cell);
    }

    public static Building GetBuilding(Axial axialPosition)
    {
        _buildings.TryGetValue(axialPosition, out Building building);
        return building;
    }

    private void CreateBuilding(HexCell cell)
    {
        Axial cellPosition = cell.Position;
        GameObject newBuildingGameObject = Instantiate(_buildingPrefab, cell.transform.position, Quaternion.identity);
        Building newBuilding = newBuildingGameObject.GetComponent<Building>();
        newBuilding.Position = cellPosition;
        _buildings.Add(cellPosition, newBuilding);
        GameMap.AddAttackable(cellPosition, newBuilding);
        AddPathTo(newBuilding);
    }

    public static void RemoveBuilding(Axial axialPosition)
    {
        if (_buildings.TryGetValue(axialPosition, out Building building))
        {
            Destroy(building.gameObject);
            _buildings.Remove(axialPosition);
        }
    }

    private void AddPathTo(Building newBuilding)
    {
        Axial closestBuildingPosition = GetClosestBuilding(newBuilding.Position);
        if (closestBuildingPosition == null)
        {
            Debug.Log("No closest building found");
            return;
        }
        Building closestBuilding = _buildings[closestBuildingPosition];
        Path path = closestBuilding.GetComponent<Path>();
        path.Initialize(closestBuilding, newBuilding);
    }

    private Axial GetClosestBuilding(Axial origin)
    {
        Axial closest = null;
        int closestDistance = int.MaxValue;
        foreach (Axial axialPosition in _buildings.Keys)
        {
            if (axialPosition.Equals(origin))
                continue;
            int distance = HexMapUtil.GetDistance(origin, axialPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = axialPosition;
            }
        }
        return closest;
    }
}

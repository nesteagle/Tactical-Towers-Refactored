using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private HexMap _map;
    [SerializeField] private UnitManager _manager;
    [SerializeField] private BuildingMap _buildingPlacer;

    private void Awake()
    {
        _map.Initialize();

        // demo below:

        _buildingPlacer.PlaceBuilding(HexMap.GetCell(new Axial(0, -8)));

        Attacker u = _manager.CreateUnit(HexMap.GetCell(new Axial(0, 0)));
        //u.MoveTo(HexMap.GetCell(new Axial(5, 2)));
    }
}

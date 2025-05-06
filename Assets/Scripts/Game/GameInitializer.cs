using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private HexMap _map;
    [SerializeField] private UnitManager _manager;

    private void Awake()
    {
        _map.Initialize();

        // demo below:

        Unit u = _manager.CreateUnit(HexMap.GetCell(new Axial(0,0)));
        u.MoveTo(HexMap.GetCell(new Axial(5, 2)));
    }
}

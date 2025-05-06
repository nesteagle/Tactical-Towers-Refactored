using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab;

    private readonly List<Unit> _units = new();
    private const int Z = 0;

    public Unit CreateUnit(HexCell c)
    {
        Vector3 cellPos = c.transform.position;
        GameObject unitObject = Instantiate(_unitPrefab);
        Unit unit = unitObject.GetComponent<Unit>();
        AddUnit(unit);
        unit.SetLocation(c.Position);
        unit.transform.position = new Vector3(cellPos.x,cellPos.y, Z);
        return unit;
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
    }

    public void RemoveUnit(Unit unit) { 
        _units.Remove(unit);
    }
}

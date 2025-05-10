using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab;

    private readonly List<Attacker> _units = new();
    private const int Z = 0;

    public Attacker CreateUnit(HexCell c)
    {
        Vector3 cellPos = c.transform.position;
        GameObject unitObject = Instantiate(_unitPrefab);
        Attacker unit = unitObject.GetComponent<Attacker>();
        AddUnit(unit);
        unit.Position = c.Position;
        unit.transform.position = new Vector3(cellPos.x, cellPos.y, Z);
        unit.State = UnitState.Rest;
        return unit;
    }

    public void AddUnit(Attacker unit)
    {
        _units.Add(unit);
    }

    public void RemoveUnit(Attacker unit)
    {
        _units.Remove(unit);
    }

    private void FixedUpdate()
    {
        foreach (Attacker unit in _units) {
            unit.TryAttack();
        }
    }
}

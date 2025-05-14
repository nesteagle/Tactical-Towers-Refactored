using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    private static Dictionary<Axial, Structure> _structures = new();
    private static Dictionary<Axial, Unit> _units = new();

    public static void SetAttackableUnit(Axial pos, Unit attackable)
    {
        if (_units.ContainsKey(pos))
        {
            _units[pos] = attackable;
        } else
        {
            _units.Add(pos, attackable);
        }
    }

    public static void RemoveAttackableUnit(Axial pos)
    {
        _units.Remove(pos);
    }

    public static void SetAttackableStructure(Axial pos, Structure attackable)
    {
        if (_structures.ContainsKey(pos))
        {
            _structures[pos] = attackable;
        }
        else
        {
            _structures.Add(pos, attackable);
        }
    }

    public static void RemoveAttackableStructure(Axial pos)
    {
        _structures.Remove(pos);
    }

    public static List<IAttackable> GetTargetsInRange(Axial position, int tileRange)
    {
        HexCell cell = HexMap.GetCell(position);
        List<IAttackable> targetsInRange = new();

        foreach (Axial targetPos in _units.Keys)
        {
            if (HexMapUtil.GetDistance(cell.Position, targetPos) <= tileRange)
            {
                targetsInRange.Add(_units[targetPos]);
            }
        }
        foreach (Axial targetPos in _structures.Keys)
        {
            if (HexMapUtil.GetDistance(cell.Position, targetPos) <= tileRange)
            {
                targetsInRange.Add(_structures[targetPos]);
            }
        }
        return targetsInRange;
    }

    public static Unit GetAttackableUnit(Axial position)
    {
        if (_units.TryGetValue(position, out Unit u))
            return u;
        return null;
    }

    public static Structure GetAttackableStructure(Axial position)
    {
        if (_structures.TryGetValue(position, out Structure s))
            return s;
        return null;
    }
}

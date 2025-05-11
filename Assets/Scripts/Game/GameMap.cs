using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    private static Dictionary<Axial, List<IAttackable>> _attackable = new();

    public static Dictionary<Axial, List<IAttackable>> GetAttackable()
    {
        return _attackable;
    }

    public static void AddAttackable(Axial pos, IAttackable attackable)
    {
        if (!_attackable.ContainsKey(pos))
        {
            _attackable[pos] = new List<IAttackable>();
        }
        _attackable[pos].Add(attackable);
    }

    public static void RemoveAttackable(Axial pos)
    {
        if (_attackable.ContainsKey(pos))
        {
            _attackable[pos].RemoveAt(0);
            if (_attackable[pos].Count == 0)
            {
                _attackable.Remove(pos);
            }
        }
    }

    public static void RemoveAttackable(IAttackable attackable)
    {
        foreach (var entry in _attackable)
        {
            if (entry.Value.Contains(attackable))
            {
                entry.Value.Remove(attackable);
                if (entry.Value.Count == 0)
                {
                    _attackable.Remove(entry.Key);
                }
                break;
            }
        }
    }

    public static List<IAttackable> GetTargetsInRange(Axial position, int tileRange)
    {
        HexCell cell = HexMap.GetCell(position);
        List<IAttackable> targetsInRange = new();

        foreach (Axial targetPos in _attackable.Keys)
        {
            if (HexMapUtil.GetDistance(cell.Position, targetPos) <= tileRange)
            {
                targetsInRange.AddRange(_attackable[targetPos]);
            }
        }
        return targetsInRange;
    }
}

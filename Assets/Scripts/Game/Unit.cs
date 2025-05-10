using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IAttackable
{
    public UnitState State { get; set; }
    public Axial Position { get; set; }
    private UnitMover _mover;

    private int _health;

    private void Awake()
    {
        _mover = GetComponent<UnitMover>();
    }

    public int GetHealth()
    {
        return _health;
    }

    public void TakeDamage()
    {
        _health--;
        if (_health <= 0)
        {
            Destroy(gameObject);
            // notify observers
        }
    }

    public void MoveTo(HexCell c)
    {
        _mover.MoveTo(c);
    }
}

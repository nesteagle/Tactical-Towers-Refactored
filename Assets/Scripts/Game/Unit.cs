using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitState State { get; set; }
    private Axial _location;
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

    public void RemoveHealth()
    {
        _health--;
        if (_health <= 0)
        {   
            Destroy(gameObject);
            // notify observers
        }
    }

    public void SetLocation(Axial a)
    {
        _location = a;
    }

    public Axial GetLocation()
    {
        return _location;
    }

    public void MoveTo(HexCell c)
    {
        _mover.MoveTo(c);
    }
}

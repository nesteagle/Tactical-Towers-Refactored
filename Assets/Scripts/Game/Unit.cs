using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitState State { get; set; }

    private int _health;

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
}

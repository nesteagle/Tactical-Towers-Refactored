using UnityEngine;

public class Building : Node, IAttackable
{
    public void TakeDamage()
    {
        // stub
        Debug.Log("Building attacked");
    }
}

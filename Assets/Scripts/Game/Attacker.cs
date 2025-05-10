using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Unit
{
    private const float _cooldown = 1f;
    private float _lastAttackTime = 0f;

    private void Attack(IAttackable target)
    {
        target.TakeDamage();
    }

    public void TryAttack()
    {
        float currentTime = Time.time;
        if (currentTime < _lastAttackTime + _cooldown) {
            return;
        }
        _lastAttackTime = currentTime;
        List<IAttackable> targets = GameMap.GetTargetsInRange(Position, 2);
        // TODO: potentially add sorting/prioritizing of targets here
        if (targets.Count > 0)
        {
            Attack(targets[0]);
            return;
        }
    }
}

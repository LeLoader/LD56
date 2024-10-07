using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RangeAttackState : AttackState
{
    protected override void OnEnter()
    {
        base.OnEnter();

        for (int i = 0; i < attackCount; i++)
        {
            Vector2 offset = target.transform.position - self.transform.position;
            Quaternion direction = Quaternion.LookRotation(Vector3.forward, offset) * Quaternion.Euler(0, 0, 360 / attackCount * (i + 1));

            self.Shoot(target, direction);
        }

    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (timer >= self.attackTime)
        {
            stateController.ChangeState(stateController.repositionState);
        }
    }
}

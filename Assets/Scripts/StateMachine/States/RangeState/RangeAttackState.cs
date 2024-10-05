using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected override void OnEnter()
    {
        base.OnEnter();

        self.Shoot(target);
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

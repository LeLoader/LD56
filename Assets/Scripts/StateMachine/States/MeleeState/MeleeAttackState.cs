using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    int attackCounter = 0;
    protected override void OnEnter()
    {
        base.OnEnter();

        self.attackCollider.enabled = true;
        attackCounter++;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (timer >= self.attackTime)
        {
            if (attackCounter < attackCount)
            {
                stateController.ChangeState(stateController.meleePrepareAttackState);
            }
            else
            {
                attackCounter = 0;
                stateController.ChangeState(stateController.chaseState);
            }
        }
    }

    protected override void OnExit()
    {
        base.OnExit();
        self.attackCollider.enabled = false;
    }
}

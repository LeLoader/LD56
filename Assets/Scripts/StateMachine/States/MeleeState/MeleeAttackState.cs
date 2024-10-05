using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected override void OnEnter()
    {
        base.OnEnter();

        self.attackCollider.enabled = true;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (timer >= self.attackTime)
        {
            stateController.ChangeState(stateController.chaseState);
        }
    }

    protected override void OnExit()
    {
        base.OnExit();

        self.attackCollider.enabled = false;
    }
}

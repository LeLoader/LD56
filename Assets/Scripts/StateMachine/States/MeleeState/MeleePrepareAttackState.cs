using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePrepareAttackState : PrepareAttackState
{
    protected override void OnEnter()
    {
        base.OnEnter();

        self.GetComponentInChildren<DamagePlayer>().SetDamage(self.damage);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (timer > self.adjustAttackerColliderTime && !hasRepositionned)
        {
            hasRepositionned = true;
            Vector2 offset = target.transform.position - self.transform.position;
            self.attackColliderWrapper.transform.rotation = Quaternion.LookRotation(Vector3.forward, offset) * Quaternion.Euler(0, 0, 90);
        }

        if (timer > self.attackWindupTime)
        {
            stateController.ChangeState(stateController.meleeAttackState);
        }
    }

    protected override void OnExit()
    {
        base.OnExit();

    }
}

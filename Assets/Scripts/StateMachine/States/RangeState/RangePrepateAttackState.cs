using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePrepareAttackState : PrepareAttackState
{
    protected override void OnUpdate()
    {
        base.OnUpdate();


        if (timer > self.attackWindupTime)
        {
            stateController.ChangeState(stateController.rangeAttackState);
        }
    }
}

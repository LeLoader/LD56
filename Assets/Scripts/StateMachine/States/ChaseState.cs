using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChaseState : State
{
    Player target;
    Enemy self;

    protected override void OnEnter()
    {
        base.OnEnter();
        target = stateController.player;
        self = stateController.enemy;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        self.spriteRenderer.color = Color.blue;
        if (target != null)
        {
            if (Vector2.Distance(self.transform.position, target.transform.position) >= self.attackRange)
            {
                self.transform.position = Vector2.MoveTowards(self.transform.position, target.transform.position, self.GetSpeed() * Time.deltaTime);
            }
            else
            {
                stateController.ChangeState(stateController.prepareAttackState);
            }
        }
    }

    protected override void OnExit()
    {
        base.OnExit();
    }
}
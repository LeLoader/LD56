using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareAttackState : State
{
    Player target;
    Enemy self;

    float timer;

    protected override void OnEnter()
    {
        base.OnEnter();
        timer = 0;
        target = stateController.player;
        self = stateController.enemy;
        self.spriteRenderer.color = Color.yellow;
        self.attackColliderWrapper.transform.LookAt(target.transform);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (timer <= self.attackWindupTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            stateController.ChangeState(stateController.attackState);
        }
    }

    protected override void OnExit()
    {
        base.OnExit();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    Enemy self;

    float timer = 0;

    protected override void OnEnter()
    {
        base.OnEnter();
        timer = 0;
        self = stateController.enemy;
        self.spriteRenderer.color = Color.red;
        self.attackCollider.enabled = true;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (timer <= self.attackTime)
        {
            timer += Time.deltaTime;
        }
        else
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
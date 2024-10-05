using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Player target;
    protected Enemy self;
    protected float timer = 0;

    protected override void OnEnter()
    {
        base.OnEnter();

        target = stateController.player;
        self = stateController.enemy;

        self.spriteRenderer.color = Color.red;
        timer = 0;    
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (timer < self.attackTime)
        {
            timer += Time.deltaTime;
        }
        
    }

    protected override void OnExit()
    {
        base.OnExit();

    }
}
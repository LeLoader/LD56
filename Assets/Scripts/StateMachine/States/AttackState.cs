using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AttackState : State
{
    protected Player target;
    protected Enemy self;
    protected float timer = 0;

    protected int attackCount;

    protected override void OnEnter()
    {
        base.OnEnter();

        target = stateController.player;
        self = stateController.enemy;
        attackCount = self.attackCount;

        timer = 0;
        self.IsAttacking = true;
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


        self.IsAttacking = false;
    }
}
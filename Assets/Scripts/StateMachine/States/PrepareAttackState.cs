using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class PrepareAttackState : State
{
    protected Player target;
    protected Enemy self;

    protected float timer;
    protected bool hasRepositionned;

    protected override void OnEnter()
    {
        base.OnEnter();

        target = stateController.player;
        self = stateController.enemy;

        timer = 0;
        hasRepositionned = false;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (timer <= self.attackWindupTime)
        {
            timer += Time.deltaTime;
        }
    }

    protected override void OnExit()
    {
        base.OnExit();
    }
}
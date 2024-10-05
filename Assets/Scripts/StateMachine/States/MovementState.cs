using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
    protected Player target;
    protected Enemy self;

    protected override void OnEnter()
    {
        base.OnEnter();

        target = stateController.player;
        self = stateController.enemy;
        self.spriteRenderer.color = Color.blue;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        
    }

    protected override void OnExit()
    {
        base.OnExit();

    }
}

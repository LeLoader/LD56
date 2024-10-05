using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionState : MovementState
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        self.spriteRenderer.color = Color.blue;
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(self.transform.position, target.transform.position);
            if (self.attackRange - self.attackRangeInterval <= distanceToTarget && distanceToTarget <= self.attackRange + self.attackRangeInterval)
            {
                stateController.ChangeState(stateController.rangePrepareAttackState);
                
            }
            else if (self.attackRange - self.attackRangeInterval > distanceToTarget) // Target is too close
            {
                self.transform.position = Vector2.MoveTowards(self.transform.position, self.transform.position - (target.transform.position - self.transform.position), self.GetSpeed() * Time.deltaTime);
            }
            else if (distanceToTarget > self.attackRange + self.attackRangeInterval) // Target is too far
            {
                self.transform.position = Vector2.MoveTowards(self.transform.position, target.transform.position, self.GetSpeed() * Time.deltaTime);
            }
        }
    }
}

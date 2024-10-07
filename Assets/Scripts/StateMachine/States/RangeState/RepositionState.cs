using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RepositionState : MovementState
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(self.transform.position, target.transform.position);
            if (self.attackRange - self.attackRangeInterval <= distanceToTarget && distanceToTarget <= self.attackRange + self.attackRangeInterval)
            {
                self.rb.velocity = Vector2.zero;
                stateController.ChangeState(stateController.rangePrepareAttackState);      
            }
            else if (self.attackRange - self.attackRangeInterval > distanceToTarget) // Target is too close
            {
                self.rb.velocity = self.GetSpeed() * (self.transform.position - (target.transform.position - self.transform.position)).normalized;
                // self.transform.position = Vector2.MoveTowards(self.transform.position, self.transform.position - (target.transform.position - self.transform.position), self.GetSpeed() * Time.deltaTime);
            }
            else if (distanceToTarget > self.attackRange + self.attackRangeInterval) // Target is too far
            {
                self.rb.velocity = self.GetSpeed() * (target.transform.position - self.transform.position).normalized;
                // self.transform.position = Vector2.MoveTowards(self.transform.position, target.transform.position, self.GetSpeed() * Time.deltaTime);
            }
        }
    }
}

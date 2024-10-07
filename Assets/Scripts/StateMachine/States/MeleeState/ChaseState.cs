using UnityEngine;

public class ChaseState : MovementState
{
    protected override void OnEnter()
    {
        base.OnEnter();

    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (target != null)
        {
            if (Vector2.Distance(self.transform.position, target.transform.position) >= self.attackRange)
            {
                // self.transform.position = Vector2.MoveTowards(self.transform.position, target.transform.position, self.GetSpeed() * Time.deltaTime);
                self.rb.velocity = self.GetSpeed() * (target.transform.position - self.transform.position).normalized;
            }
            else
            {
                self.rb.velocity = Vector2.zero;
                stateController.ChangeState(stateController.meleePrepareAttackState);
            }
        }
    }
}
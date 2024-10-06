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
        self.spriteRenderer.color = Color.blue;
        if (target != null)
        {
            if (Vector2.Distance(self.transform.position, target.transform.position) >= self.attackRange)
            {
                self.transform.position = Vector2.MoveTowards(self.transform.position, target.transform.position, self.GetSpeed() * Time.deltaTime);
            }
            else
            {
                stateController.ChangeState(stateController.meleePrepareAttackState);
            }
        }
    }
}
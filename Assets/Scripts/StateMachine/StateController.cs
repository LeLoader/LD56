
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    public Enemy enemy;
    public Player player;

    public IdleState idleState = new();

    public ChaseState chaseState = new();
    public RepositionState repositionState = new();

    public MeleePrepareAttackState meleePrepareAttackState = new();
    public RangePrepareAttackState rangePrepareAttackState = new();
    
    public MeleeAttackState meleeAttackState = new();
    public RangeAttackState rangeAttackState = new();

    private void Start()
    {
        ChangeState(idleState);
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        currentState?.OnStateUpdate();
    }

    public void ChangeState(State newState)
    {
        currentState?.OnStateExit(); // if current state is not null
        currentState = newState;
        currentState.OnStateEnter(this);
    }
}

public abstract class State
{
    protected StateController stateController;

    public void OnStateEnter(StateController stateController)
    {
        this.stateController = stateController;
        OnEnter();
    }

    protected virtual void OnEnter()
    {
        
    }

    public void OnStateUpdate()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {

    }

    public void OnStateExit()
    {
        OnExit();
    }

    protected virtual void OnExit()
    {

    }
}

using UnityEngine;

public class Enemy_Rabbie : Enemy
{
    #region States
    public RabbieIdleState idleState { get; private set; }
    public RabbieMoveState moveState { get; private set; }
    public RabbieBattleState battleState { get; private set; }
    public RabbiePatrollingState patrollingState { get; private set; }
    public RabbieAttackState attackState { get; private set; }
    
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new RabbieIdleState(this, stateMachine, "Idle", this);
        moveState = new RabbieMoveState(this, stateMachine, "Move", this);
        battleState = new RabbieBattleState(this, stateMachine, "Move", this);
        patrollingState = new RabbiePatrollingState(this, stateMachine, "Move", this);
        attackState = new RabbieAttackState(this, stateMachine, "Attack", this);
        
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }


    protected override void Update()
    {
        base.Update();
        
    }

}

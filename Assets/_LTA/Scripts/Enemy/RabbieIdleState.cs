using UnityEngine;

public class RabbieIdleState : EnemyState
{
    Enemy_Rabbie enemy;
    public RabbieIdleState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName, Enemy_Rabbie _enemy) : base(_enemyBase, _StateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime; // Set the idle time to 1 second
        enemy.anim.SetFloat("xInput", enemy.currentDirection.x);
        enemy.anim.SetFloat("yInput", enemy.currentDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.moveState); // Change to move state after idle time
        }
    }
}

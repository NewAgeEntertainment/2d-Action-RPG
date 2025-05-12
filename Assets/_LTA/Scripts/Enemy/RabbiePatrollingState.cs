using UnityEditor.Purchasing;
using UnityEngine;

public class RabbiePatrollingState : EnemyState
{
    protected Enemy_Rabbie enemy;
    public RabbiePatrollingState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName, Enemy_Rabbie _enemy) : base(_enemyBase, _StateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();


        Debug.Log("Patrolling State");
    }

    public override void Exit()
    {
        base.Exit();
        

    }

    public override void Update()
    {
        base.Update();
        enemy.anim.SetFloat("xInput", enemy.currentDirection.x);
        enemy.anim.SetFloat("yInput", enemy.currentDirection.y);

        if (enemy.IsPlayerDetected())
        {
            stateMachine.ChangeState(enemy.battleState); // Change to move state if the enemy is chasing
        }


    }
}

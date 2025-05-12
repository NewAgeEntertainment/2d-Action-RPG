using UnityEngine;

public class RabbieGroundedState : EnemyState
{
    protected Enemy_Rabbie enemy;

    public RabbieGroundedState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName, Enemy_Rabbie _enemy) : base(_enemyBase, _StateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

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

        if (!enemy.IsPlayerDetected())
        {
            stateMachine.ChangeState(enemy.patrollingState); // Change to patrolling state if the player is not detected
        }

        //if (enemy.IsPlayerDetected())
        //{
        //    stateMachine.ChangeState(enemy.battleState); // Change to move state if the enemy is chasing
        //}




    }
}

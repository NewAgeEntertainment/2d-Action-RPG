using UnityEngine;

public class RabbieGroundedState : EnemyState
{
    protected Enemy_Rabbie enemy;
    private Transform player; // Added player reference

    public RabbieGroundedState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName, Enemy_Rabbie _enemy) : base(_enemyBase, _StateMachine, _animBoolName)
    {
        this.enemy = _enemy;
        this.player = _enemy.transform.Find("Player"); // Assuming "Player" is the name of the player object in the hierarchy
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
            stateMachine.ChangeState(enemy.patrollingState);
        }
        //else if (enemy.IsPlayerDetected()) // Fixed syntax error by replacing `else (condition)` with `else if (condition)`  
        //{
        //    stateMachine.ChangeState(enemy.battleState); // Change to battle state if the player is detected  
        //}
    }
}

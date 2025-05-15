using UnityEngine;

public class RabbieAttackState : EnemyState
{
    private Enemy_Rabbie enemy;
    public RabbieAttackState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName, Enemy_Rabbie enemy) : base(_enemyBase, _StateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        

        if (triggierCalled)
        {
            stateMachine.ChangeState(enemy.battleState); // Change to battle state if the attack animation is finished
        }
        
    }
}

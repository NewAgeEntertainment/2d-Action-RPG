using UnityEngine;

public class RabbieStunnedState : EnemyState
{
    private Enemy_Rabbie enemy;
    public RabbieStunnedState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName, Enemy_Rabbie _enemy) : base(_enemyBase, _StateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.stunDuration;

        // Fix: Extract the x and y components of the stunDirection and multiply them with the currentDirection scalar
        float velocityX = enemy.currentDirection.x * enemy.stunDirection.x;
        float velocityY = enemy.currentDirection.y * enemy.stunDirection.y;

        enemy.SetVelocity(velocityX, velocityY); // Apply the stun direction to the enemy's velocity
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0 && !enemy.IsPlayerDetected())
        {
            stateMachine.ChangeState(enemy.patrollingState); // Change to idle state after stun duration
        }
    }
}

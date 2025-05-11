using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbieBattlerState : EnemyState
{
    private Transform player;
    Enemy_Rabbie enemy;


    public RabbieBattlerState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName, Enemy_Rabbie _enemy) : base(_enemyBase, _StateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Battler State Entered");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isChasing == false)
        {
            stateMachine.ChangeState(enemy.patrollingState); // Change to idle state if the enemy is not chasing
        }
    }
}

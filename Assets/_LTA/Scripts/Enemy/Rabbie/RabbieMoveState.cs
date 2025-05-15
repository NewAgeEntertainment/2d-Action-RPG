using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.Windows;

public class RabbieMoveState : RabbieGroundedState
{
    public RabbieMoveState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName, Enemy_Rabbie _enemy) : base(_enemyBase, _StateMachine, _animBoolName, _enemy)
    {

    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isKnocked)
            return; // If the enemy is knocked, do not execute the attack

        enemy.anim.SetFloat("xInput", enemy.currentDirection.x);
        enemy.anim.SetFloat("yInput", enemy.currentDirection.y);
        

        //if (enemy.isPaused == true)
        //{
        //   stateMachine.ChangeState(enemy.idleState); // Change to idle state if the enemy is paused        
        //}

        if (enemy.isKnocked == true)
        {
            stateMachine.ChangeState(enemy.idleState); // Change to knockback state if the enemy is knocked
        }


    }
}

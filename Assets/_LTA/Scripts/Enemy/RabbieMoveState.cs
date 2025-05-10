using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.Windows;

public class RabbieMoveState : EnemyState
{
    private Enemy_Rabbie enemy;
    //private float xInput; // Declare xInput variable  
    //private float yInput; //  
    //private Vector2 direction; // Declare direction variable  

    public RabbieMoveState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName, Enemy_Rabbie _enemy) : base(_enemyBase, _StateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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

        enemy.anim.SetFloat("xInput", enemy.currentDirection.x);
        enemy.anim.SetFloat("yInput", enemy.currentDirection.y);
        

        if (enemy.isPaused == true)
        {
           stateMachine.ChangeState(enemy.idleState); // Change to idle state if the enemy is paused        
        }

    }
}

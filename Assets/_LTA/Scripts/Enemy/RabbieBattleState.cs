using NUnit.Framework.Internal.Commands;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

public class RabbieBattleState : EnemyState
{
    private Transform player;
    private Enemy_Rabbie enemy;
    private Vector2 moveDir;

    public RabbieBattleState(Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName, Enemy_Rabbie _enemy) : base(_enemyBase, _StateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player object by tag      
    }

    public override void Update()
    {
        base.Update();



        Collider2D detectedPlayer = enemy.IsPlayerDetected();
        if (detectedPlayer != null) // 
        {
            stateTimer = enemy.battleTime; // Set the battle time to 1 second

            float distanceToPlayer = Vector2.Distance(enemy.transform.position, detectedPlayer.transform.position); // 


            if (distanceToPlayer < enemy.attackDistance)// 
            {

                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState); // Change to attack state if within attack distance
                }
            }
        }
        else
        {
            if (stateTimer < 0)
                stateMachine.ChangeState(enemy.patrollingState); // Change to move state if not within attack distance
            Debug.Log("Player is out of attack range");
        }
        

        moveDir = new Vector2(player.position.x - enemy.transform.position.x, player.position.y - enemy.transform.position.y); // Calculate the enemy direction to player

        enemy.anim.SetFloat("xInput", moveDir.x);
        enemy.anim.SetFloat("yInput", moveDir.y);
        
        moveDir.Normalize();
  
        enemy.SetVelocity(moveDir.x * enemy.moveSpeed, moveDir.y * enemy.moveSpeed);
    }

    public override void Exit()
    {
        base.Exit();


    }

    private bool CanAttack()
    {
        if(Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        Debug.Log("attack is on cooldown");
        return false;
    }
}

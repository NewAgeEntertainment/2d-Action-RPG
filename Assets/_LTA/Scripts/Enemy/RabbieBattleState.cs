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
    private int moveDir;

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
        if (detectedPlayer != null)
        {
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, detectedPlayer.transform.position);
            if (distanceToPlayer < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState); // Change to attack state if within attack distance
                }
            }
        }

        // Determine movement direction based on player's position relative to the enemy      
        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        if (player.position.y > enemy.transform.position.y)
            moveDir = 1;
        else if (player.position.y < enemy.transform.position.y)
            moveDir = -1;






        // Normalize the direction vector to ensure consistent speed
        //moveDir.Normalize();

        // Set the enemy's velocity towards the player      
        enemy.SetVelocity(enemy.moveSpeed * moveDir, enemy.moveSpeed * moveDir);
    }

    public override void Exit()
    {
        base.Exit();

        if (enemy.isPlayerDetected() == false)
        {
            stateMachine.ChangeState(enemy.patrollingState);
        }
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

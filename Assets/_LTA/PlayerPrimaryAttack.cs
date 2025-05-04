using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{

    private int comboCounter; // Counter for the number of attacks in the combo

    private float lastTimeAttacked; // Time of the last attack
    private float comboWindow = 2; // Time window to register a combo
    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow) // 
        {
            comboCounter = 0; // Reset the combo counter if it exceeds 2
        }
           player.anim.SetInteger("ComboCounter", comboCounter); // Set the combo counter in the animator
        
        player.SetVelocity(player.attackMovement[comboCounter] * player.playerCurrentDirection.x, player.attackMovement[comboCounter] * player.playerCurrentDirection.y); // Set the player's velocity based on the attack movement
        Debug.Log(player.attackMovement[comboCounter] * player.playerCurrentDirection.y);

        stateTimer = .1f; // Set the state timer to 0.1 seconds

        Debug.Log(comboCounter);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f); // Start a coroutine to make the player busy for a short duration after the attack

        comboCounter++; // Increment the combo counter by 1
        lastTimeAttacked = Time.time; // Reset the last attack time
        //Debug.Log(lastTimeAttacked);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            rb.linearVelocity = new Vector2(0, 0); // Stop the player's movement when the attack animation is finished


        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
        
        
    }
}

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

        #region Choose attack direction

        float attackDirx = player.playerCurrentDirection.x; // Get the attack direction based on the player's current direction
        float attackDiry = player.playerCurrentDirection.y;


        if (xInput != 0)
            attackDirx = xInput; // If there is input in the x direction, use that for the attack direction
        if (yInput != 0)
            attackDiry = yInput; // If there is input in the y direction, use that for the attack direction
        #endregion

        player.anim.SetInteger("ComboCounter", comboCounter); // Set the combo counter in the animator

        
        player.SetVelocity(player.attackMovement[comboCounter] * attackDirx, player.attackMovement[comboCounter] * attackDiry); // Set the player's velocity based on the attack movement
        

        stateTimer = .1f; // Set the state timer to 0.1 seconds


    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .10f); // Start a coroutine to make the player busy for a short duration after the attack

        comboCounter++; // Increment the combo counter by 1
        lastTimeAttacked = Time.time; // Reset the last attack time
        //Debug.Log(lastTimeAttacked);
    }

    public override void Update()
    {
        base.Update();
       

        if (stateTimer < 0)
            player.SetZeroVelocity(); // Stop the player's movement when the state timer is less than 0

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState); // 
        
        
    }


    //example of how to use a variable to mean something by adding = to it.
    // Vector2 attackMovement = new Vector2(player.attackMovement[comboCounter], player.attackMovement[comboCounter]); // Get the attack movement vector based on the combo counter
}

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
        

        Debug.Log(comboCounter);
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++; // Increment the combo counter by 1
        lastTimeAttacked = Time.time; // Reset the last attack time
        //Debug.Log(lastTimeAttacked);
    }

    public override void Update()
    {
        base.Update();

        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);
        
        
    }
}

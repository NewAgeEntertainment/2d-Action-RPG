using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = new Vector2(0, 0);
        player.anim.SetFloat("xInput", player.playerCurrentDirection.x);
        player.anim.SetFloat("yInput", player.playerCurrentDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
        // player.anim.SetFloat("xInput", xInput);
        // player.anim.SetFloat("yInput", yInput);




        if (xInput != 0 || yInput != 0 && !Input.GetKeyDown(KeyCode.C))    
        {
            stateMachine.ChangeState(player.moveState);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            stateMachine.ChangeState(player.runState);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(player.dashState);
        }



    }


}

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

        player.ZeroVelocity();
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




        if (xInput != 0 && !player.isBusy) 
        {
            stateMachine.ChangeState(player.moveState);
        }

        if (yInput != 0 && !player.isBusy)
        {
            stateMachine.ChangeState(player.moveState);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            stateMachine.ChangeState(player.runState);
        }

        



    }


}

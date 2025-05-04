using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
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


        player.SetVelocity(xInput * player.moveSpeed, yInput * player.moveSpeed);

        player.anim.SetFloat("xInput", xInput);
        player.anim.SetFloat("yInput", yInput);

        
        if (xInput == 0 && yInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else
        {
            player.playerCurrentDirection = new Vector2(xInput, yInput);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            stateMachine.ChangeState(player.runState);
        }
    }
}

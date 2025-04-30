using UnityEngine;

public class PlayerMoveState : PlayerState
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
        

        player.SetVelocity(xInput, yInput);
        
        player.anim.SetFloat("xInput", currentDirection.x);
        player.anim.SetFloat("yInput", currentDirection.y);
        




        if (xInput == 0 && yInput == 0)
        {
            player.anim.SetFloat("xInput", xInput);
            player.anim.SetFloat("yInput", yInput);
            stateMachine.ChangeState(player.idleState);
        }

        









    }
}

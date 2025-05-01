using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    
        player.anim.SetFloat("xInput", xInput);
        player.anim.SetFloat("yInput", yInput);
    
    }


    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();

        Debug.Log("Dashing");

        player.SetVelocity(player.dashSpeed * player.playerCurrentDirection.x, player.dashSpeed * player.playerCurrentDirection.y);

        player.anim.SetFloat("xInput", player.playerCurrentDirection.x);
        player.anim.SetFloat("yInput", player.playerCurrentDirection.y);


        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);

    }
}

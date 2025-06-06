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

    }


    public override void Exit()
    {
        base.Exit();

        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.playerCurrentDirection.x, player.dashSpeed * player.playerCurrentDirection.y);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);

    }
}

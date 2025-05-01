using UnityEngine;

public class PlayerDashState : PlayerState
{

    private float _dashTime;

    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        Debug.Log("ENTER");
        _dashTime = 0.3f;
        base.Enter();
    }


    public override void Exit()
    {
        Debug.Log("EXIT");
        base.Exit();

        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();
        _dashTime -= Time.deltaTime;
        player.SetVelocity(player.dashSpeed * player.playerCurrentDirection.x, player.dashSpeed * player.playerCurrentDirection.y);


        if (_dashTime < 0)
            stateMachine.ChangeState(stateMachine.idleState);

    }
}

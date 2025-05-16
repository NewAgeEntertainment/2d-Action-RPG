using UnityEngine;

public class PlayerThrustState : PlayerState
{
    public PlayerThrustState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.thrustDuration;

        Debug.Log("Thrusting");
    }


    public override void Exit()
    {
        base.Exit();

        player.SetZeroVelocity();
        player.GetComponent<Collider2D>().enabled = true; // Disable the collider when exiting the thrust state
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.thrustSpeed * player.playerCurrentDirection.x, player.thrustSpeed * player.playerCurrentDirection.y);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);

    }
}

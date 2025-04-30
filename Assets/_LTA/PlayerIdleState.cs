using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        

        player.anim.SetFloat("xInput", 1);
        player.anim.SetFloat("yInput", 0);

        player.anim.SetFloat("xInput", -1);
        player.anim.SetFloat("yInput", 0);


        player.anim.SetFloat("xInput", 0);
        player.anim.SetFloat("yInput", 1);


        player.anim.SetFloat("xInput", 0);
        player.anim.SetFloat("yInput", -1);

        player.anim.SetFloat("xInput", currentDirection.x);
        player.anim.SetFloat("yInput", currentDirection.y);



    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
        player.anim.SetFloat("xInput", xInput);
        player.anim.SetFloat("yInput", yInput);

        


        if (xInput != 0 || yInput != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
        
    }


}

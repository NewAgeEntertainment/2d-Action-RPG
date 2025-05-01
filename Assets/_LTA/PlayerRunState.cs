using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
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

        
        
       player.SetVelocity(xInput * player.runSpeed, yInput * player.runSpeed);
        
       
        
        
            player.anim.SetFloat("xInput", xInput);
            player.anim.SetFloat("yInput", yInput);
            
        

        if (Input.GetKeyUp(KeyCode.C))
        {
            stateMachine.ChangeState(player.moveState);
        }


        if (xInput == 0 && yInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        




    }
}

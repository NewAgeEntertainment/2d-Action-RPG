using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{

    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected float xInput;
    protected float yInput;

    public Vector2Int currentDirection;
    


    private string animeBoolName;


    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animeBoolName = _animeBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animeBoolName, true);
        player.anim.SetFloat(animeBoolName, xInput);
        player.anim.SetFloat(animeBoolName, yInput);

    }

    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");


        


    }

    public virtual void Exit()
    {
        player.anim.SetBool(animeBoolName, false);
    }

    




}

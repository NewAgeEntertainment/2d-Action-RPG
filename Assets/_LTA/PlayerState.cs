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
        player = _player;
        stateMachine = _stateMachine;
        animeBoolName = _animeBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animeBoolName, true);
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

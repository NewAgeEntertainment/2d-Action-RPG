using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;

    public Vector2Int currentDirection;
    private string animeBoolName;

    protected float stateTimer;
    protected bool triggerCalled; // Used to check if the trigger was called

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animeBoolName = _animeBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animeBoolName, true);
        rb = player.rb;
        triggerCalled = false; // Reset the trigger called flag
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    public virtual void Exit()
    {
        
        player.anim.SetBool(animeBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true; // Set the trigger called flag to true
    }
}

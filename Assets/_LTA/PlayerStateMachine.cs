public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }


    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerRunState runState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        idleState = new PlayerIdleState(player, this, "Idle");
        moveState = new PlayerMoveState(player, this, "Move");
        dashState = new PlayerDashState(player, this, "Dash");
        runState = new PlayerRunState(player, this, "Run");
    }

    public void Initialize()
    {
        currentState = idleState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}

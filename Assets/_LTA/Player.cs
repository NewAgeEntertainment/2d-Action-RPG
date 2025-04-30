using JetBrains.Annotations;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class Player : MonoBehaviour
{

    public float moveSpeed;

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    // public Vector2Int currentDirection { get; private set; }

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public Vector2 playerCurrentDirection;

    public void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState); // Initialize the state machine with the idle state
    }

    // Update is called once per frame
    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }
}

using JetBrains.Annotations;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float moveSpeed;
    public float runSpeed;

    [Header("Dash Info")]
    [SerializeField] private float dashCoolDown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;

    public float dashDirx {  get; private set; }
    public float dashDiry { get; private set; }

    #region Components    
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    public Vector2 playerCurrentDirection;
    
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerRunState runState { get; private set; }

    public PlayerPrimaryAttack primaryAttack { get; private set; }
    #endregion

    public void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        runState = new PlayerRunState(this, stateMachine, "Run");

        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
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

        CheckForDashInput();
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger(); // Call the AnimationTrigger method of the current state in the state machine

    private void CheckForDashInput()
    {
        dashUsageTimer -= Time.deltaTime; // Decrease the dash usage timer by the time since the last frame  

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0) // Check if the left shift key is pressed and the dash cooldown is over  
        {
            dashUsageTimer = dashCoolDown; // Reset the dash usage timer to the cooldown value  

            //dashDirx = Input.GetAxisRaw("Horizontal"); // Get the horizontal input value for dash direction  
            //dashDiry = Input.GetAxisRaw("Vertical"); // Get the vertical input value for dash direction  

            //if (dashDirx == 0 && dashDiry == 0) // If no input is given, set the dash direction to the current player direction  
            //{
            //    dashDirx = playerCurrentDirection.x;
            //    dashDiry = playerCurrentDirection.y;
            //}

            stateMachine.ChangeState(dashState); // Change to the dash state when the left shift key is pressed  
        }
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }
}

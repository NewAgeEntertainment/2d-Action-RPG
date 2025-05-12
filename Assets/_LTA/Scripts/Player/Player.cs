using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class Player : Entity
{

    [Header("Attack details")]
    public float[] attackMovement;

    public bool isBusy{ get; private set; }

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

    

    public Vector2 playerCurrentDirection;
    
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerRunState runState { get; private set; }

    public PlayerPrimaryAttack primaryAttack { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake(); // Call the base class Awake method
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        runState = new PlayerRunState(this, stateMachine, "Run");

        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start(); // Call the base class Start method

        stateMachine.Initialize(idleState); // Initialize the state machine with the idle state
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); // Call the base class Update method
        stateMachine.currentState.Update();

        CheckForDashInput();
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true; // Set the isBusy flag to true
        

        yield return new WaitForSeconds(_seconds); // Wait for the specified number of seconds

        
        isBusy = false; // Set the isBusy flag to false
    }

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger(); // Call the AnimationTrigger method of the current state in the state machine
    } 


    private void CheckForDashInput()
    {
        dashUsageTimer -= Time.deltaTime; // Decrease the dash usage timer by the time since the last frame  

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0) // Check if the left shift key is pressed and the dash cooldown is over  
        {
            dashUsageTimer = dashCoolDown; // Reset the dash usage timer to the cooldown value  

      
            stateMachine.ChangeState(dashState); // Change to the dash state when the left shift key is pressed  
        }
    }

    
}

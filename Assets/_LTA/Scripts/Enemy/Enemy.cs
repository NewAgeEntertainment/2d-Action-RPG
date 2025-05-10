using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    public Vector2[] patrolPoints; // Array of patrol points for the enemy to move between
    public float moveSpeed; // Speed of the enemy  
    public float idleTime; // Time the enemy stays idle  
    public float pauseDuration; // Duration of the pause between patrol points
    public float xInput { get; private set; } // X-axis input for enemy movement
    public float yInput { get; private set; } // Y-axis input for enemy movement
    private SpriteRenderer sr; // Reference to the sprite renderer for the enemy 
    public bool isPaused { get; private set; } // Flag to check if the enemy is patrolling

    private int currentPatrolIndex; // Index of the current patrol point  

    public EnemyStateMachine stateMachine { get; private set; } // State machine for enemy AI  
    
    public Vector2 currentDirection { get; private set; } // Current direction of the enemy  

    // Make 'target' accessible by changing its protection level to 'protected' in the base class 'Entity'.  
    protected Vector2 target;

    protected override void Awake()
    {
        base.Awake();
        sr = GetComponentInChildren<SpriteRenderer>(); // Initialize the sprite renderer
        stateMachine = new EnemyStateMachine(); // Initialize enemy-specific components or properties here  
        StartCoroutine(SetPatrolPoint()); // Move to the next patrol point
        
    }

    // Update is called once per frame  
    protected override void Update()
    {
        base.Update();
        if (isPaused == true)
        {

            rb.linearVelocity = Vector2.zero;
            return; // Pause the enemy's movement if isPaused is true  
        }
        
        stateMachine.currentState.Update(); // Call the Update method of the current state  

        //// patrol point movement
        
        // Correcting the invalid assignment to `rb.linearVelocity`  
        Vector2 direction = ((Vector3)target - transform.position).normalized; // Calculate the direction to the target point
        rb.linearVelocity = direction * moveSpeed; // Set the enemy's velocity towards the target

        if (Vector2.Distance(transform.position, target) < .1f) // check if the enemy has reached the target point
        {
            StartCoroutine(SetPatrolPoint()); // Move to the next patrol point  
        }
        //------End of patrol point movement------//
    }

    // patrol pint setup
    IEnumerator SetPatrolPoint() // set patrol point  
    {

        isPaused = true; // Set the pause flag to true
        yield return new WaitForSeconds(pauseDuration); // Wait for the specified pause duration
        currentDirection = target - (Vector2)transform.position; // Calculate the direction to the target
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Ensure index wraps around using modulus operator  
        target = patrolPoints[currentPatrolIndex]; // set the target to the next patrol point  
        isPaused = false; // Reset the pause flag
        currentDirection = target - (Vector2)transform.position; // Calculate the direction to the target
    }
    //----End of patrol point setup----//

}

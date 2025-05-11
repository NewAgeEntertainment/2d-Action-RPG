using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    public Vector2[] patrolPoints; // Array of patrol points for the enemy to move between
    [SerializeField] protected Transform playerCheck; // Transform for checking the player's position
    [SerializeField] protected float playerCheckradius; // Radius for checking the player's position
    [SerializeField] protected LayerMask WhatIsPlayer;

    public bool isplayerPatrolling; // Flag to check if the enemy is patrolling the player


    [Header("Move Info")]

    public float moveSpeed; // Speed of the enemy  
    public float idleTime; // Time the enemy stays idle  
    public float pauseDuration; // Duration of the pause between patrol points
    public bool isChasing { get; private set; } // Flag to check if the enemy is chasing the player

    public bool isPaused { get; private set; } // Flag to check if the enemy is patrolling



    private int currentPatrolIndex; // Index of the current patrol point  

    private Transform player; // Reference to the player transform
    public EnemyStateMachine stateMachine { get; private set; } // State machine for enemy AI  

    public Vector2 currentDirection { get; private set; } // Current direction of the enemy  

    // Make 'target' accessible by changing its protection level to 'protected' in the base class 'Entity'.  
    public Vector2 target { get; private set; } // Target position for the enemy to move towards

    protected override void Awake()
    {
        base.Awake();
        
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

        if (isChasing == true)
        {
            // Implement chasing logic here if needed
            Vector2 facing = (player.position - transform.position).normalized; // Calculate the direction to the player
            rb.linearVelocity = facing * moveSpeed; // Set the enemy's velocity towards the player
            isplayerPatrolling = true; // Set the player patrolling flag to true
        }


        stateMachine.currentState.Update(); // Call the Update method of the current state  

        ////// patrol point movement

        
        
        Vector2 direction = ((Vector3)target - transform.position).normalized; // Calculate the direction to the target point
        rb.linearVelocity = direction * moveSpeed; // Set the enemy's velocity towards the target

        if (Vector2.Distance(transform.position, target) < .1f) // check if the enemy has reached the target point
        {
            StartCoroutine(SetPatrolPoint()); // Move to the next patrol point  
        }
        //------End of patrol point movement------//


    }



    //// patrol pint setup
    public virtual IEnumerator SetPatrolPoint() // set patrol point  
    {
        

        isPaused = true; // Set the pause flag to true
        yield return new WaitForSeconds(pauseDuration); // Wait for the specified pause duration
        currentDirection = target - (Vector2)transform.position; // Calculate the direction to the target
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Ensure index wraps around using modulus operator  
        target = patrolPoints[currentPatrolIndex]; // set the target to the next patrol point  
        isPaused = false; // Set the pause flag to true
        currentDirection = target - (Vector2)transform.position; // Calculate the direction to the target

    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player == null)
            {

                player = collision.transform;
            }
            isChasing = true; // Set the chasing flag to true when the player enters the trigger

        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.linearVelocity = Vector2.zero; // Stop the enemy's movement when the player exits the trigger
            isChasing = false; // Set the chasing flag to false when the player exits the trigger
        }
    }
    //----End of patrol point setup----//

    //public virtual RaycastHit2D isPlayerDetected() => Physics2D.CircleCast(playerCheck.position, playerCheckradius, target, WhatIsPlayer);



    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red; // Set the color of the gizmo to red
    //    Gizmos.DrawWireSphere(playerCheck.position, playerCheckradius); // Draw a wire sphere at the player check position with the specified radius
    //}

}

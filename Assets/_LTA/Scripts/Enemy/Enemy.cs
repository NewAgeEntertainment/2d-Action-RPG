using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using JetBrains.Annotations;

public class Enemy : Entity
{

    #region Move info
    [Header("Move Info")]
    public float moveSpeed; // Speed of the enemy  
    public float idleTime; // Time the enemy stays idle  
    public float pauseDuration; // Duration of the pause between patrol points
    public float battleTime; // Time the enemy stays in battle state
    #endregion

    #region Attack Info
    [Header("Attack info")]
    public float attackDistance;
    public float attackCooldown;
    public float range; // Range for the enemy's detection of the player
    [SerializeField] protected LayerMask whatIsPlayer; // Layer mask for the player layer
    [SerializeField] public GameObject attackIndicator; // Reference to the attack signal GameObject
    [HideInInspector] public float lastTimeAttacked;
    #endregion

    #region Patrol Info
    [Header("Patrol info")]
    public Vector2[] patrolPoints; // Array of patrol points for the enemy to move between
    private int currentPatrolIndex; // Index of the current patrol point  
    public bool isPaused { get; private set; } // Flag to check if the enemy is patrolling
    #endregion

    public Vector2 currentDirection { get; private set; } // Current direction of the enemy  
     
    private Vector2 target; // Target position for the enemy to move towards

    public EnemyStateMachine stateMachine { get; private set; } // State machine for enemy AI  

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

        stateMachine.currentState.Update();

        if (isPaused == true)
        {
            rb.linearVelocity = Vector2.zero;
            return; // Pause the enemy's movement if isPaused is true  
        }

        if (isKnocked == true)
        {
            return; // If the knockback duration is greater than 0, return to prevent further updates

        }
        
        ////// patrol point movement  
        Vector2 movedirection = ((Vector3)target - transform.position).normalized; // Calculate the direction to the target point  
        rb.linearVelocity = movedirection * moveSpeed; // Set the enemy's velocity towards the target  

        if (Vector2.Distance(transform.position, target) < .1f) // check if the enemy has reached the target point  
        {
            StartCoroutine(SetPatrolPoint()); // Move to the next patrol point  
        }
        //------End of patrol point movement------//  

        stateMachine.currentState.Update(); // Call the Update method of the current state  

    }

    public virtual void ShowAttackIndicator()
    {
        attackIndicator.SetActive(true); // Activate the attack signal
    }

    public virtual void HideAttackIndicator()
    {
        attackIndicator.SetActive(false); // Deactivate the attack signal
    }


    public virtual void Knockback(Transform playerTransform, float knockbackForce)
    {
        // Calculate the knockback direction based on the player's position
        Vector2 knockbackDirection = (transform.position - playerTransform.position).normalized; // Normalize the direction vector
        rb.linearVelocity = knockbackDirection * knockbackForce; // Apply the knockback force to the rigidbody
        Debug.Log("knockback works");
    }

    public virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;
        yield return new WaitForSeconds(knockbackDuration);//(0.5f) I use a variable for the duration of the knockback instead of a hardcoded value
        isKnocked = false;
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger(); // Call the AnimationFinishTrigger method of the current state  

    //// patrol pint setup
    public virtual IEnumerator SetPatrolPoint() // set patrol point  
    {
        isPaused = true; // Set the pause flag to true  
        yield return new WaitForSeconds(pauseDuration); // Wait for the specified pause duration  
        currentDirection = target - (Vector2)transform.position; // Calculate the direction to the target  
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Ensure index wraps around using modulus operator  
        target = patrolPoints[currentPatrolIndex]; // set the target to the next patrol point  
        isPaused = false; // Set the pause flag to false  
        currentDirection = target - (Vector2)transform.position; // Calculate the direction to the target  
    }
    // Closing brace added here to fix CS1513  

    public virtual bool isPlayerDetected() => Physics2D.OverlapCircle(transform.position, range, whatIsPlayer);

    public virtual Collider2D IsPlayerDetected()
    {
        return Physics2D.OverlapCircle(transform.position, range, whatIsPlayer); // Return the Collider2D detected within the radius  
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos(); // Call the base class OnDrawGizmos method  
        Gizmos.color = Color.red; // Set the color of the gizmo to red  
        Gizmos.DrawWireSphere(transform.position, range); // Draw a wire sphere at the enemy's position with the specified range  

        Gizmos.color = Color.yellow;
        // Fixing the problematic line by correctly calculating the attack range as a Vector3  
        Vector3 attackRangePosition = new Vector3(transform.position.x, transform.position.y, 0); // Set the attack range position to the enemy's position  
        Gizmos.DrawWireSphere(attackRangePosition, attackDistance); // Draw a small sphere to represent the attack range

        // draw a line between all the patrol points
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            if (i == patrolPoints.Length - 1)
            {
                Gizmos.DrawLine(patrolPoints[i], patrolPoints[0]); // Draw a line between the last and first patrol points
            }
            else
            {
                Gizmos.DrawLine(patrolPoints[i], patrolPoints[i + 1]); // Draw a line between consecutive patrol points
            }
        }
    }






}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components    
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; } // Reference to the EntityFX script for visual effects
    #endregion

    [Header("KnockBack info")]
    [SerializeField] protected Vector2 knockbackDirection; // Direction of the knockback effect
    [SerializeField] protected float KnockbackDuration;// this float variable is to swap 0.5 with your own inside the inspector // Duration of the knockback effect
    private Transform playerTransform; // Reference to the player's transform for direction calculation
    public bool isKnocked { get; private set; } // Flag to check if the entity is knocked back
    private float knockbackForce; // Force applied during knockback

    private float currentDirection; 


    [Header("Collision Check")]
    public Transform[] attackCheck; // Transform array for attack check  
    public float attackCheckRadius; // Radius for attack check  

    protected virtual void Awake()
    {
        // Initialize code here  
    }

    protected virtual void Start()
    {
        fx = GetComponent<EntityFX>(); // Get the EntityFX component for visual effects
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        // Update code here  
    }

    // Damage logic  
    public virtual void Damage()
    {
        // Damage logic here
        fx.StartCoroutine("FlashFX"); // Start the flash effect coroutine from EntityFX
        StartCoroutine("HitKnockBack"); // Start the knockback coroutine

        Debug.Log(gameObject.name + " was damaged!");
    }
    public virtual IEnumerator HitKnockBack()
    {
        
        isKnocked = true;
        // Fixing the error by correctly accessing the x and y components of the Vector2
        
        rb.linearVelocity = new Vector2(knockbackDirection.x * transform.position.x, knockbackDirection.y * transform.position.y); // Apply knockback force in the specified direction

        //rb.linearVelocity = new Vector2(playerTransform.position.x * knockbackForce, playerTransform.position.y * knockbackForce); // Apply knockback force in the direction of the player

        // if (knockbackForce > playerTransform.position.x)
        // {
        //     currentDirection = 1; // Set the direction to right
        // }
        // else if (knockbackForce < transform.position.x)
        // {
        //     currentDirection = -1; // Set the direction to left
        // }

        // if (knockbackForce > playerTransform.position.y)
        // {
        //     currentDirection = 1; // Set the direction to right
        // }
        // else if (knockbackForce < playerTransform.position.y)
        // {
        //     currentDirection = -1; // Set the direction to left
        // }


        yield return new WaitForSeconds(KnockbackDuration);//(0.5f) I use a variable for the duration of the knockback instead of a hardcoded value
        isKnocked = false;
    }

    #region Velocity  

    public void SetZeroVelocity()
    {
        if (isKnocked)
            return;// If the entity is knocked back, do not set the velocity
        rb.linearVelocity = new Vector2(0,0); // Set the player's velocity to zero  
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        
        if (isKnocked)
            return; // If the entity is knocked back, do not set the velocity

        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }
    #endregion

    #region Collision  

    protected virtual void OnDrawGizmos()
    {
        if (attackCheck != null)
        {
            Gizmos.color = Color.white;
            foreach (var check in attackCheck)
            {
                if (check != null)
                {
                    Gizmos.DrawWireSphere(check.position, attackCheckRadius); // Draw a wire sphere for attack check  
                }
            }
        }
    }
    #endregion
}

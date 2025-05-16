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

    [Header("Collision Check")]
    public Transform[] attackCheck; // Transform array for attack check  
    public float attackCheckRadius; // Radius for attack check  
    
    [Header("KnockBack info")]
    [SerializeField] protected float knockbackDuration;// this float variable is to swap 0.5 with your own inside the inspector // Duration of the knockback effect
    public float knockbackForce; // Force applied during knockback
    [HideInInspector]public bool isKnocked; // Flag to check if the entity is knocked back

    private float currentDirection; 
    private Transform playerTransform; // Reference to the player's transform for direction calculation


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
    

    #region Velocity  

    public void SetZeroVelocity()
    {
        if (isKnocked)
            return;// If the entity is knocked back, do not set the velocity
        rb.linearVelocity = new Vector2(0,0); 
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

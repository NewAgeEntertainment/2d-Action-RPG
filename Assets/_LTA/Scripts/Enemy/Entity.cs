using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Entity : MonoBehaviour
{

    #region Components    
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    

    protected virtual void Awake()
    {
        // Initialize code here
    }

    protected virtual void Start() 
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }
    #region Velocity

    protected virtual void Update()
    {
        // Update code here
    }


    public void ZeroVelocity()
    {
        rb.linearVelocity = Vector2.zero; // Set the player's velocity to zero
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }
    #endregion
}

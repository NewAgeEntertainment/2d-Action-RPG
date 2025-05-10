using UnityEngine;
using UnityEngine.InputSystem.XInput;
using static UnityEngine.GraphicsBuffer;

public class EnemyState // base class for all enemy states
{

    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;

    private string animBoolName;

    protected float stateTimer;
    protected bool triggierCalled;


    public EnemyState (Enemy _enemyBase, EnemyStateMachine _StateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _StateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        

    }

    public virtual void Enter()
    {
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }
}

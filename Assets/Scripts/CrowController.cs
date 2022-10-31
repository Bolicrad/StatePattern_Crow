using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class CrowController : MonoBehaviour
{
    //Instances of every State
    public IState Idle;
    public IState Walking;
    public IState Running;
    public IState Jumping;
    public IState DoubleJumping;
    public IState Falling;
    public IState Dashing;
    public IState Attacking;
    
    //The reference of current state
    private IState _state;
    public IState State
    {
        get => _state;
        set
        {
            if (value == null || value == _state) return;
            _state?.OnExit();
            value.OnEnter(_state);
            _state = value;
        }
    }
    
    //The reference of current Horizontal state
    private IState _horizontal;

    public IState Horizontal
    {
        get => _horizontal;
        set
        {
            if(value == _horizontal) return;
            _horizontal = value;
        }
    }

    private IState _vertical;

    public IState Vertical
    {
        get => _vertical;
        set
        {
            if (value == _vertical) return;
            _vertical = value;
        }
    }

    private bool _isLandedBuffer;
    public bool IsLanded =>
        Physics2D.Raycast(
            (Vector2)transform.position + Vector2.up * 0.05f,
            Vector2.down,
            0.1f,
            groundLayer);

    public bool canDoubleJump;
    public bool canDash;
    public bool canAttack;

    public float SpeedY => r_rigidbody.velocity.y;
    public float LastSpeedY { get; set; }

    //The reference of Crow Components
    public SkeletonAnimation spine;
    public Rigidbody2D r_rigidbody;
    public Collider2D attacker;
    
    //Parameters
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float dashDistance;
    public int attackDamage;
    public LayerMask groundLayer;
    

    //Input Functions
    void Walk()
    {
        State.Walk();
    }

    void Run()
    {
        State.Run();
    }

    void Stop()
    {
        State.Stop();
    }

    void Jump()
    {
        State.Jump();
    }

    public void Fall()
    {
        State.Fall();
    }

    void Dash()
    {
        State.Dash();
    }

    void Attack()
    {
        State.Attack();
    }

    public void Land()
    {
        State.Land();
    }

    // Start is called before the first frame update
    void Start()
    {
        Idle = new Idle(this);
        Walking = new Walking(this);
        Running = new Running(this);
        Jumping = new Jumping(this);
        DoubleJumping = new DoubleJumping(this);
        Falling = new Falling(this);
        Dashing = new Dashing(this);
        Attacking = new Attacking(this);


        State = Idle;
        LastSpeedY = 0;
        _isLandedBuffer = IsLanded;

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        
        State.Update();
    }

    void HandleInput()
    {
        //Jump-Falling Logic
        if (Input.GetButtonDown("Jump")) Jump();
        else if (IsLanded != _isLandedBuffer)
        {
            if (_isLandedBuffer)
            {
                //Now not landed, not jumped, which means walked to falling
                Fall();
            }
        }
        _isLandedBuffer = IsLanded;

        //Dash/Attack Logic
        if (Input.GetButtonDown("Fire1"))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Attack();
        }
        if (Input.GetButtonDown("Fire2")) Dash();

        //Horizontal Logic
        var horizontal = Input.GetAxis("Horizontal");
        if (horizontal == 0f) Stop();
        else
        {
            if(Input.GetButton("Fire3"))Run();
            else Walk();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Unity.VisualScripting;

public class CrowController : MonoBehaviour
{
    //Instances of every State
    public IState Idle;
    public IState Walking;
    public IState Running;
    public IState Jumping;
    public IState DoubleJumping;
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

    public bool isLanded;

    //The reference of Crow Spine Animation
    public SkeletonAnimation spine;
    
    //Parameters
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float dashDistance;
    public int attackDamage;

    //Input Functions
    void Walk()
    {
        State = State.Walk();
    }

    void Run()
    {
        State = State.Run();
    }

    void Stop()
    {
        State = State.Stop();
    }

    void Jump()
    {
        State = State.Jump();
    }

    void Dash()
    {
        State = State.Dash();
    }

    void Attack()
    {
        State = State.Attack();
    }

    // Start is called before the first frame update
    void Start()
    {
        Idle = new Idle(this);
        Walking = new Walking(this);
        Running = new Running(this);
        Jumping = new Jumping(this);
        DoubleJumping = new DoubleJumping(this);


        State = Idle;
        isLanded = true;

    }

    // Update is called once per frame
    void Update()
    {
        State.Update();
        Vertical?.Update();
        Horizontal?.Update();

        HandleInput();
    }

    void HandleInput()
    {
        //Jump-Falling Logic
        if (Input.GetButtonDown("Jump")) Jump();

        //Dash/Attack Logic
        if (Input.GetButtonDown("Fire1")) Dash();
        if (Input.GetButtonDown("Fire2")) Attack();
        
        //Horizontal Logic
        var horizontal = Input.GetAxis("Horizontal");
        if (horizontal == 0f) Stop();
        else
        {
            if(Input.GetButton("Fire3"))Run();
            else Walk();
        }
        
        //Debug Only: Call Fall Function with button
        if (Input.GetKeyDown(KeyCode.W)) State = State.Fall();
        if (Input.GetKeyDown(KeyCode.S)) State = State.Land();
    }
}

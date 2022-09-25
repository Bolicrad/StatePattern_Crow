using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Walk()
    {
        _state = _state.Walk();
    }

    void Run()
    {
        _state = _state.Run();
    }

    void Jump()
    {
        _state = _state.Jump();
    }

    void Dash()
    {
        _state = _state.Dash();
    }

    void Attack()
    {
        _state = _state.Attack();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        Idle = new Idle(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

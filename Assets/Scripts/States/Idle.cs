using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{
    private readonly CrowController _crowController;
    
    public Idle(CrowController controller)
    {
        _crowController = controller;
    }

    public IState Walk()
    {
        return _crowController.Walking;
    }

    public IState Run()
    {
        return _crowController.Running;
    }

    public IState Jump()
    {
        return _crowController.Jumping;
    }

    public IState Dash()
    {
        return _crowController.Dashing;
    }

    public IState Attack()
    {
        return _crowController.Attacking;
    }

    public IState Stop()
    {
        return _crowController.Idle;
    }
}

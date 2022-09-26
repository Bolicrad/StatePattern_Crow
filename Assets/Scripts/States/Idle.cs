using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Idle : IState
{
    protected readonly CrowController CrowController;
    protected IState Previous;

    public Idle(CrowController controller)
    {
        CrowController = controller;
    }

    public virtual IState Walk()
    {
        return CrowController.Walking;
    }

    public virtual IState Run()
    {
        return CrowController.Running;
    }

    public virtual IState Stop()
    {
        return CrowController.Idle;
    }

    public virtual IState Jump()
    {
        CrowController.Horizontal = this;
        return CrowController.Jumping;
    }

    public virtual IState Dash()
    {
        //_crowController.spine.AnimationState.SetAnimation(0, "Dash", false);
        
        //Dash Logic
        
        return CrowController.Dashing;
    }

    public virtual IState Attack()
    {
        //_crowController.spine.AnimationState.SetAnimation(0, "Attack1", false);
        
        //Attack Logic
        
        return CrowController.Attacking;
    }

    public virtual IState Fall()
    {
        return new Falling(CrowController);
    }

    public virtual IState Land()
    {
        //No response to land when landed or jumping up
        return null;
    }

    public virtual void OnEnter(IState previous)
    {
        Previous = previous;
        var trackEntry = CrowController.spine.AnimationState.GetCurrent(0);
        if (trackEntry is { Loop: true }) SetAnim();
        else AddAnim();
    }

    protected virtual void AddAnim()
    {
        CrowController.spine.AnimationState.AddAnimation(0, "idle_1", true,0f);
    }

    protected virtual void SetAnim()
    {
        CrowController.spine.AnimationState.SetAnimation(0, "idle_1", true);
    }


    public virtual void OnExit()
    {
        //CrowController.spine.AnimationState.ClearTrack(0);
    }

    public virtual void Update()
    {
        //Count time and change spine anim to idle_2
    }
}

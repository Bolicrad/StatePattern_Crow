using UnityEngine;

public class Idle : IState
{
    protected readonly CrowController CrowController;
    protected IState Previous;

    public Idle(CrowController controller)
    {
        CrowController = controller;
    }

    public virtual void Walk()
    {
        CrowController.State = CrowController.Walking;
    }

    public virtual void Run()
    {
        CrowController.State = CrowController.Running;
    }

    public virtual void Stop()
    {
        CrowController.State = CrowController.Idle;
    }

    public virtual void Jump()
    {
        if (CrowController.canJump)
        {
            CrowController.Horizontal = this;
            CrowController.State = CrowController.Jumping;
        }
    }

    public virtual void Dash()
    {
        if(CrowController.canDash)CrowController.State = CrowController.Dashing;
    }

    public virtual void Attack()
    {
        if(CrowController.canAttack)CrowController.State = CrowController.Attacking;
    }

    public virtual void Fall()
    {
        CrowController.State = CrowController.Falling;
    }

    public virtual void Land()
    {
        //No response to land when landed or jumping up
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
        if (CrowController.r_rigidbody.velocity.x != 0f)
        {
            CrowController.r_rigidbody.velocity = new Vector2(0, CrowController.r_rigidbody.velocity.y);
        }
    }
}

using System.Collections;
using UnityEngine;

public class Jumping : Falling
{
    public Jumping(CrowController controller) : base(controller) { }

    public override void Update()
    {
        if (CrowController.SpeedY * CrowController.LastSpeedY < 0)
        {
            CrowController.Fall();
        }
        CrowController.LastSpeedY = CrowController.SpeedY;
        
        CrowController.Horizontal?.Update();
    }

    public override void Jump()
    {
        //Double Jump
        CrowController.State = CrowController.DoubleJumping;
    }

    public override void Fall()
    {
        CrowController.State = CrowController.Falling;
    }

    public override void Land()
    {
        //Do nothing
    }

    public override void OnEnter(IState previous)
    {
        Previous = previous;
        SetAnim();
        Launch();
    }

    protected override void SetAnim()
    {
        CrowController.spine.AnimationState.SetAnimation(0, "Jump_start", false);
    }

    protected virtual void Launch()
    {
        CrowController.spine.AnimationState.AddAnimation(0, "Jump_up", true, 0f);
        //Launch Impulse on the rigidbody to jump
        CrowController.LastSpeedY = CrowController.jumpForce;
        CrowController.r_rigidbody.velocity = new Vector2(CrowController.r_rigidbody.velocity.x, CrowController.jumpForce);
    }
    
    public override void OnExit()
    {
        CrowController.spine.AnimationState.SetAnimation(0, "Jump_Top", false);
    }
}
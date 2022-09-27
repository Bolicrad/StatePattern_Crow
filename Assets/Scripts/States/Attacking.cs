using Spine;
using UnityEngine;
using System;
using System.Collections;

public class Attacking : Jumping
{
    public Attacking(CrowController controller) : base(controller) { }
    protected TrackEntry TrackEntry;
    protected float Timer = 0f;
    protected float Duration;

    public override void Jump()
    {
        //Cannot Jump while attacking
    }

    public override void Attack()
    {
        //Cannot Attack while Attacking
    }

    public override void Fall()
    {
        CrowController.Vertical = CrowController.Falling;
    }

    public override void Land()
    {
        CrowController.Vertical = null;
    }

    protected override void SetAnim()
    {
        TrackEntry = CrowController.spine.AnimationState.SetAnimation(0, "Attack1", false);
        CrowController.Vertical = CrowController.IsLanded ? null : Previous;
    }

    protected override void Launch()
    {
        Duration = TrackEntry.Animation.Duration;
        Timer = 0f;
    }
    

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= Duration)
        {
            CrowController.State = CrowController.Vertical ?? (CrowController.Horizontal ?? CrowController.Idle);
        }
        CrowController.Vertical?.Update();
    }
}

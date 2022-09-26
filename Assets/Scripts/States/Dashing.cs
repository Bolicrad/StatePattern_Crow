using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : Attacking
{
    public Dashing(CrowController controller) : base(controller) { }

    public override void Update()
    {
        
    }

    public override IState Fall()
    {
        //cannot fall during dashing
        return null;
    }

    public override IState Land()
    {
        return null;
    }

    public override IState Dash()
    {
        //cannot dash during dashing
        return null;
    }

    protected override void SetAnim()
    {
        TrackEntry = CrowController.spine.AnimationState.SetAnimation(0, "Dash", false);
        CrowController.Vertical = CrowController.isLanded ? new Falling(CrowController) : null;
    }
}

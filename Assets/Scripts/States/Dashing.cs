using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : Attacking
{
    public Dashing(CrowController controller) : base(controller) { }

    private float speed;

    public override void Update()
    {
        CrowController.transform.position += Vector3.left * speed * Time.deltaTime;
        
        Timer += Time.deltaTime;
        if (Timer >= Duration)
        {
            CrowController.State = CrowController.Vertical ?? (CrowController.Horizontal ?? CrowController.Idle);
        }
    }

    public override void Fall()
    {
        //cannot fall during dashing
    }

    public override void Land()
    {
        //cannot land ...
    }

    public override void Dash()
    {
        //cannot dash during dashing
    }

    protected override void SetAnim()
    {
        CrowController.spine.AnimationState.ClearTrack(0);
        TrackEntry = CrowController.spine.AnimationState.SetAnimation(0, "Dash", false);
        CrowController.Vertical = CrowController.IsLanded ? null : CrowController.Falling;
        speed = CrowController.dashDistance / TrackEntry.Animation.Duration *
                Mathf.Sign(CrowController.spine.transform.localScale.x);
        CrowController.r_rigidbody.gravityScale = 0;
        CrowController.r_rigidbody.velocity = Vector2.zero;
    }

    public override void OnExit()
    {
        CrowController.r_rigidbody.gravityScale = 1;
    }
}

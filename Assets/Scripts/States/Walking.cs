using UnityEngine;

public class Walking : Idle
{
    public Walking(CrowController controller) : base(controller) { }
    
    public override void Update()
    {
        HorizontalMove(CrowController.walkSpeed);
    }

    protected void HorizontalMove(float speed)
    {
        float dir = Mathf.Sign(Input.GetAxis("Horizontal"));
        float speedX = dir * speed;
        CrowController.r_rigidbody.velocity = new Vector2(speedX, CrowController.r_rigidbody.velocity.y);
        
        var localScale = CrowController.spine.transform.localScale;
        if (localScale.x * dir > 0)
        {
            localScale = new Vector3(-Mathf.Abs(localScale.x) * dir, localScale.y);
            CrowController.spine.transform.localScale = localScale;
            SetAnim();
        }
    }

    protected override void AddAnim()
    {
        CrowController.spine.AnimationState.AddAnimation(0, "walking", true, 0f);
    }

    protected override void SetAnim()
    {
        CrowController.spine.AnimationState.SetAnimation(0, "walking", true);
    }
}
public class Jumping : Idle
{
    public Jumping(CrowController controller) : base(controller) { }
    
    public override void Update()
    {
        //Detect falling situation here
    }

    public override IState Walk()
    {
        CrowController.Horizontal = CrowController.Walking;
        //Cannot transit from Jumping to Walking
        return null;
    }

    public override IState Run()
    {
        CrowController.Horizontal = CrowController.Running;
        //Cannot transit from Jumping to Running
        return null;
    }
    
    public override IState Stop()
    {
        CrowController.Horizontal = null;
        //Cannot transit from Jumping to Idle
        return null;
    }

    public override IState Jump()
    {
        //Double Jump
        return CrowController.DoubleJumping;
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

    protected void Launch()
    {
        CrowController.spine.AnimationState.AddAnimation(0, "Jump_up", true, 0f);
        
        //Launch Impulse on the rigidbody to jump
    }
    
    public override void OnExit()
    {
        CrowController.spine.AnimationState.SetAnimation(0, "Jump_Top", false);
    }
}
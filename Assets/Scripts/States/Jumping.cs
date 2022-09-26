public class Jumping : Falling
{
    public Jumping(CrowController controller) : base(controller) { }
    
    public override void Update()
    {
        //Detect falling situation here
    }

    public override IState Jump()
    {
        //Double Jump
        return CrowController.DoubleJumping;
    }

    public override IState Fall()
    {
        return new Falling(CrowController);
    }

    public override IState Land()
    {
        
        //Cannot Land when jumping up
        return null;
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
    }
    
    public override void OnExit()
    {
        CrowController.spine.AnimationState.SetAnimation(0, "Jump_Top", false);
    }
}
public class DoubleJumping : Jumping
{
    public DoubleJumping(CrowController controller) : base(controller) { }
    
    public override void Update()
    {
        //Detect falling situation here
    }

    public override IState Jump()
    {
        //Cannot Jump if already double jumped
        return null;
    }

    protected override void SetAnim()
    {
        CrowController.spine.AnimationState.SetAnimation(0, "Jump_doublejump", false);
    }
}

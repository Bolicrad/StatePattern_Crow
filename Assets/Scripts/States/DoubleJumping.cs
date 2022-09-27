public class DoubleJumping : Jumping
{
    public DoubleJumping(CrowController controller) : base(controller) { }
    
    public override void Jump()
    {
        //Cannot Jump if already double jumped
    }

    protected override void SetAnim()
    {
        CrowController.spine.AnimationState.SetAnimation(0, "Jump_doublejump", false);
    }
}

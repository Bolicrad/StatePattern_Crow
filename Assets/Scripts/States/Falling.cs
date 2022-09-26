public class Falling : Idle
{
    public Falling(CrowController controller) : base(controller) { }
    
    public override void Update()
    {
        //Detect landing situation
    }

    public override IState Walk()
    {
        CrowController.Horizontal = CrowController.Walking;
        //Cannot transit from Falling to Walking without landing
        return null;
    }
    
    public override IState Run()
    {
        CrowController.Horizontal = CrowController.Running;
        //Cannot transit from Falling to Running without landing
        return null;
    }

    public override IState Stop()
    {
        CrowController.Horizontal = null;
        //Cannot transit from Falling to Idle without landing
        return null;
    }

    public override IState Jump()
    {
        //Jump result depends on Previous Jump result
        return Previous.Jump();
    }

    public override IState Fall()
    {
        //cannot Enter Falling when it's already falling
        return null;
    }
    
    public override IState Land()
    {
        var result = CrowController.Horizontal ?? CrowController.Idle;
        CrowController.Horizontal = null;
        return result;
    }

    protected override void AddAnim()
    {
        CrowController.spine.AnimationState.AddAnimation(0, "Jump_down", true, 0f);
    }

    protected override void SetAnim()
    {
        CrowController.spine.AnimationState.SetAnimation(0, "Jump_down", true);
    }

    public override void OnExit()
    {
        CrowController.spine.AnimationState.ClearTrack(0);
        CrowController.spine.AnimationState.SetAnimation(0, "Jump_land", false);
    }
}

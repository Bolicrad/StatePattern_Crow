public class Falling : Idle
{
    public Falling(CrowController controller) : base(controller) { }
    
    public override void Update()
    { 
        //Landing Logic
        if (CrowController.IsLanded) CrowController.Land();
        
        CrowController.Horizontal?.Update();
    }

    public override void Walk()
    {
        CrowController.Horizontal = CrowController.Walking;
    }
    
    public override void Run()
    {
        CrowController.Horizontal = CrowController.Running;
    }

    public override void Stop()
    {
        CrowController.Horizontal = CrowController.Idle;
    }

    public override void Jump()
    {
        //Jump result depends on Previous Jump result
        Previous.Jump();
    }

    public override void Fall()
    {
        //cannot Enter Falling when it's already falling
    }
    
    public override void Land()
    {
        CrowController.State = CrowController.Horizontal ?? CrowController.Idle;
        CrowController.Horizontal = null;
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

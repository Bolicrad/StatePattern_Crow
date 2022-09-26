public class Running : Idle
{
    public Running(CrowController controller) : base(controller) { }
  
    public override void Update()
    {
        //Deal with running Logic here
    }

    protected override void AddAnim()
    { 
        CrowController.spine.AnimationState.AddAnimation(0, "running_start", false, 0f);
         CrowController.spine.AnimationState.AddAnimation(0, "running", true, 0f);
    }

    protected override void SetAnim()
    {
        CrowController.spine.AnimationState.SetAnimation(0, "running_start", false);
        CrowController.spine.AnimationState.AddAnimation(0, "running", true, 0f);
    }

    public override void OnExit()
    {
        CrowController.spine.AnimationState.SetAnimation(0, "running_stop", false);
    }
}
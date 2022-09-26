public class Walking : Idle
{
    public Walking(CrowController controller) : base(controller) { }
    
    public override void Update()
    {
        //Deal With Walking Logic Here
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
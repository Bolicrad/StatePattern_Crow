using Spine;

public class Attacking : Jumping
{
    public Attacking(CrowController controller) : base(controller) { }
    protected TrackEntry TrackEntry;

    public override IState Jump()
    {
        return Previous.Jump();
    }

    public override IState Attack()
    {
        //Cannot Attack while Attacking
        return null;
    }

    public override IState Fall()
    {
        CrowController.Vertical = new Falling(CrowController);
        return null;
    }

    public override IState Land()
    {
        CrowController.Vertical = null;
        return null;
    }

    protected override void SetAnim()
    {
        TrackEntry = CrowController.spine.AnimationState.SetAnimation(0, "Attack1", false);
        CrowController.Vertical = CrowController.isLanded ? Previous : null;
    }

    protected override void Launch()
    {
        TrackEntry.Complete += entry =>
        {
            CrowController.State = CrowController.Vertical ?? (CrowController.Horizontal ?? CrowController.Idle);
        };
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        // Deal with attacking logics
    }
}

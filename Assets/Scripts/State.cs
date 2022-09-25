public interface IState
{
    IState Walk();
    IState Run();
    IState Jump();
    IState Dash();
    IState Attack();
}
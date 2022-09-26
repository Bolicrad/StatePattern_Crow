public interface IState
{
    IState Walk();
    IState Run();
    IState Stop();
    IState Jump();
    IState Dash();
    IState Attack();
    IState Fall();
    IState Land();

    void OnEnter(IState previous);
    void OnExit();

    void Update();
}
public interface IState
{
    void Walk();
    void Run();
    void Stop();
    void Jump();
    void Dash();
    void Attack();
    void Fall();
    void Land();

    void OnEnter(IState previous);
    void OnExit();

    void Update();
}

public class StateMashine
{
    public State state { get; set; }

    public void Initialize(State startState)
    {
        state = startState;
        state.EnterState();
    }

    public void ChangeState(State newState)
    {
        state.ExitState();
        state=newState;
        state.EnterState();
    }
}

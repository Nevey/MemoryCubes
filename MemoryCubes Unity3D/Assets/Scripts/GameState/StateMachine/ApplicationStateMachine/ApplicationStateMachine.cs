public class ApplicationStateMachine : StateMachine
{
    public ApplicationStateMachine()
    {
        AddTransition<ToInitializeTransition, InitializeApplicationState>();
    }
}
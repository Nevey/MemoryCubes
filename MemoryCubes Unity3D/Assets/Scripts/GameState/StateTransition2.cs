public class StateTransition2
{
    public StateID StateID { get; private set; }

    public GameStateEvent GameStateEvent { get; private set; }

    public StateTransition2(GameStateEvent gameStateEvent, StateID stateID)
    {
        StateID = stateID;

        GameStateEvent = gameStateEvent;
    }
}
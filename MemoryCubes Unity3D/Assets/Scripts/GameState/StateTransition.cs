public class StateTransition
{
    public GameStateID gameStateEnum { get; set; }

    public GameStateEvent gameStateEventEnum { get; set; }

    public StateTransition(GameStateEvent gameStateEventEnum, GameStateID gameStateEnum)
    {
        this.gameStateEnum = gameStateEnum;

        this.gameStateEventEnum = gameStateEventEnum;
    }
}
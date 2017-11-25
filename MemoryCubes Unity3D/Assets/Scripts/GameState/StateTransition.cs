public class StateTransition
{
    public GameStateType gameStateEnum { get; set; }

    public GameStateEvent gameStateEventEnum { get; set; }

    public StateTransition(GameStateEvent gameStateEventEnum, GameStateType gameStateEnum)
    {
        this.gameStateEnum = gameStateEnum;

        this.gameStateEventEnum = gameStateEventEnum;
    }
}
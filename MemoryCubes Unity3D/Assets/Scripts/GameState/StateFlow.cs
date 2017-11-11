public class StateFlow
{
    public GameStateType gameStateEnum { get; set; }

    public GameStateEvent gameStateEventEnum { get; set; }

    public StateFlow(GameStateEvent gameStateEventEnum, GameStateType gameStateEnum)
    {
        this.gameStateEnum = gameStateEnum;

        this.gameStateEventEnum = gameStateEventEnum;
    }
}
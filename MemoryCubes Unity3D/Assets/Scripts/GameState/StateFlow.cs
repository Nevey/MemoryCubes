public class StateFlow
{
    public GameStateEnum gameStateEnum { get; set; }

    public GameStateEventEnum gameStateEventEnum { get; set; }

    public StateFlow(GameStateEventEnum gameStateEventEnum, GameStateEnum gameStateEnum)
    {
        this.gameStateEnum = gameStateEnum;

        this.gameStateEventEnum = gameStateEventEnum;
    }
}
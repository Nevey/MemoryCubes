using System;

public abstract class GameState
{
    public GameStateType gameStateType { get; set; }

    public event EventHandler<StateFinishedArgs> StateFinishedEvent;

    public GameState(GameStateType gameStateType)
    {
        this.gameStateType = gameStateType;
    }

    /// <summary>
    /// Will be called whenever the state handler is started
    /// </summary>
    public abstract void GameStateStarted();

    /// <summary>
    /// Use to end state and send an event
    /// </summary>
    /// <param name="gameStateEventEnum"></param>
    protected void GameStateFinished(GameStateEvent gameStateEventEnum)
    {
        StateFinishedArgs args = new StateFinishedArgs();

        args.gameStateEventEnum = gameStateEventEnum;

        if (StateFinishedEvent != null)
        {
            StateFinishedEvent(this, args);
        }
    }
}
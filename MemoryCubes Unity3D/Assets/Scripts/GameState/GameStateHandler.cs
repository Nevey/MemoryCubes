using System;

public class GameStateHandler
{
    private StateFinishedArgs args = new StateFinishedArgs();

    public event EventHandler<StateFinishedArgs> StateFinishedEvent;

    public GameStateEnum gameStateEnum { get; set; }

    public GameStateHandler(GameStateEnum gameStateEnum)
    {
        this.gameStateEnum = gameStateEnum;
    }

    // Will be called whenever the state handler is started
    public virtual void GameStateStarted()
    {
        // Do stuff here...
    }

    // Use to end state and send an event
    protected void GameStateFinished(GameStateEventEnum gameStateEventEnum)
    {
        args.gameStateEventEnum = gameStateEventEnum;

        if (StateFinishedEvent != null)
        {
            StateFinishedEvent(this, args);
        }
    }
}
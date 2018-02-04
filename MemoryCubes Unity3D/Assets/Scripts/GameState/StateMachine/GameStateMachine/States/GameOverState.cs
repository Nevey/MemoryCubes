using System;

public class GameOverState : GameState
{
    protected override void PreStart()
    {
        UIController.Instance.GetView<GameOverView>().ShowCompleteEvent += OnGameOverViewShowComplete;

        UIController.Instance.GetView<GameOverView>().HideCompleteEvent += OnGameOverViewHideComplete;
    }

    protected override void PostFinish()
    {
        UIController.Instance.GetView<GameOverView>().ShowCompleteEvent -= OnGameOverViewShowComplete;

        UIController.Instance.GetView<GameOverView>().HideCompleteEvent -= OnGameOverViewHideComplete;
    }

    private void OnGameOverViewShowComplete(UIView obj)
    {
        
    }

    private void OnGameOverViewHideComplete(UIView obj)
    {
        GameStateMachine.Instance.DoTransition<ToMainMenuTransition>();
    }
}
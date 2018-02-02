using System;

public class MainMenuState : GameState
{
    private MainMenuView mainMenuView;

    public MainMenuState()
    {
        mainMenuView = uiController.GetView<MainMenuView>();
    }

    public override void Start()
    {
        base.Start();

        mainMenuView.HideCompleteEvent += OnHideComplete;
    }

    private void OnHideComplete(UIView obj)
    {
        // do transition to start game
    }
}
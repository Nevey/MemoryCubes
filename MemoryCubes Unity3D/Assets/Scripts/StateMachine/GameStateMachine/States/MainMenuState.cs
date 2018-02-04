using System;

// TODO: Create ui/menu state machine?
public class MainMenuState : GameState
{
    protected override void PreStart()
    {
        UIController.Instance.GetView<MainMenuView>().GameStartPressedEvent += OnGameStartPressed;
    }

    private void OnGameStartPressed()
    {
        UIController.Instance.GetView<MainMenuView>().GameStartPressedEvent -= OnGameStartPressed;

        GameStateMachine.Instance.DoTransition<ToBuildCubeTransition>();
    }
}
using System;
using UnityEngine.SceneManagement;

public class InitializeApplicationState : ApplicationState
{
    public override void Start()
    {
        base.Start();

        // TODO: Shove in to a scene controller/manager for more generic approach
        SceneManager.sceneLoaded += OnSceneLoaded;

        SceneManager.LoadScene("Game");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameStateMachine.Instance.DoTransition<ToMainMenuTransition>();
    }
}
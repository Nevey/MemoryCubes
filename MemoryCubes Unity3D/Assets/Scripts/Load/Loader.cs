using UnityEngine;

public class Loader : MonoBehaviour
{
    private ApplicationStateMachine applicationStateMachine;

    private GameStateMachine gameStateMachine;

    private void Awake()
    {
        applicationStateMachine = new ApplicationStateMachine();

        gameStateMachine = new GameStateMachine();
    }
}
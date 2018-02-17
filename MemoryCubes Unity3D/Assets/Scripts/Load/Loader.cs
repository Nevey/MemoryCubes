using UnityEngine;

public class Loader : MonoBehaviour
{
    private ApplicationStateMachine applicationStateMachine;

    private void Awake()
    {
        applicationStateMachine = new ApplicationStateMachine();

        new GameStateMachine();
    }

    private void Start()
    {
        applicationStateMachine.DoTransition<ToInitializeTransition>();
    }
}
using UnityEngine;

public class GameState : State
{
    protected UIController uiController;
    
    public GameState()
    {
        uiController = MonoBehaviour.FindObjectOfType<UIController>();
    }
}
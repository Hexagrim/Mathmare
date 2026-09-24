using UnityEngine;


public class GameStateManager : MonoBehaviour
{
    public PlayerMovement playerMovement;


    //this should manage all the game states like doing paper, is escaped, cutscene, speaking, etc/
    public enum GameState
    {
        Normal,
        SolvingPaper,
        Paused
    }


    public static GameStateManager Instance;
    public GameState CurrentState { get; private set; }



    private void Awake()
    {
        Instance = this;
        CurrentState = GameState.Normal;
    }



    public void SetState(GameState state)
    {
        CurrentState = state;
    }


    private void Update()
    {
        if (CurrentState == GameState.SolvingPaper)
        {
            playerMovement.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else if(CurrentState == GameState.Normal)
        {
            playerMovement.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

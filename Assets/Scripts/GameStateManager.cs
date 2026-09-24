using UnityEngine;


public class GameStateManager : MonoBehaviour
{
    public PlayerMovement playerMovement;

    ItemSway itemSwayScript;
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
        itemSwayScript = FindAnyObjectByType<ItemSway>();
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
            itemSwayScript.enabled = false ;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else if(CurrentState == GameState.Normal)
        {
            playerMovement.enabled = true;
            itemSwayScript.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

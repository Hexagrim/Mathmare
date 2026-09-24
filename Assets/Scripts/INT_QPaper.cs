using UnityEngine;

public class INT_QPaper :Interactables
{
    public GameObject Outline;
    public UI_Manager uiManager;

    protected override void Start()
    {
        base.Start();
        uiManager = FindAnyObjectByType<UI_Manager>();
    }

    public override void Look()
    {
        base.Look();
        Outline.SetActive(true);
    }
    public override void StopLooking()
    {
        base.StopLooking();
        Outline.SetActive(false);
    }
    public override void Use()
    {
        base.Use();
        uiManager.QpaperUI.SetActive(true);
        FindAnyObjectByType<GameStateManager>().SetState(GameStateManager.GameState.SolvingPaper);
    }
    public override void StopUsing()
    {
        base.StopUsing();
        uiManager.QpaperUI.SetActive(false);
        FindAnyObjectByType<GameStateManager>().SetState(GameStateManager.GameState.Normal);
    }

}

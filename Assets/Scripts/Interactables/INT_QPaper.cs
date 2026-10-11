using UnityEngine;

public class INT_QPaper :Interactables
{
    public GameObject Outline;
    public UI_Manager uiManager;

    public QPaperReq Paper1;
    public QPaperReq Paper2;
    public QPaperReq Paper3;

    public QPaperManager qpManager;
    protected override void Start()
    {
        base.Start();
        uiManager = FindAnyObjectByType<UI_Manager>();
        qpManager = FindAnyObjectByType<QPaperManager>();
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
        qpManager.MakePapers(new QPaperReq[]
        {
            Paper1,
            Paper2,
            Paper3
        });
        uiManager.QpaperUI.SetActive(true);
        FindAnyObjectByType<GameStateManager>().SetState(GameStateManager.GameState.SolvingPaper);
        GetComponent<Collider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public override void StopUsing()
    {
        base.StopUsing();
        uiManager.QpaperUI.SetActive(false);
        FindAnyObjectByType<GameStateManager>().SetState(GameStateManager.GameState.Normal);
        Destroy(gameObject);
    }

}

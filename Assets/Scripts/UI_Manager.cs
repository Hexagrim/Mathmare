using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameStateManager gameManager;

    //list of UI stuff

    public GameObject QpaperUI;
    

    //----------------    

    public void QpaperUI_Off()
    {
        FindAnyObjectByType<QPaperManager>().GetComponent<Animator>().SetTrigger("close");
        Invoke(nameof(disableQUI), 0.43f);

    }


    void disableQUI()
    {
        QpaperUI.SetActive(false);
    }
}

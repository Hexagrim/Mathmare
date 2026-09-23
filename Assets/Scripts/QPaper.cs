using UnityEngine;

public class QPaper :Interactables
{
    public GameObject Outline;
    public GameObject QUI;
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
        QUI.SetActive(true);
    }
    public override void StopUsing()
    {
        base.StopUsing();
        QUI.SetActive(false);
    }

}

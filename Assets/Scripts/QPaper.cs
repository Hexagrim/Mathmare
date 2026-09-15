using UnityEngine;

public class QPaper :Interactables
{
    public GameObject Outline;
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

}

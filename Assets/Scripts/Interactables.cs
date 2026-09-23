using UnityEngine;

public abstract class Interactables : MonoBehaviour
{
    protected bool Using;
    //fun :>
    protected virtual void Start()
    {
       
    }

    public virtual void Look()
    {

    }

    public virtual void StopLooking()
    {

    }

    public virtual void Use()
    {
        Using = true;
    }

    public virtual void StopUsing()
    {
        Using = false;
    }
}
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int itemId;

    public abstract void PickUp();
    public abstract void Drop();

    protected virtual void Update()
    {
        //this run for all items while using the method with any item need to use override void :> (btw first time OOP-ing a game thing)
    }
    protected virtual void Start()
    {
        //same here
        Debug.Log(name + ":" + itemId);

    }
    public abstract void Use();

}

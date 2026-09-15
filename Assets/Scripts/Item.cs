using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Outline))]
public abstract class Item : MonoBehaviour
{
    
    public int itemId;

    public virtual void PickUp()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<ItemSway>().enabled = true;
    }
    
    public virtual void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        //GetComponent<ItemSway>().enabled = false;
    }

    protected virtual void Update()
    {
        //this run for all items while using the method with any item need to use override void :> (btw first time OOP-ing a game thing)
    }
    protected virtual void Start()
    {
        //same here
        
        //Debug.Log(name + ":" + itemId);

    }
    public abstract void Use();

}

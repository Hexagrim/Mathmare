using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    public Animator anim;
    Collider coll;
    void Start()
    {
        coll= GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.IsInTransition(0))
        {
            coll.enabled = false;
        }
        else
        {
            coll.enabled = true;
        }
    }
}

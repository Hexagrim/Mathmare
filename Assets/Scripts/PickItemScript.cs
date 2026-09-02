using Unity.AppUI.UI;
using UnityEngine;

public class PickItemScript : MonoBehaviour
{
    public Transform[] hands;
    public Item heldItem;
    Transform heldItemHand;
    public float angleSpeed;

    bool lookingAtItem;
    void Update()
    {
        //ray check
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 10f) && hit.collider.gameObject.GetComponent<Item>() != null)
            lookingAtItem = true;


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
            {
                if (lookingAtItem)
                {
                    PickItem(hit.collider.gameObject.GetComponent<Item>());
                }
            }

            else
            {
                DropItem();
            }

        }

        if (heldItem != null) MoveItem();


        // code for outline

        if (lookingAtItem)
        {

            hit.collider.GetComponent<Outline>().enabled = true;

        }


    }

    void PickItem(Item pickItem)
    {
        heldItem = pickItem;
        heldItem.PickUp();
        heldItemHand = hands[heldItem.itemId];
        //heldItem.transform.parent = hands[heldItem.itemId];
    }

    void DropItem()
    {

    }

    void MoveItem()
    {
        heldItem.transform.rotation = Quaternion.Lerp(heldItem.transform.rotation, heldItemHand.rotation, angleSpeed * Time.deltaTime);
    }

}

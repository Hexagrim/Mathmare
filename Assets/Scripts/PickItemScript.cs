using Unity.AppUI.UI;
using UnityEngine;

public class PickItemScript : MonoBehaviour
{
    public Transform[] hands;
    public Item heldItem;
    Transform heldItemHand;
    public float angleSpeed;

    bool lookingAtItem;
    GameObject lookItem;
    
    void Update()
    {
        //ray check
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 7f) && hit.collider.gameObject.GetComponent<Item>() != null)
        {

            lookingAtItem = true;
            lookItem = hit.collider.gameObject;

        }

        else
        {
            lookingAtItem = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
            {
                if (lookingAtItem)
                {
                    PickItem(lookItem.GetComponent<Item>());
                }
            }

            else
            {
                DropItem();
            }

        }

        //if (heldItem != null && heldItemHand != null)
        //{
        //    MoveItem();
        //    heldItem.transform.position = heldItemHand.transform.position;
        //}


        // code for outline
        if (lookItem != null)
        {
            if (lookingAtItem)
            {
                if (!lookItem.GetComponent<Outline>().enabled) lookItem.GetComponent<Outline>().enabled = true;
            }
            else if (lookItem.GetComponent<Outline>().enabled)
            {
                lookItem.GetComponent<Outline>().enabled = false;
            }
        }
        
        if(heldItem!=null && Input.GetKeyDown(KeyCode.F))
        {
            heldItem.Use();
        }

    }

    void PickItem(Item pickItem)
    {
        pickItem.GetComponent<Outline>().enabled = false;
        heldItem = pickItem;
        heldItem.PickUp();
        heldItemHand = hands[heldItem.itemId];
        heldItem.gameObject.layer = LayerMask.NameToLayer("holdLayer");
        //heldItem.transform.parent = hands[heldItem.itemId];
    }

    void DropItem()
    {
        heldItem.Drop();
        heldItemHand = null;
        heldItem.gameObject.layer = default;
        heldItem.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 300);
        heldItem = null;

    }

    void MoveItem()
    {
        heldItem.transform.rotation = Quaternion.Lerp(heldItem.transform.rotation, heldItemHand.rotation, angleSpeed * Time.deltaTime);

    }
    private void LateUpdate()
    {
        if (heldItem != null && heldItemHand != null)
        {
            MoveItem();
            heldItem.transform.position = Vector3.Lerp(heldItem.transform.position, heldItemHand.transform.position, 100 * Time.deltaTime);
        }
    }
}

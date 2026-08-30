using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;

    public float throwForce = 500f;
    public float pickUpRange = 100f;
    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private int LayerNumber;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }

    void Update()
    {
       
        Debug.DrawRay(transform.position, transform.forward * pickUpRange, Color.red);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange))
                {
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                StopClipping();
                DropObject();
                StopClipping();
            }
        }

        if (heldObj != null)
        {
            MoveObject();

        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform;
            heldObj.layer = LayerNumber;

            Physics.IgnoreCollision(
                heldObj.GetComponent<Collider>(),
                player.GetComponent<Collider>(),
                true
            );
        }
    }

    void DropObject()
    {
        Physics.IgnoreCollision(
            heldObj.GetComponent<Collider>(),
            player.GetComponent<Collider>(),
            false
        );

        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        Collider[] hits = Physics.OverlapSphere(heldObj.transform.position, 0.01f);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Wall") || hit.CompareTag("Ground"))
            {
                heldObj.transform.position = player.transform.position;
                break;
            }
        }
        heldObj = null;
    }

    void MoveObject()
    {
        heldObj.transform.position = Vector3.Lerp(heldObj.transform.position, holdPos.transform.position, 20*Time.deltaTime);
        
    }
    void StopClipping()
    {
        var clipRange = Vector3.Distance(
            heldObj.transform.position,
            transform.position
        );

        RaycastHit[] hits;

        hits = Physics.RaycastAll(
            transform.position,
            transform.TransformDirection(Vector3.forward),
            clipRange
        );

        if (hits.Length > 1)
        {
            heldObj.transform.position =
                transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }

}
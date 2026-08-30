using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;

    public float throwForce = 500f;
    public float pickUpRange = 100f;
    [SerializeField] private float smoothTime = 0.05f; 

    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private int LayerNumber;
    public bool isClipping;
    private Vector3 velocity;

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
                if (isClipping)
                {
                    heldObj.transform.position = player.transform.position;
                }
                DropObject();
            }
        }

        if (heldObj != null)
        {
            MoveObject();
        }
    }

    private void FixedUpdate()
    {
        if (heldObj != null)
        {
            isClipping = false;
            Collider[] hits = Physics.OverlapSphere(
                heldObj.transform.position,
                0.5f
            );

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Wall") || hit.CompareTag("Ground"))
                {
                    isClipping = true;
                    break;
                }
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObj.transform.parent = holdPos;
            heldObj.layer = LayerNumber;
          
            velocity = Vector3.zero;

            Physics.IgnoreCollision(
                heldObj.GetComponent<Collider>(),
                player.GetComponent<Collider>(),
                true
            );
        }
    }

    void DropObject()
    {
        if (isClipping)
        {
            heldObj.transform.position = player.transform.position;
        }
        Physics.IgnoreCollision(
            heldObj.GetComponent<Collider>(),
            player.GetComponent<Collider>(),
            false
        );

        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        if (isClipping)
        {
            heldObj.transform.position = player.transform.position;
        }
        heldObj = null;
    }

    void MoveObject()
    {
        // Smoothly damp local position towards (0,0,0) relative to holdPos parent
        heldObj.transform.localPosition = Vector3.SmoothDamp(
            heldObj.transform.localPosition,
            Vector3.zero,
            ref velocity,
            smoothTime
        );

        // Smoothly interpolate rotation to match parent orientation
        heldObj.transform.localRotation = Quaternion.Slerp(
            heldObj.transform.localRotation,
            Quaternion.identity,
            20f * Time.deltaTime
        );
    }

    void OnDrawGizmos()
    {
        if (heldObj != null)
        {
            Gizmos.color = isClipping ? Color.red : Color.green;
            Gizmos.DrawWireSphere(
                heldObj.transform.position,
                0.5f
            );
        }
    }
}
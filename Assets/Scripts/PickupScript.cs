using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
public class PickupScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;

    public float throwForce = 500f;
    public float pickUpRange = 100f;
    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private int LayerNumber;
    public bool isClipping;
    private Vector3 velocity;

    public Material[] outlineMats;
    private MeshRenderer currentRenderer; 
    private Material[] originalMaterials;

    public Transform parentHand;
    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position,transform.forward,out hit,pickUpRange) && hit.collider.CompareTag("canPickUp"))
        {
            MeshRenderer newRenderer =
                hit.collider.GetComponentInParent<MeshRenderer>();
            if (newRenderer != currentRenderer)
            {

                RemoveOutline();

                currentRenderer = newRenderer;

                if (currentRenderer != null)
                {
                    originalMaterials = currentRenderer.sharedMaterials;
                    currentRenderer.GetComponent<Outline>().OutlineWidth = 5;
                    currentRenderer.sharedMaterials = originalMaterials.Concat(outlineMats).ToArray();

                }
            }
        }
        else
        {
            RemoveOutline();
        }


        Debug.DrawRay(transform.position, transform.forward * pickUpRange, Color.red);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {

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
            heldObj.transform.position = holdPos.position;
            MoveObject();

        }

    }


    void RemoveOutline()
    {
        if (currentRenderer != null)
        {
            if (currentRenderer.GetComponent<Outline>())
            {
                currentRenderer.GetComponent<Outline>().OutlineWidth = 0;

            }
            currentRenderer.sharedMaterials = originalMaterials;
            currentRenderer = null;
            originalMaterials = null;

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
            heldObj.transform.rotation = Quaternion.Euler(0,0,0);
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;

            Vector3 localScale = heldObj.transform.localScale;
            heldObjRb.transform.parent = parentHand;
            heldObj.transform.localScale = localScale;

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
        Vector3 localScale = heldObj.transform.localScale;
        heldObjRb.transform.parent = null;
        heldObj.transform.localScale = localScale;
        if(isClipping)
        {
            heldObj.transform.position = player.transform.position;
        }
        heldObjRb.AddForce(transform.forward.normalized * 500);
        heldObj = null;
    }

    void MoveObject()
    {
        heldObj.transform.rotation = Quaternion.Lerp(heldObj.transform.rotation, holdPos.rotation, 20 * Time.deltaTime);
        //heldObj.transform.position = Vector3.Lerp(heldObj.transform.position,holdPos.position,20*Time.deltaTime);
        //heldObj.transform.position = Vector3.SmoothDamp(
        //    heldObj.transform.position,
        //    holdPos.position,
        //    ref velocity,
        //    0.05f
        //);

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
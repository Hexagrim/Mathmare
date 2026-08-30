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

        if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange) && hit.collider.CompareTag("canPickUp"))
        {
            MeshRenderer newRenderer = hit.collider.GetComponentInParent<MeshRenderer>();
            if (newRenderer != currentRenderer)
            {
                RemoveOutline();

                currentRenderer = newRenderer;

                if (currentRenderer != null)
                {
                    originalMaterials = currentRenderer.sharedMaterials;
                    if (currentRenderer.GetComponent<Outline>())
                    {
                        currentRenderer.GetComponent<Outline>().OutlineWidth = 5;
                    }
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
                    if (hit.transform.gameObject.CompareTag("canPickUp"))
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

    /// <summary>
    /// Traverse up the hierarchy until reaching the top-most root transform.
    /// </summary>
    private Transform GetRootTransform(Transform current)
    {
        while (current.parent != null)
        {
            current = current.parent;
        }
        return current;
    }

    void PickUpObject(GameObject pickUpObj)
    {

        Transform rootTransform = GetRootTransform(pickUpObj.transform);
        heldObj = rootTransform.gameObject;
        heldObj.transform.localPosition = Vector3.zero;
        heldObj.transform.localRotation = Quaternion.identity;
        //heldObj.transform.localRotation = Quaternion.identity;
        heldObjRb = heldObj.GetComponentInChildren<Rigidbody>();
        if (heldObjRb != null)
        {
            heldObjRb.isKinematic = true;

            Vector3 localScale = heldObj.transform.localScale;
            heldObj.transform.parent = parentHand;
            heldObj.transform.localScale = localScale;

            heldObj.layer = LayerNumber;
            Collider[] objColliders = heldObj.GetComponentsInChildren<Collider>();
            Collider playerCollider = player.GetComponent<Collider>();
            foreach (Collider col in objColliders)
            {
                Physics.IgnoreCollision(col, playerCollider, true);
            }
        }
    }
    void DropObject()
    {
        if (heldObj == null) return;

        if (isClipping)
        {
            heldObj.transform.position = player.transform.position;
        }

        // Re-enable collisions for all colliders under the root object
        Collider[] objColliders = heldObj.GetComponentsInChildren<Collider>();
        Collider playerCollider = player.GetComponent<Collider>();
        foreach (Collider col in objColliders)
        {
            Physics.IgnoreCollision(col, playerCollider, false);
        }

        heldObj.layer = 0;
        Vector3 localScale = heldObj.transform.localScale;
        // Unparent the root node (sets parent to null)
        heldObj.transform.parent = null;
        heldObj.transform.localScale = localScale;

        if (isClipping)
        {
            heldObj.transform.position = player.transform.position;
        }

        if (heldObjRb != null)
        {
            heldObjRb.isKinematic = false;
            heldObjRb.AddForce(transform.forward.normalized * throwForce);
        }

        heldObj = null;
        heldObjRb = null;
    }

    void MoveObject()
    {
        heldObj.transform.rotation = Quaternion.Lerp(heldObj.transform.rotation, holdPos.rotation, 20 * Time.deltaTime);
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
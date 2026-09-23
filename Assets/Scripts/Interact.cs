using UnityEditor.Build;
using UnityEngine;



public class Interact : MonoBehaviour
{
    float interactDistance = 3;

    private Interactables lookObj;
    public bool lookingAtObj;
    public PlayerMovement player;
    void LockPlayer()
    {
        player.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void UnlockPlayer()
    {
        player.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()

    {
        FindAnyObjectByType<UI_CrosshairScript>().isLookingInteractables = lookingAtObj;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Interactables interactable = hit.collider.GetComponent<Interactables>();

            if (interactable != null)
            {
                if (lookObj != interactable)
                {
                    if (lookObj != null)
                        lookObj.StopLooking();

                    lookObj = interactable;
                    lookingAtObj = true;
                    lookObj.Look();
                }
            }
            else
            {
                StopLooking();
            }
        }
        else
        {
            StopLooking();
        }
        if(lookingAtObj && lookObj.gameObject != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                lookObj.Use();
                LockPlayer();
            }
        }
    }

    //using a better seperated structure for this cause its so messy with the old method;;;;
    void StopLooking()
    {
        if (lookObj != null)
        {
            lookObj.StopLooking();
            lookObj = null;
        }

        lookingAtObj = false;
    }
}
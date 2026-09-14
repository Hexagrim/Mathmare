using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    float interactDistance = 3;

    private GameObject currentLookedAtObject;
    private 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance))
        {
            if (hit.collider.GetComponent<QPaper>())
            {
                if (currentLookedAtObject.GetComponent<QPaper>().lookingAt) currentLookedAtObject.GetComponent<QPaper>().lookingAt = false;
                currentLookedAtObject = hit.collider.gameObject;
            }
            else
            {
                
                currentLookedAtObject = null;
            }
        }
        
        if(currentLookedAtObject != null && currentLookedAtObject.GetComponent<QPaper>())
        {
            currentLookedAtObject.GetComponent<QPaper>
        }

    }
} 
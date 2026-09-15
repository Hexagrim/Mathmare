using UnityEngine;

public class UI_CrosshairScript : MonoBehaviour
{

    public GameObject Crosshair;
    public GameObject E_interact;
    public GameObject F_interact;

    public bool isLookingItem;
    public bool isLookingInteractables;

    void Start()
    {
        
    }

    void Update()
    {
        if (isLookingItem)
        {
            E_interact.SetActive(true);
            F_interact.SetActive(false);
            Crosshair.SetActive(false);
        }
        else if(isLookingInteractables)
        {
            E_interact.SetActive(false);
            F_interact.SetActive(true);
            Crosshair.SetActive(false);
        }
        else
        {
            E_interact.SetActive(false);
            F_interact.SetActive(false);
            Crosshair.SetActive(true);
        }
    }
}

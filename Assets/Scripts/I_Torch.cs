using UnityEngine;
using UnityEngine.Rendering.Universal;

public class I_Torch : Item
{
    public GameObject lightSource;
    
    public override void Use()
    {
        lightSource.SetActive(!lightSource.activeInHierarchy);
    }

    public override void Drop() 
    {
        base.Drop();
        lightSource.SetActive(false);
    }
}

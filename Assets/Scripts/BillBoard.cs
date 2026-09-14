using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Vector3 cameraDir;

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

    }
}

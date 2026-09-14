using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Vector3 cameraDir;

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

        //cameraDir = Camera.main.transform.position;
        //cameraDir.y = 0;
        //
        //transform.rotation = Quaternion.LookRotation(cameraDir);    

    }
}

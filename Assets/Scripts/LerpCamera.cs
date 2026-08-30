using UnityEngine;

public class LerpCamera : MonoBehaviour
{
    public GameObject MainCam;
    public float posSpeed,rotSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,MainCam.transform.position, posSpeed*Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, MainCam.transform.rotation, rotSpeed * Time.deltaTime);
    }
}

using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float moveInput;
    float turnInput;
    private CharacterController ch;

    public float moveSpeed = 10;
    public float sens;

    public GameObject camera;

    public float gravity;
    float verticalVelocity;

    float verticalRotation;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ch = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();

        //movement code:
        Vector3 moveDir = transform.forward * moveInput + transform.right * turnInput;
        Vector3 move = moveDir.normalized * moveSpeed;
        move.y = verticalForce();
        ch.Move(move * Time.deltaTime);

        //cameraLook
        MouseLook();

    }

    void InputManagement()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");
    }
    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 80f);

        transform.Rotate(mouseX * sens * Vector3.up);
        camera.transform.localRotation = Quaternion.Euler(verticalRotation, camera.transform.localRotation.y, camera.transform.localRotation.z);

        
    }
    float verticalForce()
    {
        if(ch.isGrounded)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }
}

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

    public Camera camera;
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
        ch.Move(moveDir.normalized*moveSpeed*Time.deltaTime);

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

        transform.Rotate(mouseX * sens * Vector3.up);
        camera.transform.Rotate(mouseY * sens * Vector3.left);
    }
}

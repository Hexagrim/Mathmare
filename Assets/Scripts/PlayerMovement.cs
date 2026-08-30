using Unity.Cinemachine;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public float runMultipler;
    float moveMultipler = 1;

    public CinemachineCamera virtualCamera;

    float baseCamFov;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ch = GetComponent<CharacterController>();
        baseCamFov = virtualCamera.Lens.FieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();

        //movement code:
        Vector3 moveDir = transform.forward * moveInput + transform.right * turnInput;
        Vector3 move = moveDir.normalized * moveSpeed * moveMultipler;
        move.y = verticalForce();
        ch.Move(move * Time.deltaTime);

        //cameraLook
        MouseLook();

        //run thingy
        RunLogic();

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
    void RunLogic()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveMultipler = runMultipler;
            GetComponent<HeadBobController>().freqMult = moveMultipler;
            if (virtualCamera.Lens.FieldOfView >= baseCamFov * 1.1f) virtualCamera.Lens.FieldOfView = baseCamFov * 1.1f;
            else virtualCamera.Lens.FieldOfView += 100 * Time.deltaTime;

        }
        else
        {
            moveMultipler = 1;
            GetComponent<HeadBobController>().freqMult = 1;
            if (virtualCamera.Lens.FieldOfView <= baseCamFov) virtualCamera.Lens.FieldOfView = baseCamFov;
            else virtualCamera.Lens.FieldOfView -= 100 * Time.deltaTime;
        }
    }

}

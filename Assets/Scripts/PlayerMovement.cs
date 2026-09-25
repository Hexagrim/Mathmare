using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Round<TInput, TOutput>;

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
    bool isRunning = false;

    public float maxRun;
    private float runRechargeTimer;
    public float run;

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
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) isRunning = false;

        Vector3 movement = new Vector3(ch.velocity.x, 0, ch.velocity.z);



        if (isRunning && Input.GetAxisRaw("Vertical") == 1)
        {
            moveMultipler = runMultipler;
            GetComponent<HeadBobController>().freqMult = moveMultipler;
            if (virtualCamera.Lens.FieldOfView >= baseCamFov * 1.1f) virtualCamera.Lens.FieldOfView = baseCamFov * 1.1f;
            else virtualCamera.Lens.FieldOfView += 70 * Time.deltaTime;

            if (run > 0)
            {
                run -= Time.deltaTime;
                runRechargeTimer = 5f;
            }
        }
        else
        {

            moveMultipler = 1;
            GetComponent<HeadBobController>().freqMult = 1;
            if (virtualCamera.Lens.FieldOfView <= baseCamFov) virtualCamera.Lens.FieldOfView = baseCamFov;
            else virtualCamera.Lens.FieldOfView -= 70 * Time.deltaTime;

            if(runRechargeTimer > 0) runRechargeTimer -= Time.deltaTime;
            else if(run < maxRun) run += 5 * Time.deltaTime;

        }
        run = Mathf.Clamp(run, 0, maxRun);
    }

}

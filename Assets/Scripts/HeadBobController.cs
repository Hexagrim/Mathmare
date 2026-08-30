using Unity.AppUI.UI;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] bool _enabled = true;
    [SerializeField] private float amplitude = 0.015f;
    [SerializeField] private float frequency = 10.0f;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    private float _toggleSpeed = 3f;
    private Vector3 _startPos;
    private CharacterController _controller;

    public float tiltSpeed;

    public float freqMult;
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;

    }
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Sin(Time.time * frequency * freqMult) * amplitude;
        pos.x = Mathf.Cos(Time.time * frequency * freqMult / 2) * amplitude / 4;
        return pos;
    }
    private void CheckMotion()
    {
        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
        ResetMotion();
        if (speed < _toggleSpeed) return;
        if(!_controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }
    private void ResetMotion()
    {
        if(_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }
    private void PlayMotion(Vector3 motion)
    {
        _camera.position += transform.TransformDirection(motion);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) return;
        CheckMotion();
        CamTilt(2);

    }

    void CamTilt(float tiltAmt)
    {
        Vector3 localVelocity =
            transform.InverseTransformDirection(_controller.velocity);

        // LEFT / RIGHT → Z tilt
        float targetSideTilt = 0f;

        if (localVelocity.x > 0.1f)
            targetSideTilt = -tiltAmt;
        else if (localVelocity.x < -0.1f)
            targetSideTilt = tiltAmt;


        // FORWARD / BACKWARD → X tilt
        float targetForwardTilt = 0f;

        if (localVelocity.z > 0.1f)
            targetForwardTilt = tiltAmt * 2;
        else if (localVelocity.z < -0.1f)
            targetForwardTilt = -tiltAmt * 2;


        // Current rotation
        Vector3 currentRotation = _camera.localEulerAngles;

        float currentX = currentRotation.x;
        float currentZ = currentRotation.z;

        if (currentX > 180f) currentX -= 360f;
        if (currentZ > 180f) currentZ -= 360f;


        // Smooth both tilts
        float newX = Mathf.LerpAngle(
            currentX,
            targetForwardTilt,
            tiltSpeed * Time.deltaTime
        );

        float newZ = Mathf.LerpAngle(
            currentZ,
            targetSideTilt,
            tiltSpeed * Time.deltaTime
        );


        _camera.localEulerAngles = new Vector3(
            newX,
            0f,
            newZ
        );
    }
}

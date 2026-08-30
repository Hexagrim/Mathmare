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

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;

    }
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x = Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
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
        float targetTilt = 0f;
        Vector3 localVelocity = transform.InverseTransformDirection(_controller.velocity);
        if (localVelocity.x > 0.1f)
        {
            targetTilt = -tiltAmt;
        }
        else if (localVelocity.x < -0.1f)
        {
            targetTilt = tiltAmt;
        }


        float currentTilt = _camera.localEulerAngles.z;

        if (currentTilt > 180f)
            currentTilt -= 360f;

        float newTilt = Mathf.LerpAngle(
            currentTilt,
            targetTilt,
            tiltSpeed * Time.deltaTime
        );

        _camera.localEulerAngles = new Vector3(
            _camera.localEulerAngles.x,
            0f,
            newTilt
        );
    }
}

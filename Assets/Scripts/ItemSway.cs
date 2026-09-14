using System;
using UnityEngine;

public class ItemSway : MonoBehaviour
{
    public float smooth;
    public float multiplier;
    public float bobSpeed = 8f;
    public float bobAmount = 0.03f;
    public CharacterController mover;

    private Vector3 startPosition;
    private float bobTimer;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * multiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * multiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            smooth * Time.deltaTime
        );

        float speed = mover.velocity.magnitude;

        if (speed > 0.1f)
        {
            float intensity = Mathf.Clamp01(speed / 6f);

            bobTimer += Time.deltaTime * bobSpeed * (1f + intensity);

            float bobX = Mathf.Sin(bobTimer) * bobAmount * (1f + intensity) * 0.7f;
            float bobY = Mathf.Cos(bobTimer * 2f) * bobAmount * (1f + intensity);

            Vector3 targetPosition = startPosition + new Vector3(bobX, bobY, 0f);

            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                targetPosition,
                smooth * Time.deltaTime
            );
        }
        else
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                startPosition,
                smooth * Time.deltaTime
            );
        }
    }
}
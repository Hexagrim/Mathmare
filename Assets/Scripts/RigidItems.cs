using UnityEngine;

public class RigidItems : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void ResolvePenetration()
    {
        Physics.SyncTransforms();

        for (int i = 0; i < 10; i++)
        {
            Collider[] hits = Physics.OverlapBox(
                col.bounds.center,
                col.bounds.extents,
                transform.rotation
            );

            bool pushed = false;

            foreach (Collider hit in hits)
            {
                if (hit == col)
                    continue;

                if (!hit.CompareTag("Wall") && !hit.CompareTag("Ground"))
                    continue;

                Vector3 direction;
                float distance;

                if (Physics.ComputePenetration(
                    col,
                    transform.position,
                    transform.rotation,
                    hit,
                    hit.transform.position,
                    hit.transform.rotation,
                    out direction,
                    out distance))
                {
                    transform.position += direction * (distance + 0.01f);
                    pushed = true;
                }
            }

            if (!pushed)
                break;
        }

        Physics.SyncTransforms();
    }
}
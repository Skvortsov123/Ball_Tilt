using UnityEngine;

public class PullDown : MonoBehaviour
{
    public float force = 20f;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        rb.AddForce(Vector3.down * force, ForceMode.Acceleration);
    }
}

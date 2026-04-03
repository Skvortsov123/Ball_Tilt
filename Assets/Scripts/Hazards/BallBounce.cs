using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float bounceMultiplier = 2f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Kolla om objektet har r�tt layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bouncy"))
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 bounce = Vector3.Reflect(rb.linearVelocity, normal);
            rb.linearVelocity = bounce * bounceMultiplier;
        }
    }
}
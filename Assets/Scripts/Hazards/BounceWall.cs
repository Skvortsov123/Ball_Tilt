using UnityEngine;

public class BounceWall : MonoBehaviour
{
    public float bounceForce = 20f;
    public float cooldownTime = 0.1f;
    private float lastBouncTime;

    void Start()
    {
        lastBouncTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if (rb != null && Time.time > lastBouncTime + cooldownTime)
        {
            lastBouncTime = Time.time;
            // Calculate direction based on the trampoline's rotation
            // transform.up works whether it's on floor (up), wall (sideways), etc.
            Vector3 bounceDirection = transform.up;

            rb.linearVelocity = new Vector3(0, 0f, 0);

            rb.linearVelocity = bounceDirection.normalized * bounceForce;
            Debug.Log("Boing!");
        }
    }
}
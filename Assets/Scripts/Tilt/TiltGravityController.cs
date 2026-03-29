using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TiltGravityController : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 offset;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 tilt = Input.acceleration;
        rb.AddForce((new Vector3(tilt.x, tilt.z, tilt.y) + offset ) * speed * rb.mass);
    }
}

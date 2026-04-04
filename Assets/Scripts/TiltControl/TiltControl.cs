using UnityEngine;
using UnityEngine.InputSystem;


public class TiltControl : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 offset;
    public bool keyboardControl;

    private Vector2 moveInput;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        
    }

    void FixedUpdate()
    {
        Vector3 tilt = Input.acceleration;
        rb.AddForce((new Vector3(tilt.y, tilt.z, -tilt.x) + offset ) * speed * rb.mass);
        if (keyboardControl)
        {
            rb.AddForce(new Vector3(moveInput.y , 0, -moveInput.x) * speed);
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

}
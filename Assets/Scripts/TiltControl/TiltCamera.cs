using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TiltCamera : MonoBehaviour
{
    public float speed = 5;
    public float amplitude = 10;
    private Quaternion offset;
    
    void Start()
    {
        offset = gameObject.transform.rotation;
    }

    void Update()
    {
        Vector3 tilt = Input.acceleration.normalized;
        Quaternion yaw   = Quaternion.AngleAxis(tilt.y * -amplitude, transform.right);   // I dont know how it works but it does, dont touch!
        Quaternion pitch = Quaternion.AngleAxis(-tilt.x * -amplitude, Vector3.right);
        Quaternion target = yaw * pitch * offset;
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, target, Time.deltaTime * speed);
    }
}

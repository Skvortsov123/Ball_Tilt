using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TiltLight : MonoBehaviour
{
    public float speed = 2;
    public float amplitude = 10;
    private TiltControl control;

    private Quaternion offset;
    
    void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(90, 0, -90);   // z must be -90 to make light rotate at right direction
        offset = gameObject.transform.rotation;
        control = FindFirstObjectByType<TiltControl>();
    }

    void Update()
    {
        Vector3 tilt = new Vector3(-control.getControl().z, control.getControl().x, control.getControl().y);
        Quaternion yaw   = Quaternion.AngleAxis(tilt.y * amplitude, transform.right);   // I dont know how it works but it does, dont touch!
        Quaternion pitch = Quaternion.AngleAxis(-tilt.x * amplitude, Vector3.right);
        Quaternion target = yaw * pitch * offset;

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, target, Time.deltaTime * speed);
    }
}

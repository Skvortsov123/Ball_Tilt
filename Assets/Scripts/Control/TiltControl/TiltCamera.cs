using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TiltCamera : MonoBehaviour
{
    public float tiltSpeed = 2;
    public float tiltAmplitude = 10;
    public GameObject target;
    public float followSpeed = 2f;
    private TiltControl control;

    private Quaternion offset;
    
    void Start()
    {
        offset = gameObject.transform.rotation;
        control = FindFirstObjectByType<TiltControl>();
    }

    void Update()
    {
        //Tilt
        Vector3 tilt = new Vector3(-control.getControl().z, control.getControl().x, control.getControl().y);
        Quaternion yaw   = Quaternion.AngleAxis(tilt.y * -tiltAmplitude, transform.right);   // I dont know how it works but it does, dont touch!
        Quaternion pitch = Quaternion.AngleAxis(-tilt.x * -tiltAmplitude, Vector3.right);
        Quaternion targetTilt = yaw * pitch * offset;

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetTilt, Time.deltaTime * tiltSpeed);

        //Follow
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), followSpeed * Time.deltaTime);         
        }
    }
}

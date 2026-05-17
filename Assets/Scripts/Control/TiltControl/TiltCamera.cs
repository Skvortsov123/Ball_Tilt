using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TiltCamera : MonoBehaviour
{
    public float speed = 2;
    public float amplitude = 10;
    public GameObject target;
    public float followSpeed = 2f;
    private TiltControl control;

    private Quaternion offset;


    [Header("Aspect Ratio Zoom")]
    public float referenceAspect = 19.5f / 9f; // the aspect ratio your level was designed for (usually phone)
    public float referenceDistance = 30f;    // default camera zoom distance at reference aspect
    public float minDistance = 30f;           // prevents camera getting too close
    public float maxDistance = 45f;          // prevents camera getting too far away

    void Start()
    {
        offset = gameObject.transform.rotation;
        control = FindFirstObjectByType<TiltControl>();

        AdjustCameraZoom(); // run once when level starts
    }

    void Update()
    {
        //Tilt
        Vector3 tilt = new Vector3(-control.getControl().z, control.getControl().x, control.getControl().y);
        Quaternion yaw = Quaternion.AngleAxis(Mathf.Clamp(tilt.y * -amplitude, -amplitude, amplitude), transform.right);   // I dont know how it works but it does, dont touch!
        Quaternion pitch = Quaternion.AngleAxis(Mathf.Clamp(-tilt.x * -amplitude, -amplitude, amplitude), Vector3.right);
        Quaternion targetTilt = yaw * pitch * offset;

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetTilt, Time.deltaTime * speed);

        //Follow
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), followSpeed * Time.deltaTime);
        }
    }

    void AdjustCameraZoom()
    {
        float currentAspect = (float)Screen.width / Screen.height; // current device shape (phone/tablet)

        float scale = referenceAspect / currentAspect; // wider screen = smaller scale

        float newDistance = referenceDistance * scale; // calculate how far camera should be
        newDistance = Mathf.Clamp(newDistance, minDistance, maxDistance); // keep within safe range

        if (target != null)
        {
            // If following player:
            // keep same viewing angle but move camera closer/further from the ball

            Vector3 direction = (transform.position - target.transform.position).normalized; // direction from target to camera
            transform.position = target.transform.position + direction * newDistance; // apply zoom around target
        }
        else
        {
            // Static camera:
            // only move on y axis 

            Vector3 pos = transform.position;
            pos.y = newDistance; // farther back on tablets so level sides stay visible
            transform.position = pos;
        }
    }

}
